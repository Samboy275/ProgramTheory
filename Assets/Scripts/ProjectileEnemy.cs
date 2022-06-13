using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class ProjectileEnemy : Enemy
{
    // game objects
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePosition;


    // control variables
    [SerializeField] private float fireRate;
    private float timeSinceLastBullet = 0;
    override protected void Update()
    {
        base.Update();
    }
    protected override void FollowPlayer()
    {
        base.FollowPlayer();
    }

    // POLYMORPHISM
    protected override void Attack()
    {
        if (Time.time > timeSinceLastBullet)
            {
                timeSinceLastBullet = Time.time + 1f / fireRate;
                GameObject shootedProjectile = Instantiate(projectile, firePosition.position, firePosition.rotation);
            }
    }


    public override void TakeDamage(int amount = 1)
    {
        if (amount >= hp)
        {
            anim.SetLayerWeight(1, 0);
        }
        base.TakeDamage(amount);
    }

}
