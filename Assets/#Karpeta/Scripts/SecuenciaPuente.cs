using System.Collections;
using UnityEngine;

public class SecuenciaPuente : MonoBehaviour
{
    public GameObject objeto2;
    public GameObject objeto4;

    public Animator palanca1Animator;
    public Animator palanca2Animator;

    public Animator puenteParte1;
    public Animator puenteParte2;
    public Animator puenteParte3;

    public CameraFollow camaraFollow;
    public Transform focoPuente;

    public float delayEntrePartes = 0.5f; // Tiempo entre partes del puente

    private bool secuenciaIniciada = false;

    void Update()
    {
        if (!secuenciaIniciada && objeto2.activeSelf && objeto4.activeSelf)
        {
            secuenciaIniciada = true;
            StartCoroutine(IniciarSecuencia());
        }
    }

    private IEnumerator IniciarSecuencia()
    {
        // 1. Animar las palancas
        if (palanca1Animator) palanca1Animator.SetTrigger("Girar");
        if (palanca2Animator) palanca2Animator.SetTrigger("Girar");

        yield return new WaitForSeconds(2f); // Espera tras las palancas

        // Esperar hasta que la cámara esté libre
        while (camaraFollow.camaraOcupada)
        {
            yield return null;
        }

        camaraFollow.tiempoEspera = 3f;

        // 2. Mover cámara al puente
        camaraFollow.focoTemporal = focoPuente;
        camaraFollow.particulasEscultura = null;
        camaraFollow.EnfocarTemporalmente();

        // Esperar mientras la cámara se mueve
        yield return new WaitForSeconds(camaraFollow.duracionMovimiento + 0.5f);

        // 3. Activar las partes del puente una por una
        if (puenteParte1) puenteParte1.SetTrigger("Subir");
        yield return new WaitForSeconds(delayEntrePartes);

        if (puenteParte2) puenteParte2.SetTrigger("Subir");
        yield return new WaitForSeconds(delayEntrePartes);

        if (puenteParte3) puenteParte3.SetTrigger("Subir");

        // Esperar un poco más antes de volver la cámara
        yield return new WaitForSeconds(3f);
    }
}
