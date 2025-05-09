using UnityEngine;
using System.Collections;

public class PowerUpLuz : MonoBehaviour
{
    [SerializeField] private AtenuacionLuz atenuacionLuz;
    [SerializeField] private float rangoExtra = 4f;
    [SerializeField] private float duracion = 10f;

    private bool activo = false;
    private float tiempoRestante = 0f;
    private float rangoOriginal;
    public bool EstaActivo => activo;


    public void Activar()
    {
        if (atenuacionLuz == null)
        {
            UnityEngine.Debug.LogWarning("No se asignó AtenuacionLuz al power-up");
            return;
        }

        if (!activo)
        {
            
            rangoOriginal = atenuacionLuz.luz.range;
            atenuacionLuz.enabled = false;

            atenuacionLuz.luz.range = rangoOriginal + rangoExtra;

            activo = true;
            tiempoRestante = duracion;

        }
    }

    private void Update()
    {
        if (activo)
        {
            tiempoRestante -= Time.deltaTime;

            if (tiempoRestante <= 0f)
            {
             StartCoroutine(TransicionSuaveAlFinal());
             activo = false;

            }
        }
    }
    private IEnumerator TransicionSuaveAlFinal()
{
    float duracion = 1.5f; 
    float tiempo = 0f;

    float rangoInicial = atenuacionLuz.luz.range;
    float rangoObjetivo = rangoOriginal;

    while (tiempo < duracion)
    {
        tiempo += Time.deltaTime;
        float t = tiempo / duracion;

        atenuacionLuz.luz.range = Mathf.Lerp(rangoInicial, rangoObjetivo, t);
        yield return null;
    }

    atenuacionLuz.luz.range = rangoObjetivo;
    atenuacionLuz.enabled = true;
}

}
