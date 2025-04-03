using UnityEngine;

public class AtenuacionLuz : MonoBehaviour
{
    public Light luz;
    public float tiempo = 5f;

    public float rangoMaximo = 8f;
    public float rangoMinimo = 4.6f;

    public float intensidadMaxima = 10f;
    public float intensidadMinima = 40f;

    private float tiempoActual = 0f;

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
    }

    public void ReiniciarLuz()
    {
        tiempoActual = 0f;
        luz.range = rangoMaximo;
        luz.intensity = intensidadMaxima;
    }
}
