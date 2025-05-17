using UnityEngine;

public class AbrirCofreTrampa : MonoBehaviour
{
    public GameObject cofreAbiertoPrefab;
    private bool jugadorCerca = false;
    private bool abierto = false;

    public GameObject mensajeUI;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = FindFirstObjectByType<PlayerController>().transform;
    }

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
        if (other.transform == playerTransform)
        {
            jugadorCerca = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == playerTransform)
        {
            jugadorCerca = false;
        }
    }
}
