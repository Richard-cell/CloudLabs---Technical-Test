using System.Collections.Generic;
using SchoolNotesApp.StudentFeature.Domain;
using SchoolNotesApp.StudentFeature.Factory;
using SchoolNotesApp.StudentFeature.Reader;
using TMPro;
using UnityEngine;

namespace SchoolNotesApp.StudentFeature.View
{
    /// <summary>
    /// Instancia los paneles de información de los estudiantes dentro del
    /// contenedor correspondiente del panel de notas.
    /// </summary>
    public sealed class StudentInfoPanelSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform _contentParent;
        [SerializeField] private TextMeshProUGUI _noStudentsText;

        private IStudentReader _studentReader;
        private StudentPanelFactory _factory;
        private IReadOnlyList<Student> _students = System.Array.Empty<Student>();

        private void Start()
        {
            if (_factory == null)
            {
                return;
            }

            _studentReader ??= JSONStudentReader.FromStreamingAssets();
            SpawnStudents(_studentReader.ReadStudents());
        }

        public void Initialize(IStudentReader studentReader, StudentPanelFactory factory)
        {
            _studentReader = studentReader;
            _factory = factory;
        }

        private void SpawnStudents(IReadOnlyList<Student> students)
        {
            _students = students;
            ClearChildren(_contentParent);

            foreach (Student student in students)
            {
                StudentInfoPanelSetter panel = _factory.CreateInfoPanel(student);
                panel.transform.SetParent(_contentParent, false);

                StudentStateSetterUI stateSetter = panel.GetComponent<StudentStateSetterUI>();
                StudentStateDesignSetter designSetter = panel.GetComponent<StudentStateDesignSetter>();
                if (stateSetter != null && designSetter != null)
                {
                    stateSetter.Configure(student);
                    stateSetter.StateChanged += designSetter.Apply;
                }
            }
            _noStudentsText.SetText($"{_students.Count} estudiantes");
        }

        private static void ClearChildren(RectTransform parent)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}