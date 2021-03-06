using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class MeleeEnemy : Enemy
{
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
            player.GetComponent<PlayerController>().TakeDamage(baseDmg);
        }
    }
}
