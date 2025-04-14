using UnityEngine;
using System.Collections;

public class BatTrapTrigger : MonoBehaviour
{
    public BatAttackTimer batAttackTimer;
    public float delayBeforeAttack = 2f;

    [Header("Texto de advertencia")]
    public GameObject warningText;
    public float warningDuration = 2f;

    void OnEnable()
    {
        Debug.Log("⚠️ ¡Murciélagos han sido atraídos por una trampa!");

        if (warningText != null)
        {
            warningText.SetActive(true);
            StartCoroutine(HideWarningAfterDelay(warningDuration));
        }

        if (batAttackTimer != null)
        {
            // Reinicia el temporizador antes de atacar
            batAttackTimer.ResetTimer();
            Invoke(nameof(TriggerBats), delayBeforeAttack);
        }
        else
        {
            Debug.LogWarning("🛑 Referencia a BatAttackTimer ausente.");
        }
    }

    void TriggerBats()
    {
        if (batAttackTimer != null)
        {
            // Activa si estuviera inactivo
            batAttackTimer.gameObject.SetActive(true);

            Debug.Log("🟡 Forzando ataque de murciélagos desde BatTrapTrigger.");
            batAttackTimer.ForceBatAttack(); // El propio método controla si ya hay un ataque
        }

        // Opcional: desactivar la trampa una vez usada
        gameObject.SetActive(false);
    }

    IEnumerator HideWarningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (warningText != null)
        {
            warningText.SetActive(false);
            Debug.Log("🔕 Texto de advertencia de trampa ocultado automáticamente.");
        }
    }
}
