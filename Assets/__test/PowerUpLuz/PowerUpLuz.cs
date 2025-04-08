using UnityEngine;

public class PowerUpLuz : MonoBehaviour
{
    [SerializeField] private AtenuacionLuz atenuacionLuz;
    [SerializeField] private float rangoExtra = 4f;
    [SerializeField] private float duracion = 10f;

    private bool activo = false;
    private float tiempoRestante = 0f;
    private float rangoOriginal;

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
                atenuacionLuz.luz.range = rangoOriginal;
                atenuacionLuz.enabled = true;
                activo = false;
            }
        }
    }
}
