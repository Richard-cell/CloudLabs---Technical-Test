using System.Globalization;
using SchoolNotesApp.StudentFeature.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SchoolNotesApp.StudentFeature.View
{
    /// <summary>
    /// Configura el panel de información de un estudiante con su nombre, notas
    /// e iniciales, y refleja visualmente su estado de calificación.
    /// </summary>
    public sealed class StudentInfoPanelSetter : MonoBehaviour
    {
        [Header("Name Column")]
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _initialText;
        [SerializeField] private Image _initialCircleImage;

        [Header("Info Column")]
        [SerializeField] private TextMeshProUGUI _codeText;
        [SerializeField] private TextMeshProUGUI _emailText;

        [Header("Note Column")]
        [SerializeField] private TextMeshProUGUI _noteText;

        public void Configure(Student student)
        {
            _nameText.text = student.NombreCompleto;
            _initialText.text = StudentVisuals.GetInitials(student);
            _codeText.text = student.Codigo;
            _emailText.text = student.Correo;
            _noteText.text = student.Nota.ToString("0.0", CultureInfo.InvariantCulture);
            ApplyRandomInitialColor();
        }

        private void ApplyRandomInitialColor()
        {
            _initialCircleImage.color = StudentVisuals.GetRandomInitialColor();
        }
    }
}