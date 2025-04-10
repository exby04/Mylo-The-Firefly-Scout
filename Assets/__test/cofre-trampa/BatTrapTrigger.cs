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
            Debug.LogWarning("🛑 BatAttackTimer reference is missing!");
        }
    }

   void TriggerBats()
{
    if (batAttackTimer != null)
    {
        // 💡 Force-enable it here
        batAttackTimer.gameObject.SetActive(true);

        Debug.Log("🟡 Triggering bats via: " + batAttackTimer.gameObject.name);
        batAttackTimer.ForceBatAttack();
    }

    gameObject.SetActive(false);
}


}
