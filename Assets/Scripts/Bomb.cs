using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Weapon
{
    [SerializeField] private float timeToExplode;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private float explosionRadius;
    //[SerializeField] private bool isChild;
    [SerializeField] private MeshRenderer mesh;
    protected bool exploded;
    private bool ticking = false;
    

    protected virtual void Start()
    {
        exploded = false;
    }

    protected virtual void Update()
    {
        if (ticking)
        {
            timeToExplode -= Time.deltaTime;

            if (timeToExplode <= 0)
            {
                Explode();
            }
        }
    }

    public void StartTimer()
    {
        Debug.Log("This has been called for " + gameObject.name);
        ticking = true;
        Debug.Log(ticking);
    }

    public void Explode() // create explosion and apply damage to objects in radius
    {
        if (exploded)
        {
            return;
        }
        exploded = true;
        ticking = false;
        explosion.Play();
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        bool playerFound = false;
        foreach (Collider chars in colliders)
        {
            if (chars.transform.root.tag == "Player")
            {
                if(!playerFound)
                {
                    playerFound = true;
                    Debug.Log(chars.transform.root.tag);
                    chars.transform.root.GetComponent<IDamagable>().TakeDamage(dmgAmount);
                }
            }
            else if (chars.transform.root.tag == "Enemy" || chars.transform.root.tag == "Bomber")
            {
                chars.transform.root.GetComponent<IDamagable>().TakeDamage(dmgAmount);
            }
        }
        mesh.enabled = false;
        Destroy(gameObject, 1f);
    }



    public void RemoveParent()
    {
        transform.SetParent(null);
    }
}
