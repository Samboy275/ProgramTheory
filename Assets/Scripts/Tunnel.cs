using UnityEngine;

public class Tunnel : MonoBehaviour
{
    [SerializeField] private Transform camPosition;




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.transform.position = camPosition.position;
        }
    }
}
