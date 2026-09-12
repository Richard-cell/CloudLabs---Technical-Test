using SchoolNotesApp.StudentFeature.Domain;
using UnityEngine;

namespace SchoolNotesApp.StudentFeature.View
{
    /// <summary>
    /// Zona de destino del arrastre que clasifica a un estudiante como aprobado
    /// o reprobado al recibir su botón y cambia su estado de calificación.
    /// </summary>
    public sealed class DropZone : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform _content;

        [Header("Configuration")]
        [SerializeField] private StudentState _targetState;

        [Header("Hover Design")]
        [Range(1f, 1.5f)]
        [SerializeField] private float _hoverScale = 1.1f;

        private Vector3 _originalScale;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        private void Start()
        {
            ClearContent();
        }

        private void ClearContent()
        {
            if (_content == null)
            {
                return;
            }

            for (int i = _content.childCount - 1; i >= 0; i--)
            {
                Destroy(_content.GetChild(i).gameObject);
            }
        }

        public RectTransform Rect => (RectTransform)transform;

        public RectTransform Content => _content;

        public void OnHoverEnter()
        {
            transform.localScale = _originalScale * _hoverScale;
        }

        public void OnHoverExit()
        {
            transform.localScale = _originalScale;
        }

        public void Accept(StudentDragButtonDragger button)
        {
            RectTransform rectTransform = (RectTransform)button.transform;
            rectTransform.SetParent(_content != null ? _content : Rect, false);
            rectTransform.anchorMin = Vector2.one * 0.5f;
            rectTransform.anchorMax = Vector2.one * 0.5f;
            rectTransform.pivot = Vector2.one * 0.5f;
            rectTransform.sizeDelta = new Vector2(700f, 80f);
            rectTransform.anchoredPosition = Vector2.zero;
            button.enabled = false;

            StudentDragButtonSetter setter = button.GetComponent<StudentDragButtonSetter>();
            if (setter != null && setter.Student != null)
            {
                setter.Student.SetState(_targetState);
            }
        }
    }
}