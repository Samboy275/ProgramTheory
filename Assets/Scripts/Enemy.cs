using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Enemy : IDamagable
{
    // game objects
    [SerializeField] protected Transform player;


    // components
    [SerializeField] protected Animator anim;

    // control variables
    [SerializeField] protected float attackRange;
    private float velocity;
    [SerializeField] protected float acceleration;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected int baseDmg;
    [SerializeField] private int points;
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
            float speedMapping = Mathf.Clamp01(velocity);
            anim.SetFloat("Speed", speedMapping);
        }
    }


    // ABSTRACTION
    protected virtual void FollowPlayer()
    {
        // ABSTRACTION
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
            
            
            transform.position = Vector3.MoveTowards(transform.position, player.position, velocity *Time.deltaTime);
    }

    // ABSTRACTION
    protected bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }
    protected virtual void Attack()
    {
        anim.SetTrigger("Attack");
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
            GameManager._Instance.CheckEnemiesRemaining(points);
            if (GetComponent<Bomber>() == null && GetComponent<Boss>() == null)
            {
                SpawnManager.Instance.SpawnHpPickUp(transform.position);
            }
            else if (GetComponent<Boss>() != null)
            {
                int playerMxHp = player.GetComponent<PlayerController>().GetMaxHp();
                SpawnManager.Instance.SpawnHpPickUp(transform.position, playerMxHp);
            }
            Destroy(gameObject, 4f);
        }
    }
    protected void ResetSpeed()
    {
        velocity = 0;
    }

    void OnMouseEnter()
    {
        if (!isDead)
        {
            CursorManager.instance.SetCursor();
        }
    }

    void OnMouseExit()
    {
        CursorManager.instance.SetDefaultCursor();
    }
}
