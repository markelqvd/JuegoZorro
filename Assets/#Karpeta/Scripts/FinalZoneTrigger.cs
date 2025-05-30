using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalZoneTrigger : MonoBehaviour
{
    public GameObject panelTecla;         // Panel que muestra la tecla "E"
    public GameObject creditos;           // Panel o sistema de créditos (Scroll View, animación, etc.)
    public GameObject camara;             // Cámara que se moverá (opcional)
    public MonoBehaviour controlJugador;  // Referencia al script de control del jugador (opcional)

    public CamerasPathFollower camerasPathFollower;

    private bool enZonaFinal = false;
    private bool creditosIniciados = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panelTecla.SetActive(true);
            enZonaFinal = true;
        }
    }

    private void Update()
    {
        if (enZonaFinal && !creditosIniciados && Input.GetKeyDown(KeyCode.E))
        {
            creditosIniciados = true;
            panelTecla.SetActive(false);
            creditos.SetActive(true);
            camerasPathFollower.IniciarMovimiento();
            /*if (camara != null)
            {
                camara.SetActive(true); // Por si el movimiento de cámara está en otro objeto habilitable
            }*/

            if (controlJugador != null)
            {
                controlJugador.enabled = false; // Desactiva control del jugador si se asigna
            }
        }
    }
}
