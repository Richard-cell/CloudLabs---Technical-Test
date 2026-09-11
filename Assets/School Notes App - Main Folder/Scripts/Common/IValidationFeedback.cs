namespace SchoolNotesApp.Common
{
    public interface IValidationFeedback
    {
        bool IsValidationSuccessful { get; }
        void NotifyValidationRequired();
    }
}