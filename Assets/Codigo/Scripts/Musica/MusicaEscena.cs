using UnityEngine;

public class MusicaEscena : MonoBehaviour
{
    public AudioClip musicaDeEstaEscena;

    void Start()
    {
        if (MusicaManager.instancia != null)
        {
            MusicaManager.instancia.ReproducirDesdeCero(musicaDeEstaEscena);
        }
    }
}
