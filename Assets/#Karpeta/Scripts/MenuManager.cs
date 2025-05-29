using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menuInicio;
    public GameObject menuOpciones;
    public GameObject menuPausa;
    public GameObject confirmacionSalida;
    public CinematicaInicial cinematicaInicial;

    private bool isPaused = false;

    // NUEVO: Referencias para el checkbox
    public Toggle miToggle;
    public GameObject objetoTarget;

    void Start()
    {
        Time.timeScale = 0f; // Asegurarse de que el juego comienza sin estar pausado
        menuInicio.SetActive(true);
        menuOpciones.SetActive(false);
        menuPausa.SetActive(false);
        confirmacionSalida.SetActive(false);

        // NUEVO: Asignar listener al Toggle
        if (miToggle != null && objetoTarget != null)
        {
            miToggle.onValueChanged.AddListener(OnToggleCambiado);
            objetoTarget.SetActive(miToggle.isOn); // Estado inicial
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuPausa.activeSelf)
            {
                AbrirMenuPausa();
            }
            else
            {
                CerrarMenuPausa();
            }
        }
    }

    public void AbrirMenuPausa()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void CerrarMenuPausa()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ComenzarJuego()
    {
        menuInicio.SetActive(false);
        cinematicaInicial.IniciarCinematica();
        Time.timeScale = 1f;
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

    // NUEVO: Método para manejar el cambio del checkbox
    public void OnToggleCambiado(bool estado)
    {
        if (objetoTarget != null)
            objetoTarget.SetActive(estado);
    }
}
