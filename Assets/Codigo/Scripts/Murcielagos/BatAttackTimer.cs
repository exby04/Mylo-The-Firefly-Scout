using System.Collections;
using UnityEngine;

public class BatAttackTimer : MonoBehaviour
{
    [Header("Configuración de Ataque de Murciélagos")]
    public float attackInterval = 40f;

    [SerializeField] private float timer = 0f;
    private bool hasShownWarning = false;
    private bool isAttacking = false;

    [SerializeField] private Transform jugador;

    [Header("Puntos de Spawn de Murciélagos")]
    public Transform[] batSpawnPoints;

    [Header("Referencias")]
    public GameObject batPrefab;
    public GameObject warningText;

    [Header("Animacion de Ataque")]
    public Animator animator;
    public float delayAnimacionAtaque;

    private GameObject cachedPlayer;
    private Coroutine flashingCoroutine;


    void Start()
    {
        cachedPlayer = GameObject.FindWithTag("Player");
        if (cachedPlayer == null)
            Debug.LogWarning("Jugador no encontrado. Asegúrate de que tenga la etiqueta 'Player'.");
    }

    void Update()
    {
        if (isAttacking || CriaturaOscuridad.criaturaEstaAtacando) return;

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
    if (warningText == null) return;

    warningText.SetActive(true);

    if (flashingCoroutine != null)
        StopCoroutine(flashingCoroutine);

    flashingCoroutine = StartCoroutine(FlashWarningOverTime(5f));
    hasShownWarning = true;

    Debug.Log("¡Murciélagos en camino! " + warningText.name);
}



   IEnumerator FlashWarningOverTime(float duration)
{
    float elapsed = 0f;

    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        float progress = elapsed / duration;

    
        float curvedProgress = Mathf.Pow(progress, 2.5f); 

        
        float flashRate = Mathf.Lerp(0.6f, 0.02f, curvedProgress);

        if (warningText != null)
            warningText.SetActive(true);

        yield return new WaitForSeconds(flashRate / 2f);

        if (warningText != null)
            warningText.SetActive(false);

        yield return new WaitForSeconds(flashRate / 2f);
    }

    if (warningText != null)
        warningText.SetActive(false);
}




    IEnumerator DesactivarMovimiento(float delay)
    {
        if (cachedPlayer == null)
            yield break;

        var controller = cachedPlayer.GetComponent<PlayerController>();
        if (controller != null)
            controller.enabled = false;

        yield return new WaitForSeconds(delay);

        if (controller != null)
            controller.enabled = true;
    }

    IEnumerator TriggerAttackAnimation(float retraso)
    {
        yield return new WaitForSeconds(retraso);
        animator.SetTrigger("GetAttacked");
        StartCoroutine(DesactivarMovimiento(1f));
    }

    IEnumerator TriggerBatAttack()
{
    isAttacking = true;

    
    if (flashingCoroutine != null)
    {
        StopCoroutine(flashingCoroutine);
        flashingCoroutine = null;
    }

    
    if (warningText != null)
        warningText.SetActive(false);

    Debug.Log("¡Ataque de murciélagos iniciado!");

    if (batSpawnPoints.Length > 0 && batPrefab != null && cachedPlayer != null)
    {
        Transform spawnPoint = batSpawnPoints[Random.Range(0, batSpawnPoints.Length)];
        Vector3 spawnPos = spawnPoint.position;

        GameObject bat = Instantiate(batPrefab, spawnPos, Quaternion.identity);
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

    yield return new WaitForSeconds(2f);

    isAttacking = false;
}


    public void ForceBatAttack()
    {
        StopAllCoroutines();

        ShowWarning();
        StartCoroutine(TriggerBatAttackConRetraso(2f));

        timer = 0f;
        hasShownWarning = false;
    }

    IEnumerator TriggerBatAttackConRetraso(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return TriggerBatAttack();
    }

    public void ResetTimer()
    {
        timer = 0f;
        hasShownWarning = false;
        Debug.Log("Temporizador reiniciado manualmente desde otra fuente (ej. cofre trampa).");
    }
}
