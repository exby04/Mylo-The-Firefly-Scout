using UnityEngine;
using UnityEngine.UI;

public class AbrirCofre : MonoBehaviour, IInteractable
{
    private bool abierto = false;
    private static bool llaveObtenida = false;
    private static int cofresAbiertos = 0;

    public float tiempoParaAbrir = 2f;

    public float HoldDuration => tiempoParaAbrir;

    public Animator animator;

    [SerializeField] private AudioSource sonidoCofre;

    private void Start()
    {
        cofresAbiertos = 0;
    }

    public void OnInteract()
    {
        if (abierto) return;

        Abrir();
    }

    void Abrir()
    {
        abierto = true;

        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetTrigger("AbrirCofre");

        if(sonidoCofre != null)
        {
            sonidoCofre.Play();
        }

        cofresAbiertos++;

        if (!llaveObtenida && cofresAbiertos == 6)
        {
            llaveObtenida = true;
            Inventario.instance.RecogerLlave();
            Debug.Log("¡Has encontrado la llave por obligación!");
        }
        else if (!llaveObtenida && Random.Range(1, 8) == 1)
        {
            llaveObtenida = true;
            Inventario.instance.RecogerLlave();
            Debug.Log("¡Has encontrado una llave!");
        }
        else
        {
            string actual = Inventario.instance.ObtenerPowerUp();
            string nuevoPowerUp;

            if (actual == "Velocidad")
                nuevoPowerUp = "Vision";
            else if (actual == "Vision")
                nuevoPowerUp = "Velocidad";
            else
                nuevoPowerUp = (Random.Range(0, 2) == 0) ? "Velocidad" : "Vision";

            if (!string.IsNullOrEmpty(actual))
            {
                Sprite spriteActual = Inventario.instance.GetSpriteFromName(actual);
                Sprite spriteNuevo = Inventario.instance.GetSpriteFromName(nuevoPowerUp);

                PanelCambioPowerUpUI.instance.MostrarPanel(actual, nuevoPowerUp, spriteActual, spriteNuevo);
                Debug.Log("Mostrando panel de cambio para power-up.");
            }
            else
            {
                Inventario.instance.RecogerPowerUp(nuevoPowerUp);
                Debug.Log("¡Has obtenido un Power-Up de " + nuevoPowerUp + "!");
            }
        }
    }
    public bool EstaAbierto()
    {
        return abierto;
    }

}
