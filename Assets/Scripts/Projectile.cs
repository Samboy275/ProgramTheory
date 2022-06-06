using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : IDamagable
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }


    private void OnCollisionEnter(Collision Other)
    {
        if (Other.gameObject.CompareTag("Player"))
        {
            Other.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
        Destroy(gameObject);
    }
}
