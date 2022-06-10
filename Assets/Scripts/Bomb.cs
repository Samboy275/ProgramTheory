using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Bomb : Weapon
{
    [SerializeField] protected float timeToExplode;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem sparks;
    [SerializeField] private float explosionRadius;
    [SerializeField] private MeshRenderer mesh;
    protected bool exploded;
    private bool ticking = false;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] 

    protected virtual void Start()
    {
        exploded = false;
    }

    protected virtual void Update()
    {
        if (ticking)
        {
            TimerTicking();
            timerText.text = Mathf.RoundToInt(timeToExplode).ToString();
            
        }
    }

    protected void TimerTicking()
    {
        timeToExplode -= Time.deltaTime;
            if (timeToExplode <= 0)
            {
                Explode();
            }
    }
    public virtual void StartTimer()
    {
        ticking = true;
        sparks.Play();
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
                    chars.transform.root.GetComponent<IDamagable>().TakeDamage(dmgAmount);
                }
            }
            else if (chars.transform.root.tag == "Enemy" || chars.transform.root.tag == "Bomber")
            {
                chars.transform.root.GetComponent<IDamagable>().TakeDamage(dmgAmount);
            }
        }
        mesh.enabled = false;
        Destroy(gameObject, 0.5f);
    }



    public void RemoveParent()
    {
        transform.SetParent(null);
    }
}
