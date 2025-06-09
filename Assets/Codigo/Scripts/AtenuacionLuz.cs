using System.Diagnostics;
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

    public delegate void AdvertenciaLuzEvent();
    public event AdvertenciaLuzEvent OnAdvertenciaLuz;

    public delegate void LuzApagadaEvent();
    public event LuzApagadaEvent OnLuzApagada;

    private bool advertenciaLanzada = false;

    public delegate void LuzReiniciadaEvent();
    public event LuzReiniciadaEvent OnLuzReiniciada;


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

            if (!advertenciaLanzada && tiempoActual >= (tiempo - 10f))
            {
                advertenciaLanzada = true;
                OnAdvertenciaLuz?.Invoke();
            }
        }
        else if (!luzApagada && luz.range <= rangoMinimo)
        {
            luzApagada = true;
            luz.range = rangoMinimo;

            OnLuzApagada?.Invoke();
        }
    }

    public void ReiniciarLuz()
    {
        tiempoActual = 0f;
        luz.range = rangoMaximo;
        luz.intensity = intensidadMaxima;
        luzApagada = false;
        advertenciaLanzada = false;

        UnityEngine.Debug.Log("La luz se ha reiniciado.");
        OnLuzReiniciada?.Invoke();
    }
}
