using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Weapon
{
    [SerializeField] private float timeToExplode;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private float explosionRadius;
    [SerializeField] private bool isChild;
    private MeshRenderer mesh;
    private bool exploded;
    [SerializeField] private bool ticking = false;
    

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        exploded = false;
    }

    private void Update()
    {
        if (ticking && !exploded)
        {
            timeToExplode -= Time.deltaTime;

            if (timeToExplode <= 0)
            {
                exploded = true;
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
        if (isChild)
        {
            transform.parent.root.GetComponent<Bomber>().Die();
        }
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
            else if (chars.transform.root.tag == "Enemy")
            {
                chars.transform.root.GetComponent<IDamagable>().TakeDamage(dmgAmount);
            }
        }
        mesh.enabled = false;
        Destroy(gameObject, 1f);
    }



    public void RemoveParent()
    {
        isChild = false;
        transform.SetParent(null);
    }
}
