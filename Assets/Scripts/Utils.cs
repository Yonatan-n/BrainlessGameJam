using UnityEngine;
using UnityEngine.UI;
public static class Utils
{

    public static Vector3 UIToWorldPosition(RectTransform uiElement, Vector3 worldPos, Canvas canvas)
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, worldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiElement.parent as RectTransform,
            screenPos,
            canvas.worldCamera,
            out Vector2 localPos
        );
        return localPos;
    }

    public static void MoveUIToWorldPosition(RectTransform uiElement, Vector3 worldPos, Canvas canvas)
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, worldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiElement.parent as RectTransform,
            screenPos,
            canvas.worldCamera,
            out Vector2 localPos
        );
        uiElement.anchoredPosition = localPos;
    }
}