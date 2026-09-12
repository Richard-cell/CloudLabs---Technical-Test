using UnityEngine;

namespace SchoolNotesApp.StudentFeature.View
{
    /// <summary>
    /// Utilidad estática para el ajuste de RectTransform, estirándolos para que
    /// ocupen por completo el área de su padre.
    /// </summary>
    public static class RectTransformFitter
    {
        public static void StretchToParent(RectTransform rectTransform)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
    }
}