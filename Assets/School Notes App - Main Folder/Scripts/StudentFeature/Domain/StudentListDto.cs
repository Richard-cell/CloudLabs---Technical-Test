using System;

namespace SchoolNotesApp.StudentFeature.Domain
{
    [Serializable]
    /// <summary>
    /// DTO raíz que contiene la lista de estudiantes deserializada del JSON.
    /// </summary>
    public sealed class StudentListDto
    {
        public StudentDto[] estudiantes;
    }
}