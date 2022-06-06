using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE 
public class Bomber : Enemy
{

    // game objects
    [SerializeField] private ParticleSystem explosion;
    // control variables
    [SerializeField] private float bombTime;
    [SerializeField] private float startBombRange;
    [SerializeField] private float explosionRadius;
    private bool isTicking;

    protected override void Start()
    {
        isTicking = false;
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
            }
            else if (isTicking)
            {
                BombTimer();
            }
        }

        base.Update();
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
    private void BombTimer()
    {
       bombTime -= Time.deltaTime;

       if (bombTime <= 0)
       {
            Explode("IsDead");
       }   
    }

    // POLYMORPHISM
    protected override void Attack()
    {
        Explode("Bombing");
    }


    private void Explode(string bombingType) // create explosion and apply damage to objects in radius
    {
        Debug.Log(bombingType);
        explosion.Play();
        anim.SetBool(bombingType, true);
        TakeDamage(hp);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        bool playerFound = false;
        foreach (Collider chars in colliders)
        {
            if (chars.transform.root.tag == "Player")
            {
                if(!playerFound)
                {
                    playerFound = true;
                    Debug.Log(chars.transform.root.tag);
                    player.GetComponent<IDamagable>().TakeDamage(3);
                }
            }
            else if (chars.transform.root.tag == "Enemy")
            {
                chars.transform.root.GetComponent<IDamagable>().TakeDamage(3);
            }
        }
    }


    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(transform.position, explosionRadius);
    // }
}
