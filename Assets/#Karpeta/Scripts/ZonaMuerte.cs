using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaMuerte : MonoBehaviour
{
    // Posición inicial para el respawn
    public Vector3 puntoInicial = new Vector3(0f, 1f, 0f); // Ajusta esto a tus coordenadas iniciales

    void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que colisiona tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Llama a la función para "morir" y respawnear al jugador
            Morir(other.transform);
        }
    }

    private void Morir(Transform jugador)
    {
        // Verifica si hay un checkpoint guardado
        if (PlayerPrefs.HasKey("CheckpointX"))
        {
            // Cargar las coordenadas del checkpoint
            float x = PlayerPrefs.GetFloat("CheckpointX");
            float y = PlayerPrefs.GetFloat("CheckpointY");
            float z = PlayerPrefs.GetFloat("CheckpointZ");
            jugador.position = new Vector3(x, y, z); // Respawn en el checkpoint
        }
        else
        {
            // Respawn en el punto inicial
            jugador.position = puntoInicial; // Respawn en la posición inicial
        }

        // Aquí puedes agregar lógica de muerte, como desactivar el jugador, si lo deseas
        Debug.Log("Jugador ha muerto. Respawn en: " + jugador.position);
    }
}
