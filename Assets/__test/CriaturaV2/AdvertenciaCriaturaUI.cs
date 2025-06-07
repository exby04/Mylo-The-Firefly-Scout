using UnityEngine;
using UnityEngine.UI;

public class AdvertenciaCriaturaUI : MonoBehaviour
{
    [SerializeField] private GameObject uiIconoObjeto; 
    [SerializeField] private float tiempoTotalAdvertencia = 10f;

    private Image uiIcono;
    private float tiempoRestante;
    private bool mostrando = false;

    void Start()
    {
        if (uiIconoObjeto != null)
        {
            uiIcono = uiIconoObjeto.GetComponent<Image>();
            if (uiIcono == null)
                uiIcono = uiIconoObjeto.GetComponentInChildren<Image>();

            if (uiIcono != null)
                uiIcono.enabled = false;
        }
    }

    void Update()
    {
        if (!mostrando || uiIcono == null)
            return;

        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0f)
        {
            uiIcono.enabled = false;
            mostrando = false;
            return;
        }

        float frecuencia = Mathf.Lerp(0.3f, 0.05f, 1 - (tiempoRestante / tiempoTotalAdvertencia));
        float t = Mathf.PingPong(Time.time, frecuencia);
        uiIcono.enabled = t > frecuencia / 2f;
    }

    public void IniciarAdvertencia()
    {
        tiempoRestante = tiempoTotalAdvertencia;
        mostrando = true;

        if (uiIcono != null)
            uiIcono.enabled = true;
    }

    public void CancelarAdvertencia()
    {
        mostrando = false;

        if (uiIcono != null)
            uiIcono.enabled = false;
    }
}
