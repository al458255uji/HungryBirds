using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float vidaActual = 100f;
    private float vidaMaxima = 100f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource sonidoDano;

    private bool yaEstaMuerto = false;

    [Header("Interfaz (UI)")]
    public Slider sliderBarraVida;

    void Start()
    {
        GameObject objetoSonido = GameObject.Find("DañoJugador");
        if (objetoSonido != null)
        {
            sonidoDano = objetoSonido.GetComponent<AudioSource>();
        }

        vidaActual = vidaMaxima;
        yaEstaMuerto = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (sliderBarraVida != null)
        {
            sliderBarraVida.minValue = 0;
            sliderBarraVida.maxValue = vidaMaxima;
            sliderBarraVida.value = vidaActual; 
        }
    }

    public void TakeDamage(float cantidad)
    {
        if (sonidoDano != null)
        {
            sonidoDano.Play();
        }

        if (yaEstaMuerto) return;

        vidaActual -= cantidad;
        Debug.Log("¡Daño al jugador! Vida actual: " + vidaActual + "%");

        if (sliderBarraVida != null)
        {
            sliderBarraVida.value = vidaActual;
        }

        if (spriteRenderer != null)
        {
            StartCoroutine(EfectoParpadeoRojo());
        }

        if (vidaActual <= 0)
        {
            MorderElPolvo();
        }
    }

    IEnumerator EfectoParpadeoRojo()
    {
        if (animator != null) animator.enabled = false;

        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;

        if (animator != null) animator.enabled = true;
    }

    void MorderElPolvo()
    {
        yaEstaMuerto = true;
        Debug.Log("Has muerto");

        Time.timeScale = 0f;

        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }

        SceneManager.LoadScene("GameOver");
    }
}