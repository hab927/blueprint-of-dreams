using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used https://www.youtube.com/watch?v=XLCMrguxIs0 as reference with modifications for better customizations
public class TurretControl : MonoBehaviour
{

    Transform _Player;
    float dist;
    public float minDistance;
    public Transform head, barrel;
    public GameObject _projectile;
    public float fireRate, nextFire = 3f;
    public float projectileSpeed = 500f;
    public float destroyProjectile = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(_Player.position, transform.position);
        if(dist <= minDistance)
        {
            head.LookAt(_Player);
            if(Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject clone = Instantiate(_projectile, barrel.position, head.rotation);
        clone.GetComponent<Rigidbody>().AddForce(head.forward * projectileSpeed);
        Destroy(clone, destroyProjectile);
    }
}
