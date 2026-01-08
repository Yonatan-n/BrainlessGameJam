using UnityEngine;
using System.Collections.Generic;
public static class UIManager
{
    private static GameObject _points;
    private static GameObject _animatedWordsTemplate;

    public static void Initialize()
    {
        _points = GameObject.Find("RootGame/Points");
        _animatedWordsTemplate = GameObject.Find("RootGame/AnimatedWords");
    }

}