using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SchoolNotesApp.StudentFeature.View
{
    /// <summary>
    /// Permite arrastrar un botón de estudiante por la interfaz, detecta la zona
    /// sobrevolada y lo deposita en la DropZone correcta o lo restaura a su origen.
    /// </summary>
    public sealed class StudentDragButtonDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private IReadOnlyList<DropZone> _dropZones = System.Array.Empty<DropZone>();
        private DropZone _hoveredZone;
        private RectTransform _rectTransform;
        private RectTransform _homeParent;
        private Vector3 _originalPosition;

        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
        }

        public void Initialize(IReadOnlyList<DropZone> dropZones, RectTransform homeParent)
        {
            _dropZones = dropZones;
            _homeParent = homeParent;
        }

        public void ResetToHome()
        {
            if (_homeParent == null)
            {
                return;
            }

            _rectTransform.SetParent(_homeParent, false);
            RectTransformFitter.StretchToParent(_rectTransform);
            enabled = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalPosition = _rectTransform.position;
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position += (Vector3)eventData.delta;

            DropZone currentZone = GetDropZoneAt(eventData.position);
            if (currentZone != _hoveredZone)
            {
                if (_hoveredZone != null)
                {
                    _hoveredZone.OnHoverExit();
                }

                _hoveredZone = currentZone;

                if (_hoveredZone != null)
                {
                    _hoveredZone.OnHoverEnter();
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_hoveredZone != null)
            {
                _hoveredZone.OnHoverExit();
                _hoveredZone = null;
            }

            DropZone dropZone = GetDropZoneAt(eventData.position);
            if (dropZone != null)
            {
                dropZone.Accept(this);
            }
            else
            {
                _rectTransform.position = _originalPosition;
            }
        }

        private DropZone GetDropZoneAt(Vector2 screenPoint)
        {
            foreach (DropZone dropZone in _dropZones)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(dropZone.Rect, screenPoint))
                {
                    return dropZone;
                }
            }
            return null;
        }
    }
}