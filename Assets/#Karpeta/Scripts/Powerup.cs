using UnityEngine;

public class DoubleJumpPickup : MonoBehaviour
{
    public GameObject particulasEscultura;
    public CameraFollow cameraFollow;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.doubleJumpActive = true;
                cameraFollow.particulasEscultura = particulasEscultura;
                cameraFollow.EnfocarTemporalmente();
            }
            Destroy(gameObject);
        }
    }
}
