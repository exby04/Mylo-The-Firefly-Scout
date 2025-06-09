using UnityEngine;
using System.Collections;

public class MyloAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip[] pasosClips;
    public AudioClip[] quejidos; 
    public AudioClip respiracionCansado;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SonidoPasoAleatorio()
    {
        if (pasosClips != null && pasosClips.Length > 0)
        {
            int index = Random.Range(0, pasosClips.Length);
            audioSource.PlayOneShot(pasosClips[index]);
        }
    }

    public void SonidoCansancio()
    {
        if (respiracionCansado != null)
            audioSource.PlayOneShot(respiracionCansado);
    }

    public void SonidoCansancioConDuracion(float duracion)
    {
        if (respiracionCansado != null)
            StartCoroutine(ReproducirRespiracionPorTiempo(duracion));
    }

    private IEnumerator ReproducirRespiracionPorTiempo(float duracion)
    {
        audioSource.clip = respiracionCansado;
        audioSource.Play();

        yield return new WaitForSeconds(duracion);

        audioSource.Stop();
        audioSource.clip = null;
    }


    public void SonidoDañoAleatorio()
    {
        if (quejidos != null && quejidos.Length > 0)
        {
            int index = Random.Range(0, quejidos.Length);
            audioSource.PlayOneShot(quejidos[index]);
        }
    }
}
