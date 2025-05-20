using UnityEngine;

public class AbrirCofreTrampa : MonoBehaviour, IInteractable
{
    [Header("Animación")]
    public Animator animator;

    [Header("Configuración de interacción")]
    public float tiempoParaAbrir = 2f;

    private bool abierto = false;
    private BatAttackTimer batAttackTimer;

    private void Start()
    {
        batAttackTimer = FindFirstObjectByType<BatAttackTimer>();
    }

    public float HoldDuration => tiempoParaAbrir;

    public void OnInteract()
    {
        if (abierto) return;

        abierto = true;

        if (animator != null)
            animator.SetTrigger("AbrirCofre");

        if (batAttackTimer != null)
            batAttackTimer.ForceBatAttack();
    }
}
