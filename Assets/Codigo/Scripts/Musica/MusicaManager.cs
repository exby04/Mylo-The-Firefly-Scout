using UnityEngine;
using System.Collections;

public class MusicaManager : MonoBehaviour
{
    public static MusicaManager instancia;

    public AudioSource source;
    public float volumenMaximo = 1f;
    public float duracionFade = 1.5f;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);

        if (source == null)
            source = gameObject.AddComponent<AudioSource>();

        source.loop = true;
        source.playOnAwake = false;
        source.volume = 0f;
    }

    public void ReproducirDesdeCero(AudioClip nuevaMusica)
    {
        if (nuevaMusica == null) return;

        StopAllCoroutines();
        StartCoroutine(FadeCambio(nuevaMusica));
    }

    private IEnumerator FadeCambio(AudioClip nuevoClip)
    {
        float t = 0f;
        while (t < duracionFade)
        {
            source.volume = Mathf.Lerp(volumenMaximo, 0f, t / duracionFade);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        source.Stop();
        source.clip = nuevoClip;
        source.Play();

        t = 0f;
        while (t < duracionFade)
        {
            source.volume = Mathf.Lerp(0f, volumenMaximo, t / duracionFade);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        source.volume = volumenMaximo;
    }

    public void DetenerMusica()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutYParar());
    }

    private IEnumerator FadeOutYParar()
    {
        float t = 0f;
        float volInicial = source.volume;
        while (t < duracionFade)
        {
            source.volume = Mathf.Lerp(volInicial, 0f, t / duracionFade);
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        source.volume = 0f;
        source.Stop();
        source.clip = null;
    }
}
