using UnityEngine;

public class GestorCriatura : MonoBehaviour
{
    [SerializeField] private AtenuacionLuz atenuacionLuz;
    [SerializeField] private GameObject prefabCriatura;
    [SerializeField] private Transform jugador;
    [SerializeField] private GameObject simboloExclamacionUI;
    [SerializeField] private Camera camara;
    [SerializeField] private Vector2 esquinaPantalla = new Vector2(0.95f, 0.95f); // esquina sup. derecha
    [SerializeField] private float zMundo = 0f;

    private bool criaturaInstanciada = false;
    private GameObject criaturaActual;

    void Update()
    {
        if (!criaturaInstanciada && atenuacionLuz.luzApagada)
        {
            criaturaInstanciada = true;

            // Calcular posición visible desde la cámara
            Vector3 posPantalla = new Vector3(
                Screen.width * esquinaPantalla.x,
                Screen.height * esquinaPantalla.y,
                camara.nearClipPlane + 5f
            );
            Vector3 posicionMundo = camara.ScreenToWorldPoint(posPantalla);
            posicionMundo.z = zMundo;

            // Instanciar la criatura
            criaturaActual = Instantiate(prefabCriatura, posicionMundo, Quaternion.identity);

            // Pasar referencias
            CriaturaOscuridad script = criaturaActual.GetComponent<CriaturaOscuridad>();
            if (script != null)
            {
                script.Configurar(atenuacionLuz, jugador);
            }

            // Desactivar el movimiento del jugador
            PlayerController pc = jugador.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.puedeMover = false;
            }

            // Posicionar y mostrar el símbolo
            if (simboloExclamacionUI != null)
            {
                simboloExclamacionUI.SetActive(true);
                simboloExclamacionUI.transform.position = jugador.position + Vector3.up * 2f;
                simboloExclamacionUI.transform.rotation = Quaternion.identity;
                Invoke(nameof(DesactivarSimbolo), 1f);
            }
        }

        // Si la luz vuelve y la criatura no ha atacado todavía, reactivar movimiento
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
