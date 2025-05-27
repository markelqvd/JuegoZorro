using UnityEngine;
using System.Collections;

public class CinematicaInicial : MonoBehaviour
{
    [Header("Cámaras")]
    public Camera camCinematica1;
    public Camera camCinematica2;
    public Camera camGameplay1;
    public Camera camGameplay2;

    [Header("Movimiento entre puntos")]
    public Transform puntoA; // Inicio de la cinemática
    public Transform puntoB; // Fin de la cinemática
    public float duracion = 3f;

    [Header("Control del menú")]
    public MenuManager menuManager;

    void Start()
    {
        // Activar cámaras de cinemática, desactivar cámaras de gameplay
        camCinematica1.gameObject.SetActive(true);
        camCinematica2.gameObject.SetActive(true);
        camGameplay1.gameObject.SetActive(false);
        camGameplay2.gameObject.SetActive(false);

        // Colocar cámaras en puntoA
        camCinematica1.transform.position = puntoA.position;
        camCinematica2.transform.position = puntoA.position;
    }

    public void IniciarCinematica()
    {
        StartCoroutine(MoverCamarasEntrePuntos());
    }

    private IEnumerator MoverCamarasEntrePuntos()
    {
        Vector3 origen = puntoA.position;
        Vector3 destino = puntoB.position;

        float t = 0f;
        while (t < duracion)
        {
            float factor = t / duracion;
            camCinematica1.transform.position = Vector3.Lerp(origen, destino, factor);
            camCinematica2.transform.position = Vector3.Lerp(origen, destino, factor);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        // Asegurar posición exacta al final
        camCinematica1.transform.position = destino;
        camCinematica2.transform.position = destino;

        yield return new WaitForSeconds(0.5f);

        // Cambiar a cámaras de gameplay
        camCinematica1.gameObject.SetActive(false);
        camCinematica2.gameObject.SetActive(false);
        camGameplay1.gameObject.SetActive(true);
        camGameplay2.gameObject.SetActive(true);

        menuManager.ComenzarJuego(); // Reanuda el juego
    }
}
