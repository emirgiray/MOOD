using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : StateMachineBehaviour
{
    //Reference Values
    float _attackRange = 15;
    bool _lineOfSight = false;
    public bool isTargetHit = false;
    //References
    GameObject cube;
    NavMeshAgent agent;
    Transform _player;
    LayerMask _ground;
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
        //Get Agent
        agent = animator.GetComponent<NavMeshAgent>();

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
        //Look At Player
        animator.transform.LookAt(_player);

        //Distance Check To Switch From Chase To Attack
        agent.SetDestination(_player.position);
        float distance = Vector3.Distance(animator.transform.position, _player.position);
        if (distance < _attackRange)
        {

            //Check For Line Of Sight With Raycast
            RaycastHit hit;
            if (Physics.Raycast(animator.transform.position, (_player.position - animator.transform.position), out hit, _attackRange))
            {
                if (hit.collider.tag == "Player")
                {
                    _lineOfSight = true;
                }
                
            }
            //Check For Line Of Sight With Raycast
            if (_lineOfSight == true)
            {
                animator.SetBool("Attacking", true);
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
