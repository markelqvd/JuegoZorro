using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hoguera : MonoBehaviour
{
    public GameObject panelTeclaE;  // UI que muestra "Presiona E"
    public GameObject fuego;         // Hoguera encendida (partículas)
    public Animator fadeAnimator;    // Animator del fundido a negro
    private bool puedeEncender = false;
    private bool encendida = false;

    void Start()
    {
        panelTeclaE.SetActive(false);
        fuego.SetActive(false); // Hoguera empieza apagada
    }

    void Update()
    {
        if (puedeEncender && Input.GetKeyDown(KeyCode.E) && !encendida)
        {
            StartCoroutine(EncenderHoguera());
        }
    }

    private IEnumerator EncenderHoguera()
    {
        encendida = true;
        fadeAnimator.SetTrigger("Fundido"); // Activa la animación de fundido

        yield return new WaitForSeconds(1f); // Espera 1 segundo

        fuego.SetActive(true); // Enciende la hoguera
        GuardarCheckpoint();   // Guarda la posición

        yield return new WaitForSeconds(1f); // Espera 1 segundo más
        fadeAnimator.SetTrigger("Desfundido"); // Desactiva el fundido
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
        // Guarda el checkpoint en una posición específica
        Vector3 posicionCheckpoint = new Vector3(transform.position.x - 0.135f, transform.position.y + 0.101f, transform.position.z + 0.466f); // Ajusta la posición
        PlayerPrefs.SetFloat("CheckpointX", posicionCheckpoint.x);
        PlayerPrefs.SetFloat("CheckpointY", posicionCheckpoint.y);
        PlayerPrefs.SetFloat("CheckpointZ", posicionCheckpoint.z);
        PlayerPrefs.Save();
        Debug.Log("Checkpoint guardado en: " + posicionCheckpoint);
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
