using System;

namespace SchoolNotesApp.StudentFeature.Domain
{
    public sealed class Student
    {
        private const float _minGrade = 0f;
        private const float _maxGrade = 5f;

        private string _nombre;
        private string _apellido;
        private string _codigo;
        private string _correo;
        private float _nota;
        private StudentState _estado;

        public string Nombre => _nombre;
        public string Apellido => _apellido;
        public string Codigo => _codigo;
        public string Correo => _correo;
        public float Nota => _nota;
        public StudentState Estado => _estado;

        public string NombreCompleto => $"{_nombre} {_apellido}".Trim();

        public Student(string nombre, string apellido, string codigo, string correo, float nota)
        {
            SetData(nombre, apellido, codigo, correo, nota);
        }

        public void SetData(string nombre, string apellido, string codigo, string correo, float nota)
        {
            _nombre = nombre;
            _apellido = apellido;
            _codigo = codigo;
            _correo = correo;
            _nota = nota;
            _estado = StudentState.NoCalificado;
        }

        public bool ValidateData()
        {
            return !string.IsNullOrWhiteSpace(_nombre)
                && !string.IsNullOrWhiteSpace(_apellido)
                && !string.IsNullOrWhiteSpace(_codigo)
                && !string.IsNullOrWhiteSpace(_correo)
                && _correo.Contains("@")
                && _nota >= _minGrade
                && _nota <= _maxGrade;
        }

        public StudentState ValidateState(float passingGrade)
        {
            return _nota >= passingGrade ? StudentState.Aprobado : StudentState.Reprobado;
        }

        public void SetState(StudentState state)
        {
            _estado = state;
        }
    }
}