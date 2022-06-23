using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class FireBall : Weapon
{
    [SerializeField] private float speed;
    [SerializeField] private float ttl;
    [SerializeField] private float acceleration;
    private Transform playerPos;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        ttl -= Time.deltaTime;

        if (ttl <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        if (playerPos == null)
        {
            Destroy(gameObject);
        }
        transform.LookAt(new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z));
        speed += acceleration * Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IDamagable>().TakeDamage(dmgAmount);
            Destroy(gameObject);
        }  
    }   
}
