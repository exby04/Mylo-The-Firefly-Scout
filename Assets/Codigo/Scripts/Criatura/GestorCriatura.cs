using System.Collections;
using UnityEngine;

public class GestorCriatura : MonoBehaviour
{
    [SerializeField] private AtenuacionLuz atenuacionLuz;
    [SerializeField] private GameObject prefabCriatura;
    [SerializeField] private Transform jugador;
    [SerializeField] private Camera camara;

    [SerializeField] private AdvertenciaCriaturaUI advertenciaUI;

    [SerializeField] private AudioSource sonidoAparicion;
    [SerializeField] private AudioSource sonidoLatidos;

    [SerializeField] private float distanciaDesdeJugador = 6f;
    [SerializeField] private float alturaCriatura = 0f;

    private bool criaturaInstanciada = false;
    private GameObject criaturaActual;

    private void Start()
    {
        UnityEngine.Debug.Log("GestorCriatura activo");

        if (atenuacionLuz != null)
        {
            atenuacionLuz.OnAdvertenciaLuz += IniciarAdvertencia;
            atenuacionLuz.OnLuzApagada += AparecerCriatura;
        }
    }

    private void IniciarAdvertencia()
    {
        advertenciaUI?.IniciarAdvertencia();
    }

    private void AparecerCriatura()
    {
        advertenciaUI?.CancelarAdvertencia();
        criaturaInstanciada = true;

        Vector3 posicion = BuscarPosicionDelanteDeCamara(jugador.position, distanciaDesdeJugador, alturaCriatura);

        UnityEngine.Debug.Log($"🐾 Criatura aparecerá en: {posicion}, jugador en: {jugador.position}");
        UnityEngine.Debug.DrawLine(jugador.position, posicion, Color.green, 5f);

        criaturaActual = Instantiate(prefabCriatura, posicion, Quaternion.identity);
        criaturaActual.transform.LookAt(jugador);

        CriaturaOscuridad script = criaturaActual.GetComponent<CriaturaOscuridad>();
        if (script != null)
        {
            script.Configurar(atenuacionLuz, jugador);
        }

        //Sonido

        if (sonidoAparicion != null)
        {
            sonidoAparicion.Play();
        }

        if(sonidoLatidos != null && !sonidoLatidos.isPlaying)
{
            sonidoLatidos.loop = true;
            sonidoLatidos.Play();
        }
    }

    private void Update()
    {
        if (criaturaInstanciada && !atenuacionLuz.luzApagada)
        {
            criaturaInstanciada = false;

            if (criaturaActual != null)
            {
                Destroy(criaturaActual);
            }

            advertenciaUI?.CancelarAdvertencia();

            if (sonidoLatidos != null && sonidoLatidos.isPlaying)
                sonidoLatidos.Stop();

        }
    }

    private Vector3 BuscarPosicionDelanteDeCamara(Vector3 origen, float distancia, float altura)
    {
        Vector3 direccion = camara.transform.forward;
        direccion.y = 0;
        direccion.Normalize();

        Vector3 posicion = origen + direccion * distancia;
        posicion.y = origen.y + altura;

        return posicion;
    }

}
