using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bomb
{
    [SerializeField] private float speed;
    [SerializeField] ParticleSystem engineEffect;
    protected override void Start()
    {
        engineEffect.Play();
        base.Start();
    }

    protected override void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        // ABSTRACTION
        TimerTicking();
    }
    void OnCollisionEnter(Collision other)
    {
        // ABSTRACTION
        Explode();
    }
}
