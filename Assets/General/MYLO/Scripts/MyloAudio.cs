using UnityEngine;
using System.Collections;

public class MyloAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip[] pasosClips;            // Varios sonidos sueltos
    public AudioClip[] quejidos;
    public AudioClip respiracionCansado;

    [Header("Fuentes de sonido")]
    public AudioSource pasosSource;           // Un único AudioSource
    public AudioSource efectosSource;

    void Start()
    {
        if (pasosSource == null || efectosSource == null)
            Debug.LogError("Faltan AudioSources asignados");
    }

    public void SonidoPasoAleatorio()
    {
        if (pasosClips != null && pasosClips.Length > 0)
        {
            int index = Random.Range(0, pasosClips.Length);
            pasosSource.PlayOneShot(pasosClips[index]); // 👈 aquí usas el array perfectamente
        }
    }

    public void SonidoCansancio()
    {
        if (respiracionCansado != null)
            efectosSource.PlayOneShot(respiracionCansado);
    }

    public void SonidoCansancioConDuracion(float duracion)
    {
        if (respiracionCansado != null)
            StartCoroutine(ReproducirRespiracionPorTiempo(duracion));
    }

    private IEnumerator ReproducirRespiracionPorTiempo(float duracion)
    {
        efectosSource.clip = respiracionCansado;
        efectosSource.Play();

        yield return new WaitForSeconds(duracion);

        efectosSource.Stop();
        efectosSource.clip = null;
    }

    public void SonidoDañoAleatorio()
    {
        if (quejidos != null && quejidos.Length > 0)
        {
            int index = Random.Range(0, quejidos.Length);
            efectosSource.PlayOneShot(quejidos[index]);
        }
    }
}
