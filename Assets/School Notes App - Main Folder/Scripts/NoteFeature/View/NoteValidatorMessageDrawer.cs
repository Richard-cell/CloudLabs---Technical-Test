using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SchoolNotesApp.Common;
using SchoolNotesApp.NoteFeature.Validation;
using SchoolNotesApp.StudentFeature.Domain;
using SchoolNotesApp.StudentFeature.Reader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SchoolNotesApp.NoteFeature.View
{
    /// <summary>
    /// Presenta en pantalla los mensajes de validación de notas (éxito o errores)
    /// y notifica cuando la validación es requerida para continuar.
    /// </summary>
    public sealed class NoteValidatorMessageDrawer : MonoBehaviour, IValidationFeedback
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private GameObject _resultPanel;
        [SerializeField] private Button _validateButton;
        [SerializeField] private Image _resultImage;

        [Header("Design")]
        [SerializeField] private Color _successColor = new Color(0.15f, 0.65f, 0.25f, 1f);
        [SerializeField] private Color _errorColor = new Color(0.75f, 0.2f, 0.2f, 1f);
        [SerializeField] private Sprite _successSprite;
        [SerializeField] private Sprite _errorSprite;

        private NoteValidator _validator;
        private IStudentReader _studentReader;
        private NoteValidationResult _lastValidationResult;

        public bool IsValidationSuccessful =>
            _lastValidationResult != null && !_lastValidationResult.HasErrors;

        private void Awake()
        {
            if (_resultPanel != null)
            {
                _resultPanel.SetActive(false);
            }
        }

        public void NotifyValidationRequired()
        {
            ShowResultPanel();
            _messageText.text = _lastValidationResult == null
                ? "Valida las notas antes de continuar."
                : "Corrige los errores de calificación antes de continuar.";
            _messageText.color = _errorColor;

            if (_resultImage != null)
            {
                _resultImage.sprite = _errorSprite;
            }
        }

        public void Initialize(NoteValidator validator, IStudentReader studentReader)
        {
            _validator = validator;
            _studentReader = studentReader;

            if (_validateButton != null)
            {
                _validateButton.onClick.AddListener(OnValidateClicked);
            }
        }

        private void OnDestroy()
        {
            if (_validateButton != null)
            {
                _validateButton.onClick.RemoveListener(OnValidateClicked);
            }
        }

        private void OnValidateClicked()
        {
            if (_validator == null || _studentReader == null)
            {
                return;
            }

            Draw(_validator.Validate(_studentReader.ReadStudents()));
        }

        public void ResetValidation()
        {
            _lastValidationResult = null;

            if (_messageText != null)
            {
                _messageText.text = string.Empty;
            }

            if (_resultPanel != null)
            {
                _resultPanel.SetActive(false);
            }
        }

        public void Draw(NoteValidationResult result)
        {
            _lastValidationResult = result;
            ShowResultPanel();
            _messageText.text = BuildMessage(result);
            _messageText.color = result.HasErrors ? _errorColor : _successColor;

            if (_resultImage != null)
            {
                _resultImage.sprite = result.HasErrors ? _errorSprite : _successSprite;
            }
        }

        private void ShowResultPanel()
        {
            if (_resultPanel != null)
            {
                _resultPanel.SetActive(true);
            }
        }

        private static string BuildMessage(NoteValidationResult result)
        {
            if (result.UngradedStudents.Count > 0)
            {
                return $"Califica a todos los estudiantes antes de validar: {JoinNames(result.UngradedStudents, false)}";
            }

            if (!result.HasErrors)
            {
                return "Todos los estudiantes fueron calificados correctamente.";
            }

            var builder = new StringBuilder();
            if (result.WronglyApproved.Count > 0)
            {
                builder.AppendLine($"Aprobaste a quien no debías: {JoinNames(result.WronglyApproved, true)}");
            }

            if (result.WronglyFailed.Count > 0)
            {
                builder.AppendLine($"Reprobaste a quien debías aprobar: {JoinNames(result.WronglyFailed, true)}");
            }

            return builder.ToString().TrimEnd();
        }

        private static string JoinNames(IReadOnlyList<Student> students, bool includeNote)
        {
            string[] names = new string[students.Count];
            for (int i = 0; i < students.Count; i++)
            {
                Student student = students[i];
                names[i] = includeNote
                    ? $"{student.NombreCompleto} ({student.Nota.ToString("0.0", CultureInfo.InvariantCulture)})"
                    : student.NombreCompleto;
            }

            return string.Join(", ", names);
        }
    }
}