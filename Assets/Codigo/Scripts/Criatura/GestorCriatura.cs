using System;
using UnityEngine;

public class GestorCriatura : MonoBehaviour
{
    [SerializeField] private AtenuacionLuz atenuacionLuz;
    [SerializeField] private GameObject prefabCriatura;
    [SerializeField] private Transform jugador;
    [SerializeField] private GameObject simboloExclamacionUI;

    [SerializeField] private float distanciaAlrededorJugador = 6f;
    [SerializeField] private float alturaCriatura = 0f;

    private bool criaturaInstanciada = false;
    private GameObject criaturaActual;

    void Update()
    {
        if (!criaturaInstanciada && atenuacionLuz.luzApagada)
        {
            criaturaInstanciada = true;

            Vector3 posicionValida = BuscarPosicionValidaCercaJugador(jugador.position, distanciaAlrededorJugador, alturaCriatura);

            criaturaActual = Instantiate(prefabCriatura, posicionValida, Quaternion.identity);
            criaturaActual.transform.LookAt(jugador);

            CriaturaOscuridad script = criaturaActual.GetComponent<CriaturaOscuridad>();
            if (script != null)
            {
                script.Configurar(atenuacionLuz, jugador);
            }

            if (simboloExclamacionUI != null)
            {
                simboloExclamacionUI.SetActive(true);
                if (simboloExclamacionUI.GetComponent<LookAtPlayer>() == null)
                {
                    simboloExclamacionUI.AddComponent<LookAtPlayer>();
                }
                Invoke(nameof(DesactivarSimbolo), 1f);
            }
        }

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

    private Vector3 BuscarPosicionValidaCercaJugador(Vector3 origen, float radioMax, float altura)
    {
        int intentosMaximos = 20;
        float distanciaMinima = 2f;

        for (int i = 0; i < intentosMaximos; i++)
        {
            Vector2 randomCircle = UnityEngine.Random.insideUnitCircle * radioMax;
            Vector3 puntoDesdeAire = new Vector3(origen.x + randomCircle.x, origen.y + 10f, origen.z + randomCircle.y);

            if (Physics.Raycast(puntoDesdeAire, Vector3.down, out RaycastHit hit, 20f))
            {
                if ((hit.collider.CompareTag("Floor") || hit.collider.CompareTag("Camino")) &&
                    Vector3.Distance(hit.point, origen) >= distanciaMinima)
                {
                    Vector3 puntoValido = hit.point;
                    puntoValido.y = altura;
                    return puntoValido;
                }
            }
        }

        UnityEngine.Debug.LogWarning("No se encontró suelo válido lejos del jugador. Se usará la posición del jugador.");
        return new Vector3(origen.x, altura, origen.z);
    }
}
