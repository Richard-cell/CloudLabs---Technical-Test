using System;

namespace SchoolNotesApp.StudentFeature.Domain
{
    [Serializable]
    public sealed class StudentListDto
    {
        public StudentDto[] estudiantes;
    }
}