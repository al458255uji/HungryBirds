using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class MenuGameOver : MonoBehaviour
{
    [Header("Configuración de Audio")]
    public AudioSource soundSource;

    public void VolverAlInicio()
    {
        StartCoroutine(WaitAndLoadMenu());
    }

    public void SalirDelJuego()
    {
        StartCoroutine(WaitAndExit());
    }

    IEnumerator WaitAndLoadMenu()
    {
        Debug.Log("Reproduciendo sonido y volviendo al menú de inicio...");

        if (soundSource != null)
        {
            soundSource.Play();
        }

        yield return new WaitForSeconds(0.15f);

        SceneManager.LoadScene("Menu");
    }

    IEnumerator WaitAndExit()
    {
        Debug.Log("¡Botón Exit (GameOver) pulsado! Reproduciendo sonido y cerrando...");

        if (soundSource != null)
        {
            soundSource.Play();
        }

        yield return new WaitForSeconds(0.15f);
        //Medida de seguridad para que el boton exit funcione siempre, incluso en el editor de Unity
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
    }
}