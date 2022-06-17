using UnityEngine;

public class Tunnel : MonoBehaviour
{
    [SerializeField] private Transform camPosition;
    [SerializeField] private Transform ResetPos;
    [SerializeField] private Vector3 originalPos;

    [SerializeField] private BoxCollider enterTrigger;
    [SerializeField] private BoxCollider exitTrigger;
    private bool isEntering;
    void Start()
    {
        originalPos = transform.position;
        isEntering = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isEntering)
            {
                GameObject playerRef = other.transform.root.gameObject;
                playerRef.transform.SetParent(transform);
                transform.position = ResetPos.position;
                Camera.main.transform.position = camPosition.position;
                playerRef.transform.SetParent(null);
                enterTrigger.enabled = false;
                isEntering = false;
                exitTrigger.enabled = true;
                Enviorment.Instance.ActivateBackWall(false);
                GameManager._Instance.LoadNextArea();
            }
            else
            {
                Camera.main.GetComponent<FollowPlayer>().ResetCamPosition();
                transform.position = originalPos;
                isEntering = true;
                enterTrigger.enabled = true;
                exitTrigger.enabled = false;
                Enviorment.Instance.ActivateBackWall(true);
                GameManager._Instance.SpawnNextWave();
            }
        }
    }
}
