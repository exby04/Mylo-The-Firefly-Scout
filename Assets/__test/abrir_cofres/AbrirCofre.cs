using UnityEngine;

public class AbrirCofre : MonoBehaviour
{
    public GameObject cofreAbiertoPrefab;
    private bool jugadorCerca = false;
    private bool abierto = false;
    public GameObject mensajeUI;

    private static bool llaveObtenida = false; // Variable estática para asegurar que solo haya una llave

    void Update()
    {
        if (jugadorCerca && !abierto && Input.GetKeyDown(KeyCode.E))
        {
            Abrir();
        }
    }

    void Abrir()
    {
        abierto = true;
        Instantiate(cofreAbiertoPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
        //mensajeUI.SetActive(true);

        // Probabilidad de 1/7 para obtener la llave (si aún no ha sido obtenida)
        if (!llaveObtenida && Random.Range(1, 8) == 1)
        {
            llaveObtenida = true;
            Inventario.instance.RecogerLlave();
            Debug.Log("¡Has encontrado una llave!");
        }
        else
        {
            int random = Random.Range(0, 2); // 0 = Velocidad, 1 = Vision

            if (random == 0)
            {
                Inventario.instance.RecogerPowerUp("Velocidad");
                Debug.Log("¡Has encontrado un Power-Up de Velocidad!");
            }
            else
            {
                Inventario.instance.RecogerPowerUp("Vision");
                Debug.Log("¡Has encontrado un Power-Up de Vision!");
            }
        }
    }
    //Revisar aqui porque no esta bien que sea con el TAG
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}
