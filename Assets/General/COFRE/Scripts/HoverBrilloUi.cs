using UnityEngine;
using UnityEngine.EventSystems;

public class HoverBrilloUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject brillo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (brillo != null)
            brillo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (brillo != null)
            brillo.SetActive(false);
    }

    public void DesactivarBrillo()
    {
        if (brillo != null)
            brillo.SetActive(false);
    }

}
