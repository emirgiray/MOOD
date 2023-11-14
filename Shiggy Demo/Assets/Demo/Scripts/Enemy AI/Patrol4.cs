using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol4 : StateMachineBehaviour
{
    //Reference Values
    float timer;
    float _chaseRange = 1;
    bool _lineOfSight = false;
    public bool isTargetHit = false;
    //References
    GameObject cube;
    NavMeshAgent agent;
    Transform _player;
    LayerMask _ground;

    //List Of Waypoints
    List<Transform> _wayPoints = new List<Transform>();
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Light Object" || collision.gameObject.tag == "Heavy Object")
        {
            isTargetHit = true;
            Debug.Log("hit");
        }
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Timer Set
        timer = 0;

        //Get Waypoints
        Transform wayPointsOBJ = GameObject.FindGameObjectWithTag("Waypoints4").transform;
        foreach (Transform t in wayPointsOBJ)
        {
            _wayPoints.Add(t);
        }

        //Get Agent
        agent = animator.GetComponent<NavMeshAgent>();

        //Set First Destination
        agent.SetDestination(_wayPoints[Random.Range(0, _wayPoints.Count)].position);

        //Get Player
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        //Get Layer
        _ground = LayerMask.GetMask("Ground");

        //Get cube trigger
        cube = animator.transform.GetChild(1).gameObject;
        //Activate cube
        cube.gameObject.SetActive(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Move The Agent Between Waypoints Randomly
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(_wayPoints[Random.Range(0, _wayPoints.Count)].position);
        }

        //Time Counter Till Switch To Idle
        timer += Time.deltaTime;
        if (timer > 10)
        {
            animator.SetBool("Patrolling", false);
        }

        //Distance Check For Chasing
        float distance = Vector3.Distance(animator.transform.position, _player.position);
        if(distance < _chaseRange)
        {
            //Check For Line Of Sight With Raycast
            RaycastHit hit;
            if (Physics.Raycast(animator.transform.position, (_player.position - animator.transform.position), out hit, _chaseRange))
            {
                if (hit.collider.tag == "Player")
                {
                    _lineOfSight = true;
                }
            }

            //Switch To Chasing If Line Of Sight And Distance Are Met
            if (_lineOfSight == true)
            {
                animator.SetBool("Chasing", true);
            }
            //Checks if the enemy is hit 
            if (isTargetHit)
            {
                animator.SetBool("IsDead", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}
