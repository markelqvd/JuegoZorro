using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Jugador
    public Vector3 offset;
    public GameObject particulasEscultura1;

    public Transform focoTemporal; // Objeto a enfocar (puede ser un empty)
    public float duracionMovimiento = 1.5f;
    public float tiempoEspera = 2f;

    private bool siguiendoJugador = true;
    private Vector3 posicionOriginal;

    private void Start()
    {
        posicionOriginal = transform.position;
    }

    private void LateUpdate()
    {
        if (siguiendoJugador && target != null)
        {
            transform.position = target.position + offset;
        }
    }

    public void EnfocarTemporalmente()
    {
        if (!siguiendoJugador)
            return;

        StartCoroutine(MoverACentroDeInteres());
    }

    private IEnumerator MoverACentroDeInteres()
    {
        siguiendoJugador = false;

        // Guardar posición actual
        Vector3 desdePos = transform.position;
        Vector3 hastaPos = focoTemporal.position + offset;
        Quaternion rotacionFija = transform.rotation;

        // Mover a zona del objeto (sin rotar)
        yield return StartCoroutine(Mover(transform, desdePos, hastaPos, rotacionFija));

        if (particulasEscultura1 != null)
            particulasEscultura1.SetActive(true);

        // Esperar
        yield return new WaitForSeconds(tiempoEspera);

        // Volver al jugador
        Vector3 regresoPos = target.position + offset;
        yield return StartCoroutine(Mover(transform, transform.position, regresoPos, rotacionFija));

        siguiendoJugador = true;
    }

    private IEnumerator Mover(Transform camara, Vector3 desdePos, Vector3 hastaPos, Quaternion rotacionFija)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duracionMovimiento;
            camara.position = Vector3.Lerp(desdePos, hastaPos, t);
            camara.rotation = rotacionFija; // Mantener la rotación isométrica
            yield return null;
        }
    }
}
