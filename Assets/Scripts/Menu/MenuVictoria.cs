using UnityEngine;

public class MenuVictoria : MonoBehaviour
{
    public void SalirDelJuego()
    {
        StartCoroutine(WaitAndExit());
    }

    private System.Collections.IEnumerator WaitAndExit()
    {
        yield return new WaitForSecondsRealtime(0.15f);

        #if UNITY_EDITOR
            // Si estás en el editor de Unity, detiene el Play
            UnityEditor.EditorApplication.isPlaying = false;
        #else
       
        Application.Quit();
        #endif
    }
}