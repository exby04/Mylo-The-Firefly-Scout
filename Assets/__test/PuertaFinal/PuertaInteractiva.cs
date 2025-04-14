using UnityEngine;

public class PuertaInteractiva : MonoBehaviour
{
    public Animator animator;
    public Collider colisionPuerta;
    private bool enRango = false;
    private bool yaSeAbrio = false;

    void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E) && !yaSeAbrio)
        {
            if (Inventario.instance.TieneLlave())
            {
                animator.SetTrigger("Abrir");
                yaSeAbrio = true;
                Invoke(nameof(ActivarTrigger), 1.0f);
            }
            else
            {
                Debug.Log("Necesitas una llave para abrir esta puerta.");
            }
        }
    }

    void ActivarTrigger()
    {
        if (colisionPuerta != null)
        {
            colisionPuerta.isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enRango = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enRango = false;
        }
    }
}
