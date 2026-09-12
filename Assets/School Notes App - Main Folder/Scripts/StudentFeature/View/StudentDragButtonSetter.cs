using System.Globalization;
using SchoolNotesApp.StudentFeature.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SchoolNotesApp.StudentFeature.View
{
    /// <summary>
    /// Configura un botón de arrastre de estudiante mostrando su nombre completo
    /// y sus iniciales con color, y expone la referencia al estudiante asociado.
    /// </summary>
    public sealed class StudentDragButtonSetter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _noteText;
        [SerializeField] private TextMeshProUGUI _initialText;
        [SerializeField] private Image _initialCircleImage;

        public Student Student { get; private set; }

        public void Configure(Student student)
        {
            Student = student;
            _nameText.text = student.NombreCompleto;
            _noteText.text = $"Nota: {student.Nota.ToString("0.0", CultureInfo.InvariantCulture)}";
            _initialText.text = StudentVisuals.GetInitials(student);
            _initialCircleImage.color = StudentVisuals.GetRandomInitialColor();
        }
    }
}