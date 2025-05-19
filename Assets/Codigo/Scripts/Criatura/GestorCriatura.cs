using UnityEngine;

public class GestorCriatura : MonoBehaviour
{
    [SerializeField] private AtenuacionLuz atenuacionLuz;
    [SerializeField] private GameObject prefabCriatura;
    [SerializeField] private Transform jugador;
    [SerializeField] private GameObject simboloExclamacionUI;

    [SerializeField] private float distanciaDelanteJugador = 6f;
    [SerializeField] private float alturaCriatura = 0f;

    private bool criaturaInstanciada = false;
    private GameObject criaturaActual;

    void Update()
    {
        
        if (!criaturaInstanciada && atenuacionLuz.luzApagada)
        {
            criaturaInstanciada = true;

            // aparecer delante del jugador
            Vector3 posicionDelante = jugador.position + jugador.forward.normalized * distanciaDelanteJugador;
            posicionDelante.y = alturaCriatura;

            criaturaActual = Instantiate(prefabCriatura, posicionDelante, Quaternion.identity);
            criaturaActual.transform.LookAt(jugador);

            CriaturaOscuridad script = criaturaActual.GetComponent<CriaturaOscuridad>();
            if (script != null)
            {
                script.Configurar(atenuacionLuz, jugador);
            }

            if (simboloExclamacionUI != null)
            {
                simboloExclamacionUI.SetActive(true);
                //simboloExclamacionUI.transform.position = jugador.position + Vector3.up * 2f;
                //simboloExclamacionUI.transform.rotation = Quaternion.identity;
                if (simboloExclamacionUI.GetComponent<LookAtPlayer>() == null)
                {
                    simboloExclamacionUI.AddComponent<LookAtPlayer>();

                }
                Invoke(nameof(DesactivarSimbolo), 1f);
            }
        }

        // la criatura desaparece cuando se recupera el rango de luz
        if (criaturaInstanciada && !atenuacionLuz.luzApagada)
        {
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
