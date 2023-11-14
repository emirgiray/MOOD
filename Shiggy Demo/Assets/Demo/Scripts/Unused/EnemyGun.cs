using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public EnemyAI2 EnemyAI;
    public GameObject projectile;
    public Transform projectilePoint;
    public Transform _enemy;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot()
    {
            GameObject go = Instantiate(projectile);
            go.transform.position = projectilePoint.position;
            go.transform.rotation = _enemy.rotation;

        Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb.AddForce(transform.up * 4f, ForceMode.Impulse);

        EnemyAI.alreadyAttacked = true;
        Invoke(nameof(ResetAttack), EnemyAI.timeBetweenAttacks);
        //Invoke(nameof(DestroyProjectile), 2f); //doesnt work

    }
    public void ResetAttack()
    {
        EnemyAI.alreadyAttacked = false;
    }
    private void DestroyProjectile()
    {
        Destroy(projectile);
    }
}
