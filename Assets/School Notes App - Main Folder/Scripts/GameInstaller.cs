using SchoolNotesApp.Common;
using SchoolNotesApp.NoteFeature.Validation;
using SchoolNotesApp.NoteFeature.View;
using SchoolNotesApp.StudentFeature.Factory;
using SchoolNotesApp.StudentFeature.Reader;
using SchoolNotesApp.StudentFeature.View;
using UnityEngine;

namespace SchoolNotesApp
{
    [DefaultExecutionOrder(-100)]
    /// <summary>
    /// Composition Root principal: crea e inyecta las dependencias de la aplicación
    /// (lector de estudiantes, validador, factorías y spawners) al iniciar la escena.
    /// </summary>
    public sealed class GameInstaller : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private StudentInfoPanelSpawner _studentInfoPanelSpawner;
        [SerializeField] private StudentDragButtonSpawner _studentDragButtonSpawner;
        [SerializeField] private NoteValidatorMessageDrawer _noteValidatorMessageDrawer;
        [SerializeField] private NoteValidatorMessageDrawer _classificationValidatorMessageDrawer;
        [SerializeField] private PanelNavigator _panelNavigator;

        [Header("Prefabs")]
        [SerializeField] private StudentInfoPanelSetter _studentInfoPanelPrefab;
        [SerializeField] private StudentDragButtonSetter _studentDragButtonPrefab;

        [Header("Configuration")]
        [Range(0f, 5f)]
        [SerializeField] private float _passingGrade = 3f;

        private void Start()
        {
            IStudentReader studentReader = JSONStudentReader.FromStreamingAssets();
            NoteValidator noteValidator = new NoteValidator(_passingGrade);
            StudentPanelFactory studentPanelFactory = new StudentPanelFactory(_studentInfoPanelPrefab, _studentDragButtonPrefab);

            _studentInfoPanelSpawner.Initialize(studentReader, studentPanelFactory);
            _studentDragButtonSpawner.Initialize(studentReader, studentPanelFactory);
            _noteValidatorMessageDrawer.Initialize(noteValidator, studentReader);
            if (_classificationValidatorMessageDrawer != null)
            {
                _classificationValidatorMessageDrawer.Initialize(noteValidator, studentReader);
            }

            _panelNavigator.Initialize(_noteValidatorMessageDrawer);
        }
    }
}