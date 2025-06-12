using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class BotonConSonido : MonoBehaviour, IPointerClickHandler
{
    public AudioClip sonidoBoton;
    private AudioSource audioSource;

    void Start()
    {
        GameObject audioGO = GameObject.Find("UIAudio");
        if (audioGO != null)
        {
            audioSource = audioGO.GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            audioSource = FindObjectOfType<AudioSource>();
            Debug.LogWarning("No se encontró 'UIAudio', usando el primer AudioSource que encontró.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (sonidoBoton != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoBoton);
        }
    }
}
