public interface IInteractable
{
    float HoldDuration { get; } // Tiempo requerido para completar la interacción
    void OnInteract();          // Qué pasa cuando se completa
}
