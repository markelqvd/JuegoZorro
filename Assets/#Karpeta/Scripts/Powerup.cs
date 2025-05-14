using UnityEngine;

public class DoubleJumpPickup : MonoBehaviour
{
    public GameObject particulasEscultura1;
    public CameraFollow cameraFollow;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.doubleJumpActive = true;
                cameraFollow.particulasEscultura1 = particulasEscultura1;
                cameraFollow.EnfocarTemporalmente();
            }
            Destroy(gameObject);
        }
    }
}
