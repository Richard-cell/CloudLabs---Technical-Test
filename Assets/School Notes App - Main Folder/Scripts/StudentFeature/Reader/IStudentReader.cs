using System.Collections.Generic;
using SchoolNotesApp.StudentFeature.Domain;

namespace SchoolNotesApp.StudentFeature.Reader
{
    /// <summary>
    /// Contrato de lectura de estudiantes que abstrae el origen de los datos
    /// (archivo JSON, servicio remoto, etc.) de la lógica del juego.
    /// </summary>
    public interface IStudentReader
    {
        IReadOnlyList<Student> ReadStudents();
    }
}