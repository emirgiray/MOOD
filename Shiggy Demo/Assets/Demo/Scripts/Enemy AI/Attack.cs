using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : StateMachineBehaviour
{
    //Reference Values
    float timer;
    float _attackRange = 15;
    public bool isTargetHit = false;
    //Private References
    GameObject cube;
    Transform _player;
    Transform _projectileP;
    LayerMask _ground;
    NavMeshAgent agent;
    //Enemy _enemyScp = new Enemy();

    //Public References
    public GameObject Bullet;


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

        //Get Player
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        //Get Layer
        _ground = LayerMask.GetMask("Ground");

        //Get Agent
        agent = animator.GetComponent<NavMeshAgent>();

        //Get Projectile Point
        //_projectileP = GameObject.FindGameObjectWithTag("ProjectilePoint").transform;

        GameObject projectileP = animator.transform.GetChild(0).gameObject;
        _projectileP = projectileP.transform;

        //Get cube trigger
        cube = animator.transform.GetChild(1).gameObject;
        //Deactivate cube
        cube.gameObject.SetActive(false);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Look At Player
        animator.transform.LookAt(_player);

        //Shoot If There Is Line Of Sight
        RaycastHit hit;
        if (Physics.Raycast(animator.transform.position, (_player.position - animator.transform.position), out hit, _attackRange))
        {
            if (hit.collider.tag == "Player")
            {
                //_enemyScp.Shoot();

                timer += Time.deltaTime;
                if (timer > 1)
                {
                    GameObject _bullet = Instantiate(Bullet);
                    _bullet.transform.position = _projectileP.transform.position;
                    _bullet.transform.rotation = _projectileP.transform.rotation;
                    timer = 0;
                }

            }
            else
            {
                animator.SetBool("Attacking", false);
            }
        }      
        

        //Distance Check To Switch From Attack To Chase
        float distance = Vector3.Distance(animator.transform.position, _player.position);
        if (distance > _attackRange)
        {
            animator.SetBool("Attacking", false);
        }

        //Checks if the enemy is hit 
        if (isTargetHit)
        {
            animator.SetBool("IsDead", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
