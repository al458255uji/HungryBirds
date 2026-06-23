using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class MenuGameOver : MonoBehaviour
{
    [Header("Configuración de Audio")]
    public AudioSource soundSource;

    public void VolverAlInicio()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void SalirDelJuego()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

