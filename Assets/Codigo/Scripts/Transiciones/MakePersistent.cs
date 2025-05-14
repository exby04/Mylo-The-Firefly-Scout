using UnityEngine;

public class MakePersistent : MonoBehaviour
{
    private static MakePersistent instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Eliminar duplicados
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Hacer persistente
    }
}
