using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IDamagable
{
    // game objects
    [SerializeField] private GameObject fireBall;
    [SerializeField] private Camera MainCam;
    [SerializeField] private TrailRenderer bulletTrail;
    // control variables
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 0.6f;
    [SerializeField] private Weapon gun;
    private float velocity;

    // components
    private Animator anim;
    [SerializeField] LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponentInChildren<Weapon>();
        velocity = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gun.Shoot();
        }
        Movement();
        Aim();
    }


    // ABSTRACTION
    private void Movement() // moving player object based on input
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (velocity < maxSpeed)
            {
                velocity += acceleration * Time.deltaTime;
            } 
            else
            {
                velocity = maxSpeed;
            }
        }
        else
        {
            velocity = 0;
        }
        
        float speedMapping = Mathf.Clamp01(velocity);
        anim.SetFloat("Speed", speedMapping, 0.1f, Time.deltaTime);
        transform.position += transform.forward * velocity * Time.deltaTime;
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            return (success: true, position: hitInfo.point);
        }
        else
        {
            return (success: false, position: Vector3.zero);
        }
    }


    // ABSTRACTION
    private void Aim() // make the player face the mouse position
    {
        var (success, position) = GetMousePosition();

        if (success)
        {
            Vector3 direction = position - transform.position;
            direction.y = 0;
            transform.forward = direction;
        }
    }


    // POLYMORPHISM
    override public void TakeDamage(int amount = 1)
    {
        base.TakeDamage(amount);
        if (hp <= 0)
            Debug.Log("Game Over");
    }
}
