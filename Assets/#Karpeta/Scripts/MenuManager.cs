using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menuInicio;
    public GameObject menuOpciones;
    public GameObject menuPausa;
    public GameObject confirmacionSalida;

    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 1f; // Asegurarse de que el juego comienza sin estar pausado
        menuInicio.SetActive(false);
        menuOpciones.SetActive(false);
        menuPausa.SetActive(false);
        confirmacionSalida.SetActive(false);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(menuInicio.GetComponentInChildren<Button>().gameObject);
    
    }

    public void ComenzarJuego()
    {
        Time.timeScale = 1f;
        menuInicio.SetActive(false);
    }

    public void AbrirOpciones()
    {
        menuInicio.SetActive(false);
        menuPausa.SetActive(false);
        menuOpciones.SetActive(true);
    }

    public void CerrarOpciones()
    {
        menuOpciones.SetActive(false);
        if (isPaused)
            menuPausa.SetActive(true);
        else
            menuInicio.SetActive(true);
    }

    public void AbrirConfirmacionSalida()
    {
        confirmacionSalida.SetActive(true);
    }

    public void CerrarConfirmacionSalida()
    {
        confirmacionSalida.SetActive(false);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }

    public void PausarJuego()
    {
        isPaused = true;
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
    }

    public void ReanudarJuego()
    {
        isPaused = false;
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
    }

    public void VolverAlMenuPrincipal()
    {
        AbrirConfirmacionSalida();
    }

    public void ConfirmarSalidaAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
