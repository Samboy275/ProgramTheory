using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // control variables
    private float rotationSpeed = 100f;


    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colliding with " + this.tag);
        Transform player = other.transform.root;
        if (player.tag == "Player")
        {
            // add item to the inventory
            if (this.tag == "Bomb")
            {
                if (!player.GetComponent<PlayerController>().BombsFull())
                {
                    player.GetComponent<PlayerController>().PickUpBomb(transform.gameObject);
                    gameObject.SetActive(false);
                    GetComponent<BoxCollider>().enabled = false;
                    this.enabled = false;
                }
            }
        }
    }


    public void Drop()
    {
        transform.up = Vector3.up;
        transform.GetComponent<Bomb>().RemoveParent();
        GetComponent<BoxCollider>().enabled = true;
        SetBombPosition(transform.position);
    }



    public void SetBombPosition(Vector3 position)
    {
        transform.position =  new Vector3(position.x, 0.7f, position.z);
    }
}
