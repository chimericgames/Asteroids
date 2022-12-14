using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public bool isFiring;
    public BulletController bullet;
    public float bulletSpeed;
    public float timeBetweenShots;
    private float shotCounter;
    public Transform firePoint;

    // Update is called once per frame
    void Update()
    {
        if(isFiring)
        {
            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
                newBullet.speed = bulletSpeed;
                //Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), myRigidbody.GetComponent<Collider>());
            }
        }
        else
        {
            shotCounter = 0;
        }
    }
}
