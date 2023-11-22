using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YayuColor
{
    public static Color ColorByCode(string code)
    {
        if(ColorUtility.TryParseHtmlString(code, out var col))
        {
            return col;
        }
        else
        {
            YDebugger.LogError($"Color code {code} is invalid.");
            return Color.white;
        }
    }
}
