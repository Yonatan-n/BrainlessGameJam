using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using System.Runtime.Versioning;

public static class UIManager
{
    private static GameObject _points;
    private static GameObject _animatedWordsTemplate;
    private static Vector3 _animatedWordsTemplateContainerStartPos = Vector3.zero;

    private static string _oldWordTemplate = "";
    private static string _currentWordTemplate = "";

    public static void Initialize()
    {
        _points = GameObject.Find("RootGame/Points");
        _animatedWordsTemplate = GameObject.Find("RootGame/AnimatedWords");

        Vector3[] corners = new Vector3[4];
        _animatedWordsTemplate.transform.GetComponent<RectTransform>().GetWorldCorners(corners);
        _animatedWordsTemplateContainerStartPos = (corners[0] + corners[1]) / 2f;
    }

    public static void IncreasePoints(int points)
    {
        _points.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = points.ToString();
    }

    public static void UpdateAnimatedText(string wordTemplate, bool isCurrentWordTemplate, int index)
    {
        //Get the world position of the first character and check if its x coords are less than the left corner of 
        //the animated words template container
        _animatedWordsTemplate.transform.Find("Text").GetComponent<TextMeshProUGUI>().ForceMeshUpdate();
        TMP_TextInfo textInfo = _animatedWordsTemplate.transform.Find("Text").GetComponent<TextMeshProUGUI>().textInfo;
        TMP_CharacterInfo charInfo = textInfo.characterInfo[0];

        int vIndex = charInfo.vertexIndex;
        int mIndex = charInfo.materialReferenceIndex;

        Vector3[] vertices = textInfo.meshInfo[mIndex].vertices;

        var width = vertices[vIndex + 2] - vertices[vIndex + 1];
        width.y = 0;

        // The center position of the first character.
        Vector3 charCenter =
        (vertices[vIndex + 0] +
        vertices[vIndex + 1] +
        vertices[vIndex + 2] +
        vertices[vIndex + 3]) / 4f;

        charCenter.y = 0;

        Vector3 firstCharWorldPos = _animatedWordsTemplate.transform.Find("Text").GetComponent<TextMeshProUGUI>().transform.TransformPoint(charCenter) - width / 2;

        var text = _animatedWordsTemplate.transform.Find("Text").GetComponent<TextMeshProUGUI>().text;

        text += wordTemplate[index];

        if (firstCharWorldPos.x <= _animatedWordsTemplateContainerStartPos.x + 50.0f)
            text = text.Substring(1);

        _animatedWordsTemplate.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = text;


        //Display the current template word in green
        int i = text.IndexOf(GameManager.Instance.CurrentWordTemplate);

        if (i > -1)
        {
            Debug.Log("mark current word template!");
            var start = i;
            var end = i + GameManager.Instance.CurrentWordTemplate.Length - 1;
            for (int j = start; j < end; j++)
            {
                charInfo = textInfo.characterInfo[j];

                int meshIndex = charInfo.materialReferenceIndex;
                int vertexIndex = charInfo.vertexIndex;

                Color32[] colors = textInfo.meshInfo[meshIndex].colors32;

                colors[vertexIndex + 0] = Color.green;
                colors[vertexIndex + 1] = Color.green;
                colors[vertexIndex + 2] = Color.green;
                colors[vertexIndex + 3] = Color.green;

                // Apply changes
                //_animatedWordsTemplate.transform.Find("Text").GetComponent<TextMeshProUGUI>().UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            }
            _animatedWordsTemplate.transform.Find("Text").GetComponent<TextMeshProUGUI>().UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            _animatedWordsTemplate.transform.Find("Text").GetComponent<TextMeshProUGUI>().ForceMeshUpdate();
        }

    }

}