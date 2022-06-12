using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    // game objects
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform[] firePoints;

    // components
    [SerializeField] BoxCollider attackBox;

    // control variables
    private int numofAttacks;
    private bool firing;

    // enum to check which attack state
    private enum AttackTypes
    {
        normal,
        fireballs
    }

    private AttackTypes currentAttack;
    protected override void Start()
    {
        attackBox.enabled = false;
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
            player.GetComponent<PlayerController>().TakeDamage(baseDmg);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Projectile"))
        {
            ChargeFireBall();
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
}
