using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPickup : PickUp
{
    [SerializeField] public int price{get; private set;}
    [SerializeField] private GameObject itemPrefab;
    protected override void Start()
    {
        onGround = true;
        price = 100;
    }
    protected override void Update()
    {
    }


    public GameObject GetItem()
    {
        return Instantiate(itemPrefab);
    }

}
