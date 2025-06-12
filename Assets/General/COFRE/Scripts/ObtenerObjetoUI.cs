using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjetoObtenidoUI : MonoBehaviour
{
    public static ObjetoObtenidoUI instance;

    [Header("Jerarquía visual")]
    public GameObject panelObjeto;
    public Image imagenObjeto;
    public GameObject brilloObjeto;

    [Header("Slots de destino")]
    public RectTransform slotPowerUp;
    public RectTransform slotLlave;

    [Header("Tiempos")]
    public float duracionFlotando = 1.5f;
    public float duracionMovimiento = 0.5f;

    [Header("Escalas visuales")]
    public Vector3 escalaInicio = Vector3.zero;
    public Vector3 escalaPopup = Vector3.one * 1.2f;
    public Vector3 escalaSlotFinal = Vector3.one * 0.6f;

    [Header("Ajustes visuales")]
    [Range(-200f, 200f)]
    public float offsetFinalY = -30f;

    private CanvasGroup canvasGroup;
    private RectTransform panelRect;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        canvasGroup = panelObjeto.GetComponent<CanvasGroup>();
        panelRect = panelObjeto.GetComponent<RectTransform>();
    }

    /// <summary>
    /// Muestra el objeto, lo anima, y luego ejecuta el callback cuando llega al HUD.
    /// </summary>
    public void Mostrar(Sprite sprite, string tipo, System.Action callbackFinal)
    {
        bool esLlave = tipo.ToLower() == "llave";

        StopAllCoroutines();
        imagenObjeto.sprite = sprite;
        brilloObjeto.SetActive(true);

        StartCoroutine(MostrarYMoverA(esLlave ? slotLlave : slotPowerUp, callbackFinal));
    }

    IEnumerator MostrarYMoverA(RectTransform destino, System.Action callbackFinal)
    {
        panelObjeto.SetActive(true);

        panelRect.position = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        panelRect.localScale = escalaInicio;
        canvasGroup.alpha = 0f;

        float t = 0f;
        float fadeDuration = 0.3f;

        // Aparece
        while (t < fadeDuration)
        {
            float eased = Mathf.SmoothStep(0, 1, t / fadeDuration);
            canvasGroup.alpha = eased;
            panelRect.localScale = Vector3.Lerp(escalaInicio, escalaPopup, eased);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
        panelRect.localScale = escalaPopup;

        yield return new WaitForSecondsRealtime(duracionFlotando);

        // Mover hacia destino
        Vector3 startPos = panelRect.position;
        Vector3 endPos = destino.position + new Vector3(0, offsetFinalY, 0);
        Vector3 startScale = panelRect.localScale;
        Vector3 endScale = escalaSlotFinal;

        t = 0f;
        while (t < duracionMovimiento)
        {
            float eased = Mathf.SmoothStep(0, 1, t / duracionMovimiento);
            panelRect.position = Vector3.Lerp(startPos, endPos, eased);
            panelRect.localScale = Vector3.Lerp(startScale, endScale, eased);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        panelRect.position = endPos;
        panelRect.localScale = endScale;

        // Final
        canvasGroup.alpha = 0f;
        panelObjeto.SetActive(false);
        brilloObjeto.SetActive(false);

        // ✅ Entrega el objeto al jugador
        callbackFinal?.Invoke();
    }
}
