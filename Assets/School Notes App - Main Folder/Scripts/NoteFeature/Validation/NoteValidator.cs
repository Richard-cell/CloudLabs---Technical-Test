using System;
using System.Collections.Generic;
using SchoolNotesApp.StudentFeature.Domain;

namespace SchoolNotesApp.NoteFeature.Validation
{
    /// <summary>
    /// Valida las calificaciones comparando el estado de cada estudiante con la
    /// nota de aprobación configurada y devuelve el resultado de la validación.
    /// </summary>
    public sealed class NoteValidator
    {
        private readonly float _passingGrade;

        public NoteValidator(float passingGrade)
        {
            _passingGrade = passingGrade;
        }

        public NoteValidationResult Validate(IReadOnlyList<Student> students)
        {
            var wronglyApproved = new List<Student>();
            var wronglyFailed = new List<Student>();
            var ungradedStudents = new List<Student>();

            foreach (Student student in students)
            {
                if (student.Estado == StudentState.NoCalificado)
                {
                    ungradedStudents.Add(student);
                    continue;
                }

                StudentState expectedState = student.ValidateState(_passingGrade);
                if (student.Estado == expectedState)
                {
                    continue;
                }

                if (student.Estado == StudentState.Aprobado)
                {
                    wronglyApproved.Add(student);
                }
                else
                {
                    wronglyFailed.Add(student);
                }
            }

            return new NoteValidationResult(wronglyApproved, wronglyFailed, ungradedStudents);
        }
    }
}