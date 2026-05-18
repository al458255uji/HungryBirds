using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class MenuLogic : MonoBehaviour
{
    [Header("Configuración de Audio")]
    public AudioSource soundSource; 

    public void StartGame()
    {
        StartCoroutine(WaitAndStart());
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        StartCoroutine(WaitAndExit());
    }

    IEnumerator WaitAndStart()
    {
        if (soundSource != null)
        {
            soundSource.Play();
        }

        yield return new WaitForSeconds(0.15f);

        SceneManager.LoadScene("Day1");
    }

    IEnumerator WaitAndExit()
    {
        if (soundSource != null)
        {
            soundSource.Play();
        }

        yield return new WaitForSeconds(0.15f);

        #if UNITY_EDITOR
            // Si estás jugando desde el editor de Unity, esto detiene el "Play"
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
