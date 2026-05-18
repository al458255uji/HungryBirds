using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerPausa : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject objetoMenuPausa;

    [Header("Configuración de Audio")]
    public AudioSource musicaFondo; // <--- ESTA ES LA CASILLA QUE FALTA

    private bool juegoPausado = false;

    void Start()
    {
        if (objetoMenuPausa != null) objetoMenuPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
                Continuar();
            else
                Pausar();
        }
    }

    public void Continuar()
    {
        objetoMenuPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
        // Si hay música puesta, vuelve a sonar al despausar
        if (musicaFondo != null) musicaFondo.UnPause();
    }

    public void Pausar()
    {
        objetoMenuPausa.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;
        // Si hay música puesta, se congela al pausar
        if (musicaFondo != null) musicaFondo.Pause();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void SalirAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); 
    }

    public void SalirDelJuego()
    {
        StartCoroutine(WaitAndExit());
    }
    private System.Collections.IEnumerator WaitAndExit()
    {
        yield return new WaitForSeconds(0.15f); 

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
