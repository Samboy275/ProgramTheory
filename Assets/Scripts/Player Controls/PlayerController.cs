
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : IDamagable
{
    // game objects
    [SerializeField] private TextMeshProUGUI playerText;
    [SerializeField] private Camera MainCam;
    [SerializeField] private Transform RifleHolder;
    // control variables
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 0.6f;
    [SerializeField] private List <FireArm> guns;
    [SerializeField] private FireArm currentGun;
    private List<GameObject> bombs;
    [SerializeField] private int bombsLimit;
    private float velocity;
    //private float xLimit = 8.5f;
    // components
    private Animator anim;
    [SerializeField] LayerMask groundMask;
    private void Awake()
    {
        bombs = new List<GameObject>();
        guns = new List<FireArm>();
        guns.Add(GetComponentInChildren<FireArm>());
        currentGun = guns[0];
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
            if (currentGun.CurrentGunType() == FireArm.GunType.automatic)
            {
                if (Input.GetButton("Fire1"))
                {
                    currentGun.Shoot();
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    currentGun.Shoot();
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && bombs.Count > 0)
            {
                bombs[0].GetComponent<BombPickup>().SetBombPosition(transform.position);
                bombs[0].SetActive(true);
                bombs[0].GetComponent<Bomb>().StartTimer();
                bombs.RemoveAt(0);
            }
            Movement();
            Aim();
            if (guns.Count > 0)
            {
                
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    currentGun = guns[0];
                    guns[0].gameObject.SetActive(true);
                    guns[1].gameObject.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    guns[0].gameObject.SetActive(false);
                    guns[1].gameObject.SetActive(true);
                    currentGun = guns[1];
                }
            }
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
            GameManager._Instance.PlayerDied();   
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


    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "DeathFall")
        {
            TakeDamage(hp);
        }
    }


    public void GetWeapon(GameObject weap)
    {
        weap.transform.SetParent(RifleHolder);
        guns.Add(weap.GetComponent<FireArm>());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<FireArm>() != null)
        {
            if (guns.Count > 1)
            {
                playerText.text = "Press E to Refill ammo";
            }
            else
            {
                playerText.text = "Press E to buy the automatic rifle";
                Debug.Log(other.name);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("found");
                    GameObject rifle = other.GetComponent<BuyPickup>().GetItem();
                    rifle.transform.SetParent(RifleHolder);
                    rifle.transform.localPosition = Vector3.zero;
                    rifle.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    rifle.transform.localScale = new Vector3(0.0015f, 0.0015f, 0.0015f);
                    rifle.SetActive(false);
                    guns.Add(rifle.GetComponent<FireArm>());
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            playerText.text = "";
        }
    }
}
