using UnityEngine;
public class FireflyLight : MonoBehaviour
{
    private Light fireflyLight;
    public float velocidad = 2000f; 

    void Start()
    {
        fireflyLight = GetComponent<Light>();
        if (fireflyLight == null)
        {
            Debug.LogError("No se encontró el componente Light en este GameObject.");
        }
    }

    void Update()
    {
        if (fireflyLight != null)
        {
            fireflyLight.intensity = Mathf.PingPong(Time.time * velocidad, 20f);
        }
    }
}
