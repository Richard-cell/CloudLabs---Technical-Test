namespace SchoolNotesApp.Common
{
    /// <summary>
    /// Contrato que expone si la validación fue satisfactoria y notifica
    /// cuando se requiere validar antes de continuar en el flujo.
    /// </summary>
    public interface IValidationFeedback
    {
        bool IsValidationSuccessful { get; }
        void NotifyValidationRequired();
    }
}