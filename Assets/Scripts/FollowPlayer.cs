using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] float threshold;

    [SerializeField] float rotationSpeed;
    private Vector3 offset;
    private Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ResetCamPosition()
    {
        transform.position = originalPos;
    }
}
