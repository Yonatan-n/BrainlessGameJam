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

    public static int FindFirstMatchingSubstring(string searchText, string word, out string matchingWord)
    {
        //Did we find the whole word?
        int foundIndex1 = -1;
        int foundIndex2 = -1;
        string matchingWord1 = "", matchingWord2 = "";
        matchingWord = "";

        //check if the first letters in the searchText match the first letters in the word
        for (int i = 0; i < word.Length; i++)
        {
            var result = searchText.IndexOf(word.Substring(0, i + 1));
            if (result != -1)
            {
                foundIndex1 = result;
                matchingWord1 = word.Substring(0, i + 1);
            }
        }

        //check if the whole word is found
        if (foundIndex1 == -1)
        {
            foundIndex1 = searchText.IndexOf(word);
            matchingWord1 = word;
        }

        //check if the first letters in the searchText match the last letters in the word
        for (int i = 0; i < word.Length && foundIndex2 == -1; i++)
        {
            var result = searchText.IndexOf(word.Substring(i + 1));
            if (result != -1)
            {
                foundIndex2 = result;
                matchingWord2 = word.Substring(i + 1);
            }
            else
            {
                break;
            }
        }

        if (foundIndex2 > -1 && foundIndex2 < foundIndex1)
        {
            matchingWord = matchingWord2;
            return foundIndex2;
        }

        matchingWord = matchingWord1;
        return foundIndex1;

    }
}