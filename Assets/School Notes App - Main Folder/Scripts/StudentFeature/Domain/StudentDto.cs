using System;

namespace SchoolNotesApp.StudentFeature.Domain
{
    [Serializable]
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