using UnityEngine;
using System.Collections;

public class SpawnOruga : MonoBehaviour
{
    public GameObject prefabOruga;
    public float tiempoDeEspera = 2.0f; // Tiempo que tarda en salir la siguiente tras morir la anterior
    [Header("Sonidos de Alerta")]
    public AudioSource sonidoAparicion;

    void Start()
    {
        if (prefabOruga != null) StartCoroutine(SpawnCiclo());
    }

    IEnumerator SpawnCiclo()
    {
        while (true)
        {
            if (OrugaAI.cantidadVivas == 0)
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

                // Volvemos a comprobar por si otro spawner se adelantó en ese segundo
                if (OrugaAI.cantidadVivas == 0)
                {
                    Instantiate(prefabOruga, transform.position, Quaternion.identity);
                    Debug.Log("Nueva oruga en camino desde: " + gameObject.name);

                    if (sonidoAparicion != null)
                    {
                        sonidoAparicion.Play();
                    }
                }
            }

            // Revisa cada segundo si el mapa está libre
            yield return new WaitForSeconds(1.0f);
        }
    }
}
