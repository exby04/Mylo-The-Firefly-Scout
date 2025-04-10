using System.Collections;
using UnityEngine;

public class BatAttackTimer : MonoBehaviour
{
    [Header("Configuración de Ataque de Murciélagos")]
    public float attackInterval = 40f;
    private float timer = 0f;
    private bool hasShownWarning = false;

    [Header("Referencias")]
    public GameObject batPrefab;         // Prefab del murciélago
    public Transform[] batSpawnPoints;     // Puntos de aparición de murciélagos
    public GameObject warningText;         // Objeto UI de advertencia (activar/desactivar)

    private GameObject cachedPlayer;

    void Start()
    {
        cachedPlayer = GameObject.FindWithTag("Player");
        if (cachedPlayer == null)
            Debug.LogWarning("Jugador no encontrado. Asegúrate de que tenga la etiqueta 'Player'.");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!hasShownWarning && timer >= attackInterval - 5f)
        {
            ShowWarning();
        }

        if (timer >= attackInterval)
        {
            StartCoroutine(TriggerBatAttack());
            timer = 0f;
            hasShownWarning = false;
        }
    }

    void ShowWarning()
    {
        if (warningText != null)
        {
            warningText.SetActive(true);
            Debug.Log("⚠️ ¡Advertencia! Los murciélagos se aproximan.");
        }
        hasShownWarning = true;
    }

    IEnumerator TriggerBatAttack()
    {
        if (warningText != null)
            warningText.SetActive(false);

        Debug.Log("🦇 ¡Ataque de murciélagos iniciado!");

        if (batSpawnPoints.Length > 0 && batPrefab != null && cachedPlayer != null)
        {
            // Ejemplo: usamos el primer punto de aparición
            GameObject bat = Instantiate(batPrefab, batSpawnPoints[0].position, Quaternion.identity);
            BatMovement batScript = bat.GetComponent<BatMovement>();
            if (batScript != null)
            {
                batScript.FlyToAttack(cachedPlayer);
            }
            else
            {
                Debug.LogWarning("El prefab del murciélago no tiene el script BatMovement.");
            }
        }
        else
        {
            Debug.LogWarning("Faltan datos: puntos de aparición, prefab de murciélago o referencia al jugador.");
        }
        yield return null;
    }

    // Permite forzar el ataque desde otros scripts (por ejemplo, trampas)
    public void ForceBatAttack()
    {
        StopAllCoroutines();
        StartCoroutine(TriggerBatAttack());
        timer = 0f;
        hasShownWarning = false;
    }
}
