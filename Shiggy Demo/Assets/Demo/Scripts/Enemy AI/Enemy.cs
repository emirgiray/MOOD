using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //References
    public GameObject Bullet;
    public Transform projectilePoint;
    public Transform _enemy;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Update is called once per frame
    public void Shoot()
    {
        GameObject _bullet = Instantiate(Bullet);
        _bullet.transform.position = projectilePoint.position;
        _bullet.transform.rotation = _enemy.rotation;

    }

    private void DestroyProjectile()
    {
        Destroy(Bullet);
    }
}
