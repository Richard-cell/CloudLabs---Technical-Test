using SchoolNotesApp.Common;
using SchoolNotesApp.NoteFeature.Validation;
using SchoolNotesApp.NoteFeature.View;
using SchoolNotesApp.StudentFeature.Reader;
using SchoolNotesApp.StudentFeature.View;
using UnityEngine;

namespace SchoolNotesApp
{
    [DefaultExecutionOrder(-100)]
    public sealed class GameInstaller : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private StudentInfoPanelSpawner _studentInfoPanelSpawner;
        [SerializeField] private NoteValidatorMessageDrawer _noteValidatorMessageDrawer;
        [SerializeField] private PanelNavigator _panelNavigator;

        [Header("Configuration")]
        [Range(0f, 5f)]
        [SerializeField] private float _passingGrade = 3f;

        private void Start()
        {
            IStudentReader studentReader = JSONStudentReader.FromStreamingAssets();
            NoteValidator noteValidator = new NoteValidator(_passingGrade);

            _studentInfoPanelSpawner.Initialize(studentReader);
            _noteValidatorMessageDrawer.Initialize(noteValidator, studentReader);
            _panelNavigator.Initialize(_noteValidatorMessageDrawer);
        }
    }
}