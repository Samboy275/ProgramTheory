    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArm : Weapon
{
    // control variables
    [SerializeField] float fireRate;
    [SerializeField] float nextBulletTime = 0f;


    // game objects
    [SerializeField] Transform firePoint;
    [SerializeField] TrailRenderer bulletTrail;
    [SerializeField] LayerMask enemyMask;

    [SerializeField] Transform wasitRotation;

    public enum GunType {
        automatic,
        manual
    }


    [SerializeField] private GunType gunType;

    private void Update()
    {
        transform.rotation = wasitRotation.rotation;
    }

    public virtual void Shoot()
    {
        
        if (Time.time >= nextBulletTime)
        {
            nextBulletTime = Time.time  + 1f / fireRate;
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 50f, enemyMask))
            {
                    TrailRenderer trail = Instantiate(bulletTrail, firePoint.position, firePoint.rotation);
                    StartCoroutine(SpawnTrail(trail, hit));
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (!hit.transform.root.GetComponent<IDamagable>().Dead())
                    {
                        hit.transform.GetComponent<Enemy>().TakeDamage(dmgAmount);
                    }
                }
            }
        }
    }


    IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit) // spawn bullet trail
    {
        float time = 0;
        Vector3 startPos = trail.transform.position;

        while (time < 0.4f)
        {
            trail.transform.position = Vector3.Lerp(startPos, hit.point, time);
            time += Time.deltaTime / trail.time;
            yield return null; 
        }
        trail.transform.position = hit.point;
        Destroy(trail.transform.gameObject, trail.time);
    }

    public GunType CurrentGunType()
    {
        return gunType;
    }
}
