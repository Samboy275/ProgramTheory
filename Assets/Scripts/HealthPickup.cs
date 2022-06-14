using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickUp
{
    [SerializeField] private int hpAmount = 5;


    protected override void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player")
        {
            other.transform.root.GetComponent<IDamagable>().IncreaseHP(hpAmount);
            Debug.Log("Picked Up Hp");
            Destroy(gameObject);
        }
    }


    public void SetHpAmount(int amount)
    {
        hpAmount = amount;
    }
}
