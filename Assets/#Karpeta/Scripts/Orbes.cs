using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbes : MonoBehaviour
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