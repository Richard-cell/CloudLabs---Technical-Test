using System;
using SchoolNotesApp.StudentFeature.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace SchoolNotesApp.StudentFeature.View
{
    public sealed class StudentStateSetterUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Toggle _approveToggle;
        [SerializeField] private Toggle _failToggle;

        private Student _student;
        private bool _isSyncingToggles;

        public event Action<StudentState> StateChanged;

        public void Configure(Student student)
        {
            _student = student;
            _approveToggle.onValueChanged.AddListener(OnApproveToggleChanged);
            _failToggle.onValueChanged.AddListener(OnFailToggleChanged);
        }

        private void OnDestroy()
        {
            if (_approveToggle != null)
            {
                _approveToggle.onValueChanged.RemoveListener(OnApproveToggleChanged);
            }

            if (_failToggle != null)
            {
                _failToggle.onValueChanged.RemoveListener(OnFailToggleChanged);
            }
        }

        private void OnApproveToggleChanged(bool isOn)
        {
            if (_isSyncingToggles || _student == null)
            {
                return;
            }

            ApplyState(isOn ? StudentState.Aprobado : StudentState.NoCalificado);
        }

        private void OnFailToggleChanged(bool isOn)
        {
            if (_isSyncingToggles || _student == null)
            {
                return;
            }

            ApplyState(isOn ? StudentState.Reprobado : StudentState.NoCalificado);
        }

        private void ApplyState(StudentState state)
        {
            _isSyncingToggles = true;
            _student.SetState(state);
            _approveToggle.isOn = state == StudentState.Aprobado;
            _failToggle.isOn = state == StudentState.Reprobado;
            _isSyncingToggles = false;

            StateChanged?.Invoke(state);
        }
    }
}