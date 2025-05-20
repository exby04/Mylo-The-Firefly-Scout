using UnityEngine;
using UnityEngine.UI;
using TMPro; 
public class OpcionesDebugMenu : MonoBehaviour
{
    [Header("Panel de Opciones")]
    public GameObject panelOpciones;

    [Header("Referencias de UI")]
    

public TMP_InputField inputTemporizadorLuz;
public TMP_InputField inputTemporizadorMurcielagos;


    [Header("Referencias de Juego")]
    public AtenuacionLuz atenuacionLuz;           
    public BatAttackTimer batTimer;              
    public Inventario inventario;                 

   
    public void AbrirOpciones()
{
    panelOpciones.SetActive(true);
    Time.timeScale = 0f; 

    
    inputTemporizadorLuz.text = atenuacionLuz.tiempo.ToString();
    inputTemporizadorMurcielagos.text = batTimer.attackInterval.ToString();

    Debug.Log("Panel de opciones abierto y juego pausado.");
}



    
    public void CerrarOpciones()
{
    panelOpciones.SetActive(false);
    Time.timeScale = 1f; 

    Debug.Log("Panel de opciones cerrado y juego reanudado.");
}


    
    public void ActualizarTemporizadorLuz()
{
    if (float.TryParse(inputTemporizadorLuz.text, out float nuevoValor))
    {
        atenuacionLuz.tiempo = nuevoValor;
        atenuacionLuz.ReiniciarLuz(); 
        Debug.Log("Nuevo tiempo de atenuación de luz: " + nuevoValor);
    }
    else
    {
        Debug.LogWarning("Valor inválido para el temporizador de luz");
    }
}


    
    public void ActualizarTemporizadorMurcielagos()
    {
        if (float.TryParse(inputTemporizadorMurcielagos.text, out float nuevoValor))
        {
            batTimer.attackInterval = nuevoValor;
            Debug.Log("Nuevo tiempo de ataque de murciélagos: " + nuevoValor);
        }
        else
        {
            Debug.LogWarning("Valor inválido para el temporizador de murciélagos");
        }
    }

    
    public void DarLlave()
{
    Time.timeScale = 1f;
    inventario.RecogerLlave();
    Time.timeScale = 0f;
    Debug.Log("Llave otorgada desde el menú de opciones");
}

public void ActivarPowerUpLuz()
{
    Time.timeScale = 1f;
    inventario.RecogerPowerUp("Vision");
    Time.timeScale = 0f;
    Debug.Log("Power-Up de Luz activado desde el menú");
}

public void ActivarPowerUpVelocidad()
{
    Time.timeScale = 1f;
    inventario.RecogerPowerUp("Velocidad");
    Time.timeScale = 0f;
    Debug.Log("Power-Up de Velocidad activado desde el menú");
}


}
