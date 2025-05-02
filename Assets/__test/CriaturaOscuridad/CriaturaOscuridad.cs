using UnityEngine;

public class CriaturaOscuridad : MonoBehaviour
{
    private AtenuacionLuz atenuacionLuz;
    private Transform jugador;

    [SerializeField] private float velocidad = 6f;
    [SerializeField] private float tiempoEsperaAntesDeAtacar = 1f;

    private bool haComenzadoAtaque = false;
    private float temporizador = 0f;

    public void Configurar(AtenuacionLuz luz, Transform jugadorRef)
    {
        atenuacionLuz = luz;
        jugador = jugadorRef;
    }

    void Update()
    {
        if (atenuacionLuz == null || jugador == null)
            return;

        // Esperar a que se apague la luz
        if (!atenuacionLuz.luzApagada)
            return;

        // Inicia la cuenta atrás solo una vez
        if (!haComenzadoAtaque)
        {
            haComenzadoAtaque = true;
            temporizador = 0f; // asegurarse
        }

        // Esperar el tiempo con la exclamación antes de atacar
        if (temporizador < tiempoEsperaAntesDeAtacar)
        {
            temporizador += Time.deltaTime;
            return;
        }

        // Movimiento horizontal hacia el jugador (altura fija)
        Vector3 objetivo = new Vector3(jugador.position.x, 2f, jugador.position.z);
        Vector3 direccion = (objetivo - transform.position).normalized;
        transform.position += direccion * velocidad * Time.deltaTime;

        // Comprobación de alcance en plano XZ (ignorar altura)
        Vector2 posCriatura = new Vector2(transform.position.x, transform.position.z);
        Vector2 posJugador = new Vector2(jugador.position.x, jugador.position.z);

        // Si lo alcanza, lo elimina
        if (Vector2.Distance(posCriatura, posJugador) < 0.5f)
        {
            jugador.GetComponent<PlayerHealth>()?.TakeDamage(999);
            Destroy(gameObject);
        }
    }
}
