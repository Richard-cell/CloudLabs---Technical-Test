using System;
using SchoolNotesApp.StudentFeature.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SchoolNotesApp.StudentFeature.View
{
    /// <summary>
    /// Aplica el diseño visual (colores e íconos) de un elemento de interfaz
    /// según el estado de calificación del estudiante.
    /// </summary>
    public sealed class StudentStateDesignSetter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _stateText;
        [SerializeField] private Image _noteImage;

        [Header("Design")]
        [SerializeField] private Color _approvedColor = new Color(0.15f, 0.65f, 0.25f, 1f);
        [SerializeField] private Color _failedColor = new Color(0.75f, 0.2f, 0.2f, 1f);
        [SerializeField] private Color _pendingColor = new Color(0.45f, 0.45f, 0.45f, 1f);

        public void Apply(StudentState state)
        {
            switch (state)
            {
                case StudentState.Aprobado:
                    ApplyDesign(_approvedColor, "Aprobado");
                    break;
                case StudentState.Reprobado:
                    ApplyDesign(_failedColor, "Reprobado");
                    break;
                default:
                    ApplyDesign(_pendingColor, "NA");
                    break;
            }
        }

        private void ApplyDesign(Color color, string message)
        {
            _stateText.text = message;
            _stateText.color = color;
            _noteImage.color = color;
        }
    }
}