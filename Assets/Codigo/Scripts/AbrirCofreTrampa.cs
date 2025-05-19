using UnityEngine;

public class AbrirCofreTrampa : MonoBehaviour
{
    public GameObject cofreAbiertoPrefab;
    private bool jugadorCerca = false;
    private bool abierto = false;

    private Transform playerTransform;
    private BatAttackTimer batAttackTimer; // Referencia al sistema de ataque de murciélagos

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player")?.transform;
        batAttackTimer = FindFirstObjectByType<BatAttackTimer>(); // O usa [SerializeField] y asígnalo en el inspector
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

        if (batAttackTimer != null)
        {
            batAttackTimer.ForceBatAttack();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            playerTransform = other.transform;
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
