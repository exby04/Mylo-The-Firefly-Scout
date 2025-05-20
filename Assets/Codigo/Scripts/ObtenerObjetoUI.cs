using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjetoObtenidoUI : MonoBehaviour
{
    public static ObjetoObtenidoUI instance;

    public GameObject panelObjeto;
    public Image imagenObjeto;
    public float duracion = 2f;

    private CanvasGroup canvasGroup;
    private RectTransform panelRect;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        canvasGroup = panelObjeto.GetComponent<CanvasGroup>();
        panelRect = panelObjeto.GetComponent<RectTransform>();
    }

    public void Mostrar(Sprite sprite)
    {
        StopAllCoroutines();
        imagenObjeto.sprite = sprite;
        StartCoroutine(MostrarYEsconder());
    }

    IEnumerator MostrarYEsconder()
    {
        panelObjeto.SetActive(true);

        canvasGroup.alpha = 0f;
        panelRect.localScale = Vector3.zero;

        float t = 0f;
        float fadeDuration = 0.3f;

        while (t < fadeDuration)
        {
            float eased = Mathf.SmoothStep(0, 1, t / fadeDuration);
            canvasGroup.alpha = eased;
            panelRect.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, eased);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
        panelRect.localScale = Vector3.one;

        yield return new WaitForSecondsRealtime(duracion);

        t = 0f;
        while (t < fadeDuration)
        {
            float eased = Mathf.SmoothStep(0, 1, t / fadeDuration);
            canvasGroup.alpha = 1f - eased;
            panelRect.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, eased);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        panelRect.localScale = Vector3.zero;
        panelObjeto.SetActive(false);
    }
}
