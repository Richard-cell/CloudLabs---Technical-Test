using SchoolNotesApp.StudentFeature.Domain;
using SchoolNotesApp.StudentFeature.View;
using UnityEngine;

namespace SchoolNotesApp.StudentFeature.Factory
{
    /// <summary>
    /// Fábrica que construye los paneles de información y los botones de arrastre
    /// de cada estudiante a partir de sus prefabs de interfaz.
    /// </summary>
    public sealed class StudentPanelFactory
    {
        private readonly StudentInfoPanelSetter _infoPanelPrefab;
        private readonly StudentDragButtonSetter _dragButtonPrefab;

        public StudentPanelFactory(StudentInfoPanelSetter infoPanelPrefab, StudentDragButtonSetter dragButtonPrefab)
        {
            _infoPanelPrefab = infoPanelPrefab;
            _dragButtonPrefab = dragButtonPrefab;
        }

        public StudentInfoPanelSetter CreateInfoPanel(Student student)
        {
            StudentInfoPanelSetter setter = Object.Instantiate(_infoPanelPrefab);
            setter.Configure(student);
            return setter;
        }

        public StudentDragButtonSetter CreateDragButton(Student student)
        {
            StudentDragButtonSetter setter = Object.Instantiate(_dragButtonPrefab);
            setter.Configure(student);
            return setter;
        }
    }
}