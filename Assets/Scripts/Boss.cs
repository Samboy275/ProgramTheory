using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Boss : Enemy
{
    // game objects
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform[] firePoints;

    // components
    [SerializeField] BoxCollider attackBox;
    [SerializeField] BoxCollider rageBox;

    // control variables
    private int numofAttacks;
    [SerializeField] private float rotationSpeed;
    private bool firing;

    // enum to check which attack state
    private enum AttackTypes
    {
        normal,
        fireballs,
        Rage
    }

    [SerializeField] private AttackTypes currentAttack;
    protected override void Start()
    {
        attackBox.enabled = false;
        rageBox.enabled = false;
        firing = false;
        numofAttacks = 0;
        currentAttack = AttackTypes.normal;
        base.Start();
    }

    protected override void Update()
    {
        if (!firing)
        {
            switch (currentAttack)
            {
                case AttackTypes.normal:
                    base.Update();
                    break;
                case AttackTypes.fireballs:
                    StartCoroutine(Attack(2));
                    break;
                case AttackTypes.Rage:
                    StartCoroutine(RageAttackTimer());
                    RageAttack();
                    break;
            }
        }
        else
        {
            transform.LookAt(GetPlayerDirection());
        }

        if (!PlayerInRange())
        {
            anim.ResetTrigger("Attack");
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            attackBox.enabled = true;
        }
        else
        {
            attackBox.enabled = false;
        }
    }
    protected override void Attack()
    {
        base.Attack();
    }
    protected IEnumerator Attack(float time)
    {
        ResetSpeed();
        firing = true;
        anim.ResetTrigger("Attack");
        anim.SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        for (int i  = 0; i < 2; i++)
        {
            Instantiate(fireball, firePoints[i].position, firePoints[i].rotation);
        }
        anim.SetLayerWeight(1, 0);
        currentAttack = AttackTypes.normal;
        firing = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("attacked");
            player.GetComponent<PlayerController>().TakeDamage(baseDmg);
            attackBox.enabled = false;
        }
    }

    IEnumerator RageAttackTimer()
    {
        if (currentAttack == AttackTypes.Rage)
        {
            yield return null;
        }
        rageBox.enabled = true;
        float accelerationOrigin = acceleration; // saving original acceleration value
        float maxSpeedOrigin = maxSpeed; // saving original speed value'
        maxSpeed = 8;

        yield return new WaitForSeconds(5);

        acceleration = accelerationOrigin;
        maxSpeed = 5;
        anim.SetBool("Rage", false);
        rageBox.enabled = false;    
        currentAttack = AttackTypes.normal;
       
    }
    private void RageAttack()
    {
        if (!PlayerInRange())
        {
            MoveTowardsPlayer();
        }
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Projectile"))
        {
            if (currentAttack != AttackTypes.Rage)
            {
                ChargeFireBall();
            }
            attackBox.enabled = false;
        }
    }
    private void ChargeFireBall()
    {
        numofAttacks++;
        if (numofAttacks >= 3)
        {
            numofAttacks = 0;
            currentAttack = AttackTypes.fireballs;
        }
    }

    // POLYMORPHISM
    public override void TakeDamage(int amount = 1)
    {
        if (currentAttack != AttackTypes.Rage)
        {
            base.TakeDamage(amount);
            if (isDead)
            {
                GameManager._Instance.EndBossFight();
            }

            if (hp == (maxHp / 2) || hp == (maxHp / 4) || hp == (maxHp / 8))
            {
                anim.ResetTrigger("Attack");
                currentAttack = AttackTypes.Rage;
                StartCoroutine(RageAttackTimer());
                anim.SetBool("Rage", true);
            }
        }
    }
}
