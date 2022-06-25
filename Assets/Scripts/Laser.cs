using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private LayerMask masks;
    // Update is called once per frame
    void Update()
    {
        ShootLaser();
    }

    private void ShootLaser()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, masks))
        {
            endPoint.position = hit.point;
        }
    }
}
