using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] float threshold;

    [SerializeField] float rotationSpeed;
    private Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (playerPos.position + offset)/2;
        targetPos.x += mousePos.x / 2;
        targetPos.x = Mathf.Clamp(targetPos.x, targetPos.x - threshold, targetPos.x + threshold);
        targetPos.z = Mathf.Clamp(targetPos.z, targetPos.z - threshold, targetPos.z + threshold);
        targetPos.y = transform.position.y;
        transform.position = targetPos;
        
        
    }
}
