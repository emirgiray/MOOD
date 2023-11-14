using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : StateMachineBehaviour
{
    //Reference Values
    float timer;
    float _chaseRange = 15;
    bool _lineOfSight = false;
    public bool isTargetHit = false;
    //References
    GameObject cube;
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
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Timer Set
        timer = 0;

        //Get Player
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        //Get Layer
        _ground = LayerMask.GetMask("Ground");

        //Get cube trigger
        cube = animator.transform.GetChild(1).gameObject;
        //Deactivate cube
        cube.gameObject.SetActive(false);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Look At Player
        animator.transform.LookAt(_player);

        //Time Counter Till Switch To Patrolling
        timer += Time.deltaTime;
        if (timer > 5)
        {
            animator.SetBool("Patrolling", true);
        }

        //Distance Check For Chasing
        float distance = Vector3.Distance(animator.transform.position, _player.position);
        if (distance < _chaseRange)
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

     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    //OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
         //Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
