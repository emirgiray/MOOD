using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI2 : MonoBehaviour
{
    public VisionScript VisionScript;
    //public Behaviour move;
    //public GameObject missile;
    public float stunTime = 1f;
    public float time = 1f;
    public bool hit = false;
    public float stunDuration = 0f;


    public EnemyGun enemyGun;
    public GameObject projectile;
    public float health;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Stun")
        //{
        //    move.enabled = false;
        //    hit = true;

        //}

    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        //if (hit == true)
        //{
        //    Wait();

        //    hit = false;
        //    move.enabled = true;
        //    stunTime = stunDuration;
        //    time = 0;
        //}

        playerInSightRange = VisionScript.canSeePlayer;
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
        if (playerInAttackRange && !playerInSightRange)
        {
            Patroling();
        }
    }
    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            enemyGun.Shoot();
            ////shoot, melee?
            //Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            //alreadyAttacked = true;
            //Invoke(nameof(ResetAttack), timeBetweenAttacks);
            ////Invoke(nameof(DestroyProjectile), 2f); //doesnt work

        }
    }
    public void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //public void TakeDamage(int damage)
    //{
    //    health -= damage;
    //    if (health <= 0)
    //    {
    //        Invoke(nameof(DestroyEnemy), 0.5f);
    //    }
    //}
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        stunDuration = stunTime;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
    }


}