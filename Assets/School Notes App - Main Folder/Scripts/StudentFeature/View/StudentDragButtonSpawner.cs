using System.Collections.Generic;
using SchoolNotesApp.StudentFeature.Domain;
using SchoolNotesApp.StudentFeature.Factory;
using SchoolNotesApp.StudentFeature.Reader;
using TMPro;
using UnityEngine;

namespace SchoolNotesApp.StudentFeature.View
{
    /// <summary>
    /// Crea los botones de arrastre de los estudiantes dentro del contenedor
    /// principal, los estira a su tamaño inicial y restaura el flujo de clasificación.
    /// </summary>
    public sealed class StudentDragButtonSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform _dragButtonContentParent;
        [SerializeField] private TextMeshProUGUI _noStudentsText;

        [Header("Drop Zones")]
        [SerializeField] private DropZone[] _dropZones = System.Array.Empty<DropZone>();
        
        private IStudentReader _studentReader;
        private StudentPanelFactory _factory;

        private void Start()
        {
            if (_factory == null || _dragButtonContentParent == null)
            {
                return;
            }

            _studentReader ??= JSONStudentReader.FromStreamingAssets();
            SpawnDragButtons(_studentReader.ReadStudents());
        }

        public void Initialize(IStudentReader studentReader, StudentPanelFactory factory)
        {
            _studentReader = studentReader;
            _factory = factory;
        }

        private void SpawnDragButtons(IReadOnlyList<Student> students)
        {
            ClearChildren(_dragButtonContentParent);

            foreach (Student student in students)
            {
                StudentDragButtonSetter button = _factory.CreateDragButton(student);
                RectTransform rectTransform = (RectTransform)button.transform;
                rectTransform.SetParent(_dragButtonContentParent, false);
                RectTransformFitter.StretchToParent(rectTransform);

                StudentDragButtonDragger dragger = button.GetComponent<StudentDragButtonDragger>();
                if (dragger != null)
                {
                    dragger.Initialize(_dropZones, _dragButtonContentParent);
                }
            }
            _noStudentsText.SetText($"{students.Count} estudiantes");
        }

        public void ResetFlow()
        {
            foreach (DropZone dropZone in _dropZones)
            {
                RectTransform content = dropZone.Content;
                if (content == null)
                {
                    continue;
                }

                while (content.childCount > 0)
                {
                    StudentDragButtonDragger dragger = content.GetChild(0).GetComponent<StudentDragButtonDragger>();
                    if (dragger != null)
                    {
                        ResetStudentState(dragger);
                        dragger.ResetToHome();
                    }
                    else
                    {
                        Destroy(content.GetChild(0).gameObject);
                    }
                }
            }
        }

        private static void ResetStudentState(StudentDragButtonDragger dragger)
        {
            StudentDragButtonSetter setter = dragger.GetComponent<StudentDragButtonSetter>();
            if (setter != null && setter.Student != null)
            {
                setter.Student.SetState(StudentState.NoCalificado);
            }
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