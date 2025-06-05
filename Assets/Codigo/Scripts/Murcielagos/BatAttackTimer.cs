using System.Collections;
using UnityEngine;

public class BatAttackTimer : MonoBehaviour
{
    [Header("Configuración de Ataque de Murciélagos")]
    public float attackInterval = 40f;

    [Header("Spawn Settings")]
    [Header("Puntos de Spawn de Murciélagos")]
public Transform[] batSpawnPoints;


    [SerializeField] private float distanceFromPlayer = 5f;
    [Tooltip("Altura desde la que aparecen los murciélagos sobre el jugador")]
    [SerializeField] private float spawnHeight = 5f;

    [SerializeField] private float timer = 0f;
    private bool hasShownWarning = false;
    private bool isAttacking = false;

    [SerializeField] private Transform jugador;

    [Header("Referencias")]
    public GameObject batPrefab;
    public GameObject warningText;

    [Header("Animacion de Ataque")]
    public Animator animator;
    public float delayAnimacionAtaque;

    private GameObject cachedPlayer;


    void Start()
    {
        cachedPlayer = GameObject.FindWithTag("Player");
        if (cachedPlayer == null)
            Debug.LogWarning("Jugador no encontrado. Asegúrate de que tenga la etiqueta 'Player'.");
    }

    void Update()
    {
        if (CriaturaOscuridad.CriaturaActiva) return;
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

            if (warningText.GetComponent<LookAtPlayer>() == null)
                warningText.AddComponent<LookAtPlayer>();

            Debug.Log("¡Murciélagos en camino!  " + warningText.name);
            StartCoroutine(ShakeWarningOverTime(5f));
        }

        hasShownWarning = true;
    }

    IEnumerator DesactivarMovimiento(float delay)
    {
        if (cachedPlayer == null) yield break;

        var controller = cachedPlayer.GetComponent<PlayerController>();
        if (controller != null) controller.enabled = false;

        yield return new WaitForSeconds(delay);

        if (controller != null) controller.enabled = true;
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

    if (warningText != null)
        warningText.SetActive(false);


    if (CriaturaOscuridad.CriaturaActiva)
    {
        isAttacking = false;
        yield break;
    }

    Debug.Log("¡Ataque de murciélagos iniciado!");

    if (batPrefab != null && cachedPlayer != null)
    {
        Vector3 spawnPos;

        if (batSpawnPoints != null && batSpawnPoints.Length > 0)
        {
            Transform spawnPoint = batSpawnPoints[Random.Range(0, batSpawnPoints.Length)];
            spawnPos = spawnPoint.position;
        }
        else
        {
            Debug.LogWarning("No hay puntos de spawn asignados. Usando posición por defecto.");
            spawnPos = cachedPlayer.transform.position + Vector3.back * distanceFromPlayer + Vector3.up * spawnHeight;
        }

        GameObject bat = Instantiate(batPrefab, spawnPos, Quaternion.identity);
        BatMovement batScript = bat.GetComponent<BatMovement>();

        if (batScript != null)
        {
            batScript.FlyToAttack(cachedPlayer);

            if (!CriaturaOscuridad.CriaturaActiva)
            {
                float flightTime = batScript.GetEstimatedFlightTime(cachedPlayer.transform.position);
                StartCoroutine(ShakeWarningOverTime(flightTime));
            }
        }
    }
    else
    {
        Debug.LogWarning("Faltan datos: prefab de murciélago o referencia al jugador.");
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

    IEnumerator ShakeWarningOverTime(float duration)
    {
        if (warningText == null) yield break;

        Transform warningTransform = warningText.transform;
        Vector3 originalLocalPos = warningTransform.localPosition;

        SpriteRenderer sr = warningText.GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        Color color1 = Color.white;
        Color color2 = Color.yellow;
        Color color3 = new Color(1f, 0.5f, 0f);
        Color color4 = Color.red;

        float elapsed = 0f;
        float shakeStartThreshold = 0.2f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;

            if (progress < 0.33f)
                sr.color = Color.Lerp(color1, color2, progress / 0.33f);
            else if (progress < 0.66f)
                sr.color = Color.Lerp(color2, color3, (progress - 0.33f) / 0.33f);
            else
                sr.color = Color.Lerp(color3, color4, (progress - 0.66f) / 0.34f);

            
            float intensity = 0f;
            if (progress > shakeStartThreshold)
            {
                float shakeProgress = (progress - shakeStartThreshold) / (1f - shakeStartThreshold);
                intensity = Mathf.Lerp(0f, 0.3f, shakeProgress);
            }

            float offsetX = (Mathf.PerlinNoise(Time.time * 10f, 0f) - 0.5f) * intensity;
            float offsetY = (Mathf.PerlinNoise(0f, Time.time * 10f) - 0.5f) * intensity;

            warningTransform.localPosition = originalLocalPos + new Vector3(offsetX, offsetY, 0);
            yield return null;
        }

        warningTransform.localPosition = originalLocalPos;
        sr.color = color1;
        warningText.SetActive(false);
    }
}
