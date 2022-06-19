using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPickup : PickUp
{
    [SerializeField] private int price;
    [SerializeField] private GameObject itemPrefab;
    protected override void Start()
    {
        onGround = true;
    }
    protected override void Update()
    {
    }


    public GameObject GetItem()
    {
        return Instantiate(itemPrefab);
    }

}
