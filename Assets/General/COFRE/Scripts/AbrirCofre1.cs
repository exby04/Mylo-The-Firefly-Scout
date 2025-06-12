using UnityEngine;

public class AbrirCofre_powerup: MonoBehaviour
{
    public GameObject cofreAbiertoPrefab;
    private bool jugadorCerca = false;
    private bool abierto = false;

    public GameObject mensajeUI;

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
        mensajeUI.SetActive(true);
    }

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
