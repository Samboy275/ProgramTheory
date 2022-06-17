using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviorment : MonoBehaviour
{
    //singleton
    public static Enviorment Instance{get; private set;}
    [SerializeField] private BoxCollider backWall;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        backWall.enabled = true;
    }


    public void ActivateBackWall(bool active)
    {
        backWall.enabled = active;
    }
}
