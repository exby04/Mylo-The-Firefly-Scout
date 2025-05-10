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

        
        if (!atenuacionLuz.luzApagada)
            return;

   
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

        
        Vector3 objetivo = new Vector3(jugador.position.x, 2f, jugador.position.z);
        Vector3 direccion = (objetivo - transform.position).normalized;
        transform.position += direccion * velocidad * Time.deltaTime;

        
        Vector2 posCriatura = new Vector2(transform.position.x, transform.position.z);
        Vector2 posJugador = new Vector2(jugador.position.x, jugador.position.z);

       
        if (Vector2.Distance(posCriatura, posJugador) < 0.5f)
        {
            jugador.GetComponent<PlayerHealth>()?.TakeDamage(999, true); 
            Destroy(gameObject);
        }
    }
}
