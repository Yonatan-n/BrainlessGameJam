using UnityEngine;

public static class Utils
{
    //replace all occurences of a character in a string if the character is "within" the container
    public static string ReplaceAll(string s, char source, char replace, char container)
    {
        string result = "";
        bool withinContainer = false;
        foreach (var ch in s)
        {
            if (ch == container)
            {
                withinContainer = !withinContainer;
            }

            if (withinContainer && ch == source)
            {
                result += replace;
            }
            else
            {
                result += ch;
            }
        }
        return result;
    }
}