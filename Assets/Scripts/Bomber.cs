using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE 
public class Bomber : Enemy
{

    // game objects
    [SerializeField] private ParticleSystem explosion;
    private Bomb bomb;
    // control variables
    [SerializeField] private float bombTime;
    [SerializeField] private float startBombRange;
    [SerializeField] private float explosionRadius;
    private bool isTicking;

    protected override void Start()
    {
        isTicking = false;
        bomb = GetComponentInChildren<Bomb>();
        base.Start();
    }

    protected override void Update()
    {
        if (!isDead)
        {
            // once bomber is in range start the timer
            if (Vector3.Distance(transform.position, player.position) < startBombRange && !isTicking)
            {
                isTicking = true;
                bomb.StartTimer();
            }
            base.Update();
        }

        
    }

    // POLYMORPHISM
    protected override void FollowPlayer()
    {
        if (PlayerInRange())
        {
            anim.SetBool("Bombing", true);
        }
        base.FollowPlayer();
    }

    // POLYMORPHISM
    protected override void Attack()
    {
        bomb.Explode();
    }

    public void Die()
    {
        anim.SetBool("Bombing", true);
        TakeDamage(hp);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage();
        
        if (isDead && !isTicking)
        {
            // drop a bomb for the player to pick up
            PickUp pickup =  GetComponentInChildren<PickUp>();
            pickup.enabled = true;
            pickup.Drop();
        }
    }

    private void OnDestroy()
    {

    }

}
