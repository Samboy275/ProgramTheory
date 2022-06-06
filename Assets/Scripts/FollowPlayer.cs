using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform playerPos;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.position + offset; 
    }
}
