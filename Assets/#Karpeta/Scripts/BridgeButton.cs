using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeButton : MonoBehaviour
{
    public BridgeBlockController bridgeToActivate;
    public CameraFollow cameraFollow;          // Referencia a tu script CameraFollow
    public Transform puntoDeFoco;              // El foco temporal sobre el puente

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bridgeToActivate.ActivateBridge();

            if (cameraFollow != null && puntoDeFoco != null)
            {
                cameraFollow.focoTemporal = puntoDeFoco;
                cameraFollow.EnfocarTemporalmente();
            }
        }
    }
}

