using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public Transform focoTemporal;
    public GameObject particulasEscultura;
    public float duracionMovimiento = 1f;
    public float tiempoEspera = 2f;

    [HideInInspector]
    public bool camaraOcupada = false;

    void Start()
    {
        // Desactivar para no interferir en la cinemática
        this.enabled = false;
    }

    private void LateUpdate()
    {
        if (!camaraOcupada)
            transform.position = target.position + offset;
    }

    public void EnfocarTemporalmente()
    {
        StartCoroutine(MoverCamaraTemporal());
    }

    private IEnumerator MoverCamaraTemporal()
    {
        camaraOcupada = true;

        Vector3 origen = transform.position;
        Vector3 destino = focoTemporal.position + offset;

        float t = 0;
        while (t < duracionMovimiento)
        {
            t += Time.deltaTime;
            float lerpFactor = t / duracionMovimiento;
            transform.position = Vector3.Lerp(origen, destino, lerpFactor);
            yield return null;
        }

        if (particulasEscultura != null)
            particulasEscultura.SetActive(true);

        yield return new WaitForSeconds(tiempoEspera);

        t = 0;
        while (t < duracionMovimiento)
        {
            t += Time.deltaTime;
            float lerpFactor = t / duracionMovimiento;
            transform.position = Vector3.Lerp(destino, target.position + offset, lerpFactor);
            yield return null;
        }

        camaraOcupada = false;
    }
}

