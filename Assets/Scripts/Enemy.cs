using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : IDamagable
{
    // game objects
    [SerializeField] protected Transform player;


    // components
    [SerializeField] protected Animator anim;

    // control variables
    [SerializeField] protected float attackRange;
    private float velocity;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
        anim = GetComponent<Animator>();
    }
    virtual protected void Update()
    {
        if (!isDead)
        {
            FollowPlayer();
        }
    }


    // ABSTRACTION
    protected virtual void FollowPlayer()
    {
        Vector3 direction = GetPlayerDirection();
        transform.LookAt(direction);
        if (!PlayerInRange())
        {
            // follow player if player is not in range
            MoveTowardsPlayer();
        }
        else
        {
            // attck player once player is in range
            Attack();
            velocity = 0;
        }
        float speedMapping = Mathf.Clamp01(velocity);
        anim.SetFloat("Speed", speedMapping);
    }

    // ABSTRACTION
    protected void MoveTowardsPlayer()
    {
        if (velocity < maxSpeed)
            {
                velocity += acceleration * Time.deltaTime;
            }
            else
            {
                velocity = maxSpeed;
            }
            
            
            transform.position += transform.forward * velocity * Time.deltaTime;
    }

    // ABSTRACTION
    protected bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }
    protected virtual void Attack()
    {

    }

    // ABSTRACTION
    protected Vector3 GetPlayerDirection()// getting direction of the player
    {
        return new Vector3(player.position.x, 0.5f, player.position.z);
    }


    // POLYMORPHISM
    override public void TakeDamage(int amount = 1)
    {
        base.TakeDamage(amount);
        if (isDead)
        {
            anim.SetBool("IsDead", isDead);
            //Destroy(gameObject, 10);
        }
    }

}
