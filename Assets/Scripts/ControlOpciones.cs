using UnityEngine;
using UnityEngine.UI; 

public class ControlOpciones : MonoBehaviour
{
    [Header("Sliders de UI")]
    public Slider sliderMusica;
    public Slider sliderSFX;

    [Header("Paneles del Menú")]
    public GameObject panelOpciones;

    void Start()
    {
        if (sliderMusica != null)
        {
            sliderMusica.value = PlayerPrefs.GetFloat("VolumenMusica", 0.5f);
            sliderMusica.onValueChanged.AddListener(CambiarVolumenMusica);
        }

        if (sliderSFX != null)
        {
            sliderSFX.value = PlayerPrefs.GetFloat("VolumenSFX", 0.5f);
            sliderSFX.onValueChanged.AddListener(CambiarVolumenSFX);
        }
    }

    public void CambiarVolumenMusica(float valor)
    {
        PlayerPrefs.SetFloat("VolumenMusica", valor);
        AudioSource[] todosLosAudios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audio in todosLosAudios)
        {
            if (audio.loop)
            {
                audio.volume = valor;
            }
        }
    }

    public void CambiarVolumenSFX(float valor)
    {
        PlayerPrefs.SetFloat("VolumenSFX", valor);

        AudioSource[] todosLosAudios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audio in todosLosAudios)
        {
            if (!audio.loop)
            {
                audio.volume = valor;
            }
        }
    }

    public void AbrirOpciones()
    {
        if (panelOpciones != null) panelOpciones.SetActive(true);
    }

    public void CerrarOpciones()
    {
        if (panelOpciones != null) panelOpciones.SetActive(false);
    }
}