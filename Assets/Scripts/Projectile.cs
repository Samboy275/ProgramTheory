using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }


    private void OnCollisionEnter(Collision Other)
    {
        IDamagable refrence = Other.gameObject.GetComponent<IDamagable>();
        if (refrence != null)
        {
            refrence.TakeDamage();
        }
        Destroy(gameObject);
    }
}
