using UnityEngine;

public class FireflyLight : MonoBehaviour
{
    private Light fireflyLight;

    void Start()
    {
        fireflyLight = GetComponent<Light>();
    }

    void Update()
    {
        float velocidad = 3f; 
        float valor = Mathf.PingPong(Time.time * velocidad, 5);
    }
}
