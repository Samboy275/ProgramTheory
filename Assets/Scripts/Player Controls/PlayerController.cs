using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IDamagable
{
    // game objects
    [SerializeField] private Camera MainCam;
    // control variables
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 0.6f;
    [SerializeField] private FireArm gun;
    private List<GameObject> bombs;
    [SerializeField] private int bombsLimit;
    private float velocity;

    // components
    private Animator anim;
    [SerializeField] LayerMask groundMask;
    private void Awake()
    {
        bombs = new List<GameObject>();
        gun = GetComponentInChildren<FireArm>();
        velocity = 0;
        anim = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Input.GetButton("Fire1"))
            {
                gun.Shoot();
            }
            if (Input.GetKeyDown(KeyCode.Space) && bombs.Count > 0)
            {
                bombs[0].GetComponent<PickUp>().SetBombPosition(transform.position);
                bombs[0].SetActive(true);
                bombs[0].GetComponent<Bomb>().StartTimer();
                bombs.RemoveAt(0);
            }
            Movement();
            Aim();
        }
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
        else if (Input.GetKey(KeyCode.S))
        {
            if (velocity > -maxSpeed)
            {
                velocity -= acceleration * Time.deltaTime;
            }
            else
            {
                velocity = -maxSpeed;
            }
        }
        else 
        {
            velocity = 0;
        }
        
        if (velocity > 0)
        {
            anim.SetBool("IsReverse", false);
        }
        else
        {
            anim.SetBool("IsReverse", true);
        }
        float speedMapping = Mathf.Clamp01(velocity >= 0 ? velocity : -velocity);
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
    override public void TakeDamage(int amount = 1  )
    {
        base.TakeDamage(amount);
        if (isDead)
        {
            anim.SetLayerWeight(1, 0);
            anim.SetBool("IsDead", true);
            GameManager._Instance.GameOver();   
        }
    }


    public void PickUpBomb(GameObject bomb)
    {
        bombs.Add(bomb);
    }



    public bool BombsFull()
    {
        if (bombs.Count == bombsLimit)
        {
            return true;
        }
        
        return false;
    }

}
