using System;
using System.Collections.Generic;
using System.IO;
using SchoolNotesApp.StudentFeature.Domain;
using UnityEngine;

namespace SchoolNotesApp.StudentFeature.Reader
{
    public sealed class JSONStudentReader : IStudentReader
    {
        private const string _fileName = "estudiantes.json";

        private readonly string _filePath;
        private IReadOnlyList<Student> _students;

        public JSONStudentReader(string filePath)
        {
            _filePath = filePath;
        }

        public static JSONStudentReader FromStreamingAssets()
        {
            string path = Path.Combine(Application.streamingAssetsPath, _fileName);
            return new JSONStudentReader(path);
        }

        public IReadOnlyList<Student> ReadStudents()
        {
            if (_students != null)
            {
                return _students;
            }

            _students = LoadStudents();
            return _students;
        }

        private IReadOnlyList<Student> LoadStudents()
        {
            if (!File.Exists(_filePath))
            {
                Debug.LogWarning($"No se encontró el archivo de estudiantes: {_filePath}");
                return Array.Empty<Student>();
            }

            StudentListDto listDto = JsonUtility.FromJson<StudentListDto>(File.ReadAllText(_filePath));
            if (listDto == null || listDto.estudiantes == null)
            {
                Debug.LogError($"No se pudo deserializar el archivo de estudiantes: {_filePath}");
                return Array.Empty<Student>();
            }

            var students = new List<Student>(listDto.estudiantes.Length);
            foreach (StudentDto studentDto in listDto.estudiantes)
            {
                Student student = studentDto.ToStudent();
                if (student.ValidateData())
                {
                    students.Add(student);
                }
                else
                {
                    Debug.LogWarning($"Estudiante inválido omitido: {studentDto.codigo ?? "sin código"}");
                }
            }

            return students;
        }
    }
}