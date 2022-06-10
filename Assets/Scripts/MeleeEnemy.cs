using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private int dmgAmount;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void Attack()
    {
        anim.SetTrigger("Attack");
        base.Attack();
    }

    protected override void FollowPlayer()
    {
        base.FollowPlayer();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.root.tag == "Player" && anim.GetCurrentAnimatorStateInfo(0).IsName("Armature|Attack"))
        {
            player.GetComponent<PlayerController>().TakeDamage(dmgAmount);
        }
    }
}
