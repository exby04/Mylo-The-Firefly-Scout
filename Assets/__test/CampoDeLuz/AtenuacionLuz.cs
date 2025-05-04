using UnityEngine;

public class AtenuacionLuz : MonoBehaviour
{
    public Light luz;
    public float tiempo = 5f;

    public float rangoMaximo = 8f;
    public float rangoMinimo = 4.4f;

    public float intensidadMaxima = 10f;
    public float intensidadMinima = 40f;

    private float tiempoActual = 0f;

    public bool luzApagada { get; private set; } = false;

    [SerializeField] private GameObject luzDelantera;
    [SerializeField] private GameObject luzTrasera;

    void Start()
    {
        if (luz == null)
        {
            luz = GetComponent<Light>();
        }

        luz.range = rangoMaximo;
        luz.intensity = intensidadMaxima;
    }

    void Update()
    {
        if (tiempoActual < tiempo)
        {
            tiempoActual += Time.deltaTime;
            float t = tiempoActual / tiempo;

            luz.range = Mathf.Lerp(rangoMaximo, rangoMinimo, t);
            luz.intensity = Mathf.Lerp(intensidadMaxima, intensidadMinima, t);
        } 
        else if (!luzApagada && luz.range <= rangoMinimo)
        {
            luzApagada = true;
            luz.range = 4.5f;

            //if (luzDelantera != null) luzDelantera.SetActive(false);
            //if (luzTrasera != null) luzTrasera.SetActive(false);

        }
    }

    public void ReiniciarLuz()
    {
        tiempoActual = 0f;
        luz.range = rangoMaximo;
        luz.intensity = intensidadMaxima;

        Debug.Log("La luz se ha apagado completamente.");
    }
}
