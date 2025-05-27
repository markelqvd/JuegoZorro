using UnityEngine;
using System.Collections;

public class CinematicaInicial : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public Transform puntoInicio;

    public Transform jugador; // Asigna el jugador aquí
    public float offsetZ = -10f; // Para cámara ortográfica
    public float duracion = 3f;

    public CameraFollow cameraFollow; // Script que sigue al jugador
    public MenuManager menuManager;

    private bool yaEjecutado = false;

    void Start()
    {
        cameraFollow.enabled = false;

        if (camera1 != null) camera1.transform.position = puntoInicio.position;
        if (camera2 != null) camera2.transform.position = puntoInicio.position;
    }

    public void IniciarCinematica()
    {
        if (!yaEjecutado)
        {
            yaEjecutado = true;
            StartCoroutine(MoverCamaras());
        }
    }

    private IEnumerator MoverCamaras()
    {
        // Posición final detrás del jugador
        Vector3 destino = new Vector3(jugador.position.x, jugador.position.y, offsetZ);

        Vector3 origen1 = camera1 != null ? camera1.transform.position : Vector3.zero;
        Vector3 origen2 = camera2 != null ? camera2.transform.position : Vector3.zero;

        float t = 0f;
        while (t < duracion)
        {
            float factor = t / duracion;

            if (camera1 != null)
                camera1.transform.position = Vector3.Lerp(origen1, destino, factor);

            if (camera2 != null)
                camera2.transform.position = Vector3.Lerp(origen2, destino, factor);

            t += Time.unscaledDeltaTime;
            yield return null;
        }

        if (camera1 != null)
            camera1.transform.position = destino;

        if (camera2 != null)
            camera2.transform.position = destino;

        // Activar el seguimiento del jugador
        if (cameraFollow != null)
            cameraFollow.enabled = true;

        menuManager.ComenzarJuego();
    }
}
