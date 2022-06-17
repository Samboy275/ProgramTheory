using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 originalPos;
    [SerializeField] private Transform doorPos;
    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            if (transform.position.y < doorPos.TransformPoint(doorPos.position).y)
            {
                transform.position += Vector3.up * 10 * Time.deltaTime;
            }
        }
        else
        {
            if (transform.position.y > originalPos.y)
            {
                transform.position -= Vector3.up * 10 * Time.deltaTime;
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GameManager._Instance.isWaveKilled)
            {
                open = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            open = false;
        }
    }
}
