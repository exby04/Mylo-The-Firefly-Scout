using System.Collections;
using UnityEngine;

public class BatAttackTimer : MonoBehaviour
{
    [Header("Configuración de Ataque de Murciélagos")]
    public float attackInterval = 40f;
    private float timer = 0f;
    private bool hasShownWarning = false;
    private bool isAttacking = false; 

    [Header("Referencias")]
    public GameObject batPrefab;         
    public Transform[] batSpawnPoints;   
    public GameObject warningText;       

    private GameObject cachedPlayer;

    void Start()
    {
        cachedPlayer = GameObject.FindWithTag("Player");
        if (cachedPlayer == null)
            Debug.LogWarning("Jugador no encontrado. Asegúrate de que tenga la etiqueta 'Player'.");
    }

    void Update()
    {
        // Bloquear ataque si ya hay uno en curso
        if (isAttacking) return;

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
            Debug.Log("¡Murciélagos en camino!  " + warningText.name);
            StartCoroutine(HideWarningAfterDelay(2f));
        }

        hasShownWarning = true;
    }

    IEnumerator HideWarningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (warningText != null)
        {
            warningText.SetActive(false);
            Debug.Log("Aviso de murciélagos ocultado automáticamente");
        }
    }

    IEnumerator TriggerBatAttack()
    {
        isAttacking = true;

        if (warningText != null)
            warningText.SetActive(false);

        Debug.Log("¡Ataque de murciélagos iniciado!");

        if (batSpawnPoints.Length > 0 && batPrefab != null && cachedPlayer != null)
        {
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

        // Esperar un poco antes de permitir nuevos ataques
        yield return new WaitForSeconds(2f);

        isAttacking = false;
    }

    // Permite forzar el ataque desde otros scripts (por ejemplo, trampas)
    public void ForceBatAttack()
    {
        StopAllCoroutines();
        StartCoroutine(TriggerBatAttack());
        timer = 0f;
        hasShownWarning = false;
    }

    // Método para reiniciar el temporizador sin atacar
    public void ResetTimer()
    {
        timer = 0f;
        hasShownWarning = false;
        Debug.Log("Temporizador reiniciado manualmente desde otra fuente (ej. cofre trampa).");
    }
}
