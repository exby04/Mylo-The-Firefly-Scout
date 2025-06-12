using UnityEngine;

public class MakePersistent : MonoBehaviour
{
    private static MakePersistent instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
