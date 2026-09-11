using System.Globalization;
using SchoolNotesApp.StudentFeature.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SchoolNotesApp.StudentFeature.View
{
    public sealed class StudentInfoPanelSetter : MonoBehaviour
    {
        [Header("Name Column")]
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _initialText;
        [SerializeField] private Image _initialCircleImage;

        [Header("Initial Color")]
        [Range(0f, 1f)]
        [SerializeField] private float _initialSaturation = 0.65f;
        [Range(0f, 1f)]
        [SerializeField] private float _initialValue = 0.5f;

        [Header("Info Column")]
        [SerializeField] private TextMeshProUGUI _codeText;
        [SerializeField] private TextMeshProUGUI _emailText;

        [Header("Note Column")]
        [SerializeField] private TextMeshProUGUI _noteText;

        public void Configure(Student student)
        {
            _nameText.text = student.NombreCompleto;
            _initialText.text = GetInitials(student);
            _codeText.text = student.Codigo;
            _emailText.text = student.Correo;
            _noteText.text = student.Nota.ToString("0.0", CultureInfo.InvariantCulture);
            ApplyRandomInitialColor();
        }

        private void ApplyRandomInitialColor()
        {
            float hue = Random.value;
            _initialCircleImage.color = Color.HSVToRGB(hue, _initialSaturation, _initialValue);
        }

        private static string GetInitials(Student student)
        {
            char firstNameInitial = student.Nombre.Length > 0 ? char.ToUpperInvariant(student.Nombre[0]) : ' ';
            char lastNameInitial = student.Apellido.Length > 0 ? char.ToUpperInvariant(student.Apellido[0]) : ' ';
            return $"{firstNameInitial}{lastNameInitial}";
        }
    }
}