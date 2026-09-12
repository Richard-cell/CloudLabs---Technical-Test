using SchoolNotesApp.StudentFeature.Domain;
using UnityEngine;

namespace SchoolNotesApp.StudentFeature.View
{
    /// <summary>
    /// Servicio estático de utilidades visuales para estudiantes: cálculo de las
    /// iniciales del nombre y generación de colores de iniciales.
    /// </summary>
    public static class StudentVisuals
    {
        private const float _saturation = 0.65f;
        private const float _value = 0.5f;

        public static string GetInitials(Student student)
        {
            char firstNameInitial = student.Nombre.Length > 0 ? char.ToUpperInvariant(student.Nombre[0]) : ' ';
            char lastNameInitial = student.Apellido.Length > 0 ? char.ToUpperInvariant(student.Apellido[0]) : ' ';
            return $"{firstNameInitial}{lastNameInitial}";
        }

        public static Color GetRandomInitialColor()
        {
            return Color.HSVToRGB(Random.value, _saturation, _value);
        }
    }
}