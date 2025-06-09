using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public static Inventario instance;

    private string powerUpActual = null;
    private bool tieneLlave = false;

    [Header("Sprites")]
    public Sprite llaveSprite;
    public Sprite powerUpLuzSprite;
    public Sprite powerUpRapidezSprite;
    public Sprite powerUpVacioSprite;
    public Sprite llaveSlotVacioSprite;

    [Header("HUD")]
    public Image keyImage;
    public Image powerUpImage;
    public GameObject textoSpacebar;

    [Header("Referencias")]
    public PowerUpLuz powerUpLuz;
    public SpeedPowerUpController speedPowerUpController;
    [Header("Sonido")]
    public AudioClip sonidoRecogerObjeto;
    public AudioSource audioSource;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        if (keyImage != null && llaveSlotVacioSprite != null)
        {
            keyImage.sprite = llaveSlotVacioSprite;
            keyImage.color = Color.gray;
        }

        if (powerUpImage != null && powerUpVacioSprite != null)
        {
            powerUpImage.sprite = powerUpVacioSprite;
            powerUpImage.color = Color.gray;
        }

        if (textoSpacebar != null)
            textoSpacebar.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ActivarPowerUp();
    }

    public void RecogerLlave()
    {
        tieneLlave = true;
        keyImage.sprite = llaveSprite;
        keyImage.color = Color.white;

        ObjetoObtenidoUI.instance?.Mostrar(llaveSprite);

        Transform jugador = FindFirstObjectByType<PlayerController>().transform;
        Animator animador = jugador.GetComponent<Animator>();

        CameraZoomController.instance?.ZoomAndPauseWithAnimation(jugador, animador);
        if (audioSource != null && sonidoRecogerObjeto != null)
        audioSource.PlayOneShot(sonidoRecogerObjeto);

    }

    public void RecogerPowerUp(string powerUp)
    {
        powerUpActual = powerUp;

        Sprite sprite = powerUpVacioSprite;
        if (powerUp == "Vision")
            sprite = powerUpLuzSprite;
        else if (powerUp == "Velocidad")
            sprite = powerUpRapidezSprite;

        powerUpImage.sprite = sprite;
        powerUpImage.color = Color.white;

        ObjetoObtenidoUI.instance?.Mostrar(sprite);

        if (textoSpacebar != null)
            textoSpacebar.SetActive(true);

        Transform jugador = FindFirstObjectByType<PlayerController>().transform;
        Animator animador = jugador.GetComponent<Animator>();

        CameraZoomController.instance?.ZoomAndPauseWithAnimation(jugador, animador);
        if (audioSource != null && sonidoRecogerObjeto != null)
        audioSource.PlayOneShot(sonidoRecogerObjeto);

    }

    public void ActivarPowerUp()
    {
        if (powerUpActual == null) return;

        switch (powerUpActual)
        {
            case "Vision":
                powerUpLuz?.Activar();
                break;
            case "Velocidad":
                speedPowerUpController?.GiveSpeedPowerUp();
                break;
        }

        powerUpActual = null;
        powerUpImage.sprite = powerUpVacioSprite;
        powerUpImage.color = Color.gray;

        if (textoSpacebar != null)
            textoSpacebar.SetActive(false);
    }

    public bool TieneLlave()
    {
        return tieneLlave;
    }

    public string ObtenerPowerUp()
    {
        return powerUpActual;
    }

    public void IntercambiarPowerUp(string nuevoPowerUp)
    {
        RecogerPowerUp(nuevoPowerUp);
    }

    public Sprite GetSpriteFromName(string nombre)
    {
        switch (nombre)
        {
            case "Vision": return powerUpLuzSprite;
            case "Velocidad": return powerUpRapidezSprite;
            default: return powerUpVacioSprite;
        }
    }

}
