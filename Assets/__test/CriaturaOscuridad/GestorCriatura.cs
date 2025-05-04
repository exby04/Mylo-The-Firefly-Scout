using UnityEngine;

public class GestorCriatura : MonoBehaviour
{
    [SerializeField] private AtenuacionLuz atenuacionLuz;
    [SerializeField] private GameObject prefabCriatura;
    [SerializeField] private Transform jugador;
    [SerializeField] private GameObject simboloExclamacionUI;
    [SerializeField] private Vector3 direccionFija = new Vector3(0, 0, 1); // Dirección desde donde viene la criatura
    [SerializeField] private float distanciaDesdeJugador = 8f;
    [SerializeField] private float alturaCriatura = 0f; // Altura Y fija para la criatura

    private bool criaturaInstanciada = false;
    private GameObject criaturaActual;

    void Update()
    {
        if (!criaturaInstanciada && atenuacionLuz.luzApagada)
        {
            criaturaInstanciada = true;

            // Calcular posición en dirección fija respecto al jugador
            Vector3 posicionMundo = jugador.position + direccionFija.normalized * distanciaDesdeJugador;
            posicionMundo.y = alturaCriatura; // mantener altura constante

            // Instanciar la criatura
            criaturaActual = Instantiate(prefabCriatura, posicionMundo, Quaternion.identity);

            // Pasar referencias a la criatura
            CriaturaOscuridad script = criaturaActual.GetComponent<CriaturaOscuridad>();
            if (script != null)
            {
                script.Configurar(atenuacionLuz, jugador);
            }

            // Bloquear movimiento del jugador
            PlayerController pc = jugador.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.puedeMover = false;
            }

            // Mostrar símbolo de exclamación
            if (simboloExclamacionUI != null)
            {
                simboloExclamacionUI.SetActive(true);
                simboloExclamacionUI.transform.position = jugador.position + Vector3.up * 2f;
                simboloExclamacionUI.transform.rotation = Quaternion.identity;
                Invoke(nameof(DesactivarSimbolo), 1f);
            }
        }

        // Si la luz vuelve, restaurar
        if (criaturaInstanciada && !atenuacionLuz.luzApagada)
        {
            PlayerController pc = jugador.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.puedeMover = true;
            }

            criaturaInstanciada = false;

            if (criaturaActual != null)
            {
                Destroy(criaturaActual);
            }
        }
    }

    private void DesactivarSimbolo()
    {
        if (simboloExclamacionUI != null)
        {
            simboloExclamacionUI.SetActive(false);
        }
    }
}
