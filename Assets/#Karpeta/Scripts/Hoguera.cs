using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hoguera : MonoBehaviour
{
    public GameObject panelTeclaE;   // UI que muestra "Presiona E"
    public GameObject fuego;          // Hoguera encendida (partículas)
    public Animator fadeAnimator;     // Animator del fundido a negro
    public GameObject textoGuardando;
    public GameObject rueda;

    private bool puedeEncender = false;
    private bool encendida = false;

    void Start()
    {
        panelTeclaE.SetActive(false);
        fuego.SetActive(false); // La hoguera comienza apagada
        textoGuardando.SetActive(false);
        rueda.SetActive(false);
    }

    void Update()
    {
        // Si ya está encendida, ocultamos la UI aunque el jugador esté en el trigger
        if (encendida)
            panelTeclaE.SetActive(false);

        if (puedeEncender && Input.GetKeyDown(KeyCode.E) && !encendida)
        {
            StartCoroutine(EncenderHoguera());
        }
    }

    private IEnumerator EncenderHoguera()
    {
        encendida = true;

        // Inmovilizamos al jugador durante el proceso de guardado
        DesactivarMovimientoJugador();

        // Apagamos las demás hogueras activas
        ApagarOtrasHogueras();

        // Iniciamos el fundido a negro
        fadeAnimator.SetTrigger("Fundido");

        yield return new WaitForSeconds(1f);

        textoGuardando.SetActive(true);
        rueda.SetActive(true);

        // Espera para que el fundido se ejecute (1.5 segundos)
        yield return new WaitForSeconds(1.5f);

        fuego.SetActive(true); // Enciende la hoguera
        GuardarCheckpoint();   // Guarda la posición

        // Espera el tiempo restante del fundido (1.5 segundos)
        yield return new WaitForSeconds(1.5f);

        textoGuardando.SetActive(false);
        rueda.SetActive(false);
        fadeAnimator.SetTrigger("Desfundido");

        // Vuelve a habilitar el movimiento del jugador
        ActivarMovimientoJugador();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Si la hoguera aún no está encendida se muestra la UI
            if (!encendida)
                panelTeclaE.SetActive(true);
            puedeEncender = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panelTeclaE.SetActive(false);
            puedeEncender = false;
        }
    }

    void GuardarCheckpoint()
    {
        // Guarda el checkpoint en una posición específica (ajusta según necesites)
        Vector3 posicionCheckpoint = new Vector3(transform.position.x - 0.135f, transform.position.y + 0.101f, transform.position.z + 0.466f);
        PlayerPrefs.SetFloat("CheckpointX", posicionCheckpoint.x);
        PlayerPrefs.SetFloat("CheckpointY", posicionCheckpoint.y);
        PlayerPrefs.SetFloat("CheckpointZ", posicionCheckpoint.z);
        PlayerPrefs.Save();
        Debug.Log("Checkpoint guardado en: " + posicionCheckpoint);
    }

    // Método público para apagar esta hoguera (llamado desde otras hogueras)
    public void ApagarHoguera()
    {
        encendida = false;
        fuego.SetActive(false);
    }

    // Apaga todas las hogueras encendidas que no sean esta
    private void ApagarOtrasHogueras()
    {
        Hoguera[] hogueras = FindObjectsOfType<Hoguera>();
        foreach (Hoguera h in hogueras)
        {
            if (h != this && h.estaEncendida())
            {
                h.ApagarHoguera();
            }
        }
    }

    // Método para consultar si la hoguera está encendida
    public bool estaEncendida()
    {
        return encendida;
    }

    // Método para desactivar el movimiento del jugador
    private void DesactivarMovimientoJugador()
    {
        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
        {
            // Se asume que el script de movimiento se llama "PlayerController"
            PlayerController control = jugador.GetComponent<PlayerController>();
            if (control != null)
            {
                control.enabled = false;
            }
        }
    }

    // Método para reactivar el movimiento del jugador
    private void ActivarMovimientoJugador()
    {
        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
        {
            PlayerController control = jugador.GetComponent<PlayerController>();
            if (control != null)
            {
                control.enabled = true;
            }
        }
    }

    void OnDestroy()
    {
        // Borra el checkpoint al detener el juego
        PlayerPrefs.DeleteKey("CheckpointX");
        PlayerPrefs.DeleteKey("CheckpointY");
        PlayerPrefs.DeleteKey("CheckpointZ");
        Debug.Log("Checkpoints borrados al salir del juego.");
    }
}
