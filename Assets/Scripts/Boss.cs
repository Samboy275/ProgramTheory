using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    // game objects
    [SerializeField] private GameObject fireball;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Attack(3));
    }

    protected override void Attack()
    {

    }
    protected IEnumerator Attack(float time)
    {
        yield return new WaitForSeconds(3);
        anim.SetTrigger("Attack");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Smashing");
    }
}
