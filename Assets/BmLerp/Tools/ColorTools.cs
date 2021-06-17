using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public enum ColorType
{
    RRGGBB,
    RRGGBBAA
}

public class ColorTools
{
    private const string hexRegex = "^#?(?:[0-9a-fA-F]{3,4}){1,2}$";
    public static Color32 HexToColor(string hex)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(hex, hexRegex))
        {
            return new Color32(255, 255, 255, 255);
        }
            hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);

    }

    public static string ColorToHex(Color c)
    {
        long num = 0;
        string hexStr = "";
        int retVal = 0;
        retVal |= Mathf.RoundToInt(c.r * 255f) << 24;
        retVal |= Mathf.RoundToInt(c.g * 255f) << 16;
        retVal |= Mathf.RoundToInt(c.b * 255f) << 8;
        retVal |= Mathf.RoundToInt(c.a * 255f);

        num = 0xFFFFFFFF & retVal;
        hexStr = "#" + num.ToString("X8");

        return hexStr;
    }

    public static Color CreateColor(int r, int g, int b, int a)
    {
        return new Color(((float)r) / 255, ((float)g) / 255, ((float)b) / 255, ((float)a) / 255);
    }


    public static Color SetAlpha(Color _src, float _a)
    {
        Color t = _src;
        t.a = _a;
        return t;
    }
}