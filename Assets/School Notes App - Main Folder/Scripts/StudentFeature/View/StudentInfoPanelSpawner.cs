using System.Collections.Generic;
using SchoolNotesApp.StudentFeature.Domain;
using SchoolNotesApp.StudentFeature.Reader;
using TMPro;
using UnityEngine;

namespace SchoolNotesApp.StudentFeature.View
{
    public sealed class StudentInfoPanelSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform _contentParent;
        [SerializeField] private StudentInfoPanelSetter _studentRowPrefab;
        [SerializeField] private TextMeshProUGUI _noStudentsText;

        private IStudentReader _studentReader;
        private IReadOnlyList<Student> _students = System.Array.Empty<Student>();

        private void Start()
        {
            _studentReader ??= JSONStudentReader.FromStreamingAssets();
            SpawnStudents(_studentReader.ReadStudents());
        }

        public void Initialize(IStudentReader studentReader)
        {
            _studentReader = studentReader;
        }

        private void SpawnStudents(IReadOnlyList<Student> students)
        {
            _students = students;
            ClearContent();

            foreach (Student student in students)
            {
                StudentInfoPanelSetter panel = Instantiate(_studentRowPrefab, _contentParent);
                panel.Configure(student);

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

        private void ClearContent()
        {
            for (int i = _contentParent.childCount - 1; i >= 0; i--)
            {
                Destroy(_contentParent.GetChild(i).gameObject);
            }
        }
    }
}