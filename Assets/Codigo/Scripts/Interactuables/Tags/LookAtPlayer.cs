using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = Camera.main.transform;

    }

    void LateUpdate()
    {
        if (player != null)
        {
            transform.LookAt(player);
            // Opcional: invertir para que no se vea al revés
            transform.Rotate(0, 180f, 0);
        }
    }
}
