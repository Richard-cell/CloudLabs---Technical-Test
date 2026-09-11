using UnityEngine;
using UnityEngine.UI;

namespace SchoolNotesApp.Common
{
    public sealed class PanelNavigator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button _continueButton;
        [SerializeField] private GameObject _notesPanel;
        [SerializeField] private GameObject _classificationPanel;

        private IValidationFeedback _validationFeedback;

        public void Initialize(IValidationFeedback validationFeedback)
        {
            _validationFeedback = validationFeedback;
            _continueButton.onClick.AddListener(OnContinueClicked);
        }

        private void OnDestroy()
        {
            if (_continueButton != null)
            {
                _continueButton.onClick.RemoveListener(OnContinueClicked);
            }
        }

        private void OnContinueClicked()
        {
            if (_validationFeedback == null)
            {
                return;
            }

            if (_validationFeedback.IsValidationSuccessful)
            {
                _notesPanel.SetActive(false);
                _classificationPanel.SetActive(true);
            }
            else
            {
                _validationFeedback.NotifyValidationRequired();
            }
        }
    }
}