using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : PickUp
{
    public void SetBombPosition(Vector3 position)
    {
        transform.position =  new Vector3(position.x, 0.7f, position.z);
    }

    // POLYMORPHISM
    public override void Drop()
    {
        transform.up = Vector3.up;
        transform.GetComponent<Bomb>().RemoveParent();
        GetComponent<BoxCollider>().enabled = true;
        SetDropPosition(transform.position);
        base.Drop();
    }


// POLYMORPHISM
    protected override void OnTriggerEnter(Collider other)
    {
        Transform player = other.transform.root;
        if (player.tag == "Player")
        {
            // add item to the inventory
            if (!player.GetComponent<PlayerController>().BombsFull())
            {
                player.GetComponent<PlayerController>().PickUpBomb(transform.gameObject);
                gameObject.SetActive(false);
                GetComponent<BoxCollider>().enabled = false;
                this.enabled = false;
                base.OnTriggerEnter(other);
            }
            
        }
    } 
}
