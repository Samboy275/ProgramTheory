using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // control variables
    protected float rotationSpeed = 100f;


    protected virtual void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }


    public virtual void Drop()
    {
        
    }

    public void SetDropPosition(Vector3 position)
    {
        transform.position =  new Vector3(position.x, 0.7f, position.z);
    }
}
