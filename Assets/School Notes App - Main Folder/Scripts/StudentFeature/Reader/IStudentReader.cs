using System.Collections.Generic;
using SchoolNotesApp.StudentFeature.Domain;

namespace SchoolNotesApp.StudentFeature.Reader
{
    public interface IStudentReader
    {
        IReadOnlyList<Student> ReadStudents();
    }
}