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

        
        agent.SetDestination(jugador.position);

        Vector3 direccion = jugador.position - transform.position;
        direccion.y = 0;
        if (direccion != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direccion);
            Vector3 euler = targetRotation.eulerAngles;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, euler.y, 0), 10f * Time.deltaTime);
        }


        Vector2 posCriatura = new Vector2(transform.position.x, transform.position.z);
        Vector2 posJugador = new Vector2(jugador.position.x, jugador.position.z);

        if (Vector2.Distance(posCriatura, posJugador) < 0.5f)
        {
            jugador.GetComponent<PlayerHealth>()?.TakeDamage(999);
            Destroy(gameObject);
        }
    }
}
