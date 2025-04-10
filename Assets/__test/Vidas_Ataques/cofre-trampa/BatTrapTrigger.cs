using UnityEngine;

public class BatTrapTrigger : MonoBehaviour
{
    public BatAttackTimer batAttackTimer;
    public float delayBeforeAttack = 2f;

    void OnEnable()
    {
        Debug.Log("⚠️ ¡Murciélagos han sido atraídos por una trampa!");

        if (batAttackTimer != null)
        {
            Invoke(nameof(TriggerBats), delayBeforeAttack);
        }
        else
        {
            Debug.LogWarning("Referencia a BatAttackTimer ausente.");
        }
    }

    void TriggerBats()
    {
        if (batAttackTimer != null)
        {
            // Opcional: activa el objeto del batAttackTimer si estuviera inactivo
            batAttackTimer.gameObject.SetActive(true);

            Debug.Log("🟡 Forzando ataque de murciélagos desde BatTrapTrigger.");
            batAttackTimer.ForceBatAttack();
        }
        gameObject.SetActive(false);
    }
}
