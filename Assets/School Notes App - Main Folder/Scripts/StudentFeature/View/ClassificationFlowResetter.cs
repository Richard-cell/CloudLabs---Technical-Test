using SchoolNotesApp.NoteFeature.View;
using UnityEngine;
using UnityEngine.UI;

namespace SchoolNotesApp.StudentFeature.View
{
    /// <summary>
    /// Reinicia el flujo de clasificación: devuelve los botones a su contenedor
    /// original, restaura los estados de los estudiantes y limpia la validación.
    /// </summary>
    public sealed class ClassificationFlowResetter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button _resetButton;
        [SerializeField] private StudentDragButtonSpawner _dragButtonSpawner;
        [SerializeField] private NoteValidatorMessageDrawer _validatorMessageDrawer;

        private void Awake()
        {
            if (_resetButton != null)
            {
                _resetButton.onClick.AddListener(OnResetClicked);
            }
        }

        private void OnDestroy()
        {
            if (_resetButton != null)
            {
                _resetButton.onClick.RemoveListener(OnResetClicked);
            }
        }

        private void OnResetClicked()
        {
            if (_dragButtonSpawner != null)
            {
                _dragButtonSpawner.ResetFlow();
            }

            if (_validatorMessageDrawer != null)
            {
                _validatorMessageDrawer.ResetValidation();
            }
        }
    }
}