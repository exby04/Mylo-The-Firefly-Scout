using UnityEngine;

public class CriaturaOscuridad : MonoBehaviour
{
    private AtenuacionLuz atenuacionLuz;
    private Transform jugador;

    [SerializeField] private float tiempoEsperaAntesDeAtacar = 1f;
    [SerializeField] private float velocidad = 5f;
    private bool haComenzadoAtaque = false;
    private float temporizador = 0f;

    public void Configurar(AtenuacionLuz luz, Transform jugadorRef)
    {
        atenuacionLuz = luz;
        jugador = jugadorRef;
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

        transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);

        Vector3 direccion = jugador.position - transform.position;
        direccion.y = 0;
        if (direccion != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 10f * Time.deltaTime);
        }

        //Hacer daño cuando esta cerca
        Vector2 posCriatura = new Vector2(transform.position.x, transform.position.z);
        Vector2 posJugador = new Vector2(jugador.position.x, jugador.position.z);

        if (Vector2.Distance(posCriatura, posJugador) < 0.5f)
        {
            jugador.GetComponent<PlayerHealth>()?.TakeDamage(999);
            Destroy(gameObject);
        }
    }
}
