using System;

namespace SchoolNotesApp.StudentFeature.Domain
{
    [Serializable]
    /// <summary>
    /// DTO que representa la estructura de un estudiante según el origen de datos.
    /// </summary>
    public sealed class StudentDto
    {
        public string nombre;
        public string apellido;
        public string codigo;
        public string correo;
        public float notaFinal;

        public Student ToStudent()
        {
            return new Student(nombre, apellido, codigo, correo, notaFinal);
        }
    }
}