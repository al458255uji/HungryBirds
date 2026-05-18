using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;
    public float minX = -12f, maxX = 12f, minY = -7f, maxY = 7f;

    [Header("Configuración de Tiempos")]
    [Tooltip("Tiempo de espera para la PRIMERA urraca al iniciar el nivel")]
    public float esperaInicial = 3f;
    [Tooltip("Intervalo de tiempo mínimo entre urracas")]
    public float tiempoMinEntreAves = 5f;
    [Tooltip("Intervalo de tiempo máximo entre urracas")]
    public float tiempoMaxEntreAves = 7f;

    [Header("Sonidos de Alerta (Nivel 2)")]
    public AudioSource sonidoAparicionUrraca;

    private GameObject[] allPlants;
    private int lastSide = -1;

    void Start()
    {
        allPlants = GameObject.FindGameObjectsWithTag("Plant");

        if (allPlants.Length == 0)
        {
            Debug.LogError("¡Ojo! No se ha encontrado ninguna planta con el Tag 'Plant'.");
            return;
        }

        StartCoroutine(RutinaSpawneoContinuo());
    }

    IEnumerator RutinaSpawneoContinuo()
    {
        yield return new WaitForSeconds(esperaInicial);

        while (true) 
        {
            int cantidadAves = Random.Range(1, 3);
            List<int> ladosUsadosEnEstaOleada = new List<int>();

            for (int i = 0; i < cantidadAves; i++)
            {
                SpawnBird(ladosUsadosEnEstaOleada);
            }
            float tiempoAleatorio = Random.Range(tiempoMinEntreAves, tiempoMaxEntreAves);
            yield return new WaitForSeconds(tiempoAleatorio);
        }
    }

    void SpawnBird(List<int> ladosUsadosEnEstaOleada)
    {
        if (allPlants == null || allPlants.Length == 0) return;

        int side;
        int intentos = 0;
        do { side = Random.Range(0, 4); intentos++; } while ((side == lastSide || ladosUsadosEnEstaOleada.Contains(side)) && intentos < 10);
        ladosUsadosEnEstaOleada.Add(side);
        lastSide = side;

        float px = 0, py = 0;
        switch (side)
        {
            case 0: px = minX; py = Random.Range(minY, maxY); break;
            case 1: px = maxX; py = Random.Range(minY, maxY); break;
            case 2: px = Random.Range(minX, maxX); py = maxY; break;
            case 3: px = Random.Range(minX, maxX); py = minY; break;
        }

        int index = Random.Range(0, allPlants.Length);
        GameObject target = allPlants[index];

        if (target == null) return;

        GameObject newBird = Instantiate(birdPrefab, new Vector3(px, py, 0), Quaternion.identity);

        if (sonidoAparicionUrraca != null)
        {
            sonidoAparicionUrraca.Play();
        }

        UrracaAI ai = newBird.GetComponent<UrracaAI>();
        if (ai != null)
        {
            ai.targetPlant = target.transform;
            ai.AsignarSpawner(this);
        }
    }
}