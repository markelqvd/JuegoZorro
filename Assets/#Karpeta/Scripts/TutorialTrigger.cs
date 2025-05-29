using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject panelTutorial;             // Asigna el panel de UI en el inspector
    public Text textoTutorial;        // Asigna el texto dentro del panel
    public string mensajeTutorial;               // El mensaje que quieres mostrar
    public float duracion = 3f;                  // Cuánto tiempo mostrar el mensaje

    private void Start()
    {
        panelTutorial.SetActive(false);          // Asegura que el panel está apagado al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MostrarTutorial());
        }
    }

    private IEnumerator MostrarTutorial()
    {
        panelTutorial.SetActive(true);
        textoTutorial.text = mensajeTutorial;
        yield return new WaitForSeconds(duracion);
        panelTutorial.SetActive(false);
        Destroy(gameObject);
    }
}
