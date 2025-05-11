using UnityEngine;
using UnityEngine.AI;

public class CriaturaOscuridad : MonoBehaviour
{
    private AtenuacionLuz atenuacionLuz;
    private Transform jugador;
    private NavMeshAgent agent;

    [SerializeField] private float tiempoEsperaAntesDeAtacar = 1f;
    private bool haComenzadoAtaque = false;
    private float temporizador = 0f;

    public void Configurar(AtenuacionLuz luz, Transform jugadorRef)
    {
        atenuacionLuz = luz;
        jugador = jugadorRef;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        jugador = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (jugador == null) return;

        // Simular que la luz ya está apagada para probar el ataque directamente
        if (!haComenzadoAtaque)
        {
            haComenzadoAtaque = true;
            temporizador = 0f;
        }

        if (temporizador < tiempoEsperaAntesDeAtacar)
        {
            temporizador += Time.deltaTime;
            return;
        }

        // Perseguir al jugador
        agent.SetDestination(jugador.position);

        // Mirar hacia el jugador
        Vector3 direccion = jugador.position - transform.position;
        direccion.y = 0;
        if (direccion != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direccion), 10f * Time.deltaTime);
        }

        // Verificar colisión con el jugador
        Vector2 posCriatura = new Vector2(transform.position.x, transform.position.z);
        Vector2 posJugador = new Vector2(jugador.position.x, jugador.position.z);

        if (Vector2.Distance(posCriatura, posJugador) < 0.5f)
        {
            jugador.GetComponent<PlayerHealth>()?.TakeDamage(999);
            Destroy(gameObject);
        }
    }
}
