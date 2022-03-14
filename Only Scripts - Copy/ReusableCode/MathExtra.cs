//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System;
using UnityEngine;

//This is a personal tool box of custom static math functions I find useful
public static class MathExtra
{
    public static float RoundDownFloat(float pInputFloat, int pDecimalPlaces)
    {
        return Mathf.Floor(pInputFloat * Mathf.Pow(10, pDecimalPlaces)) / Mathf.Pow(10, pDecimalPlaces);
    }

    public static double RoundDownDouble(double pInputDouble, int pDecimalPlaces)
    {
        return Math.Floor(pInputDouble * Math.Pow(10, pDecimalPlaces)) / Math.Pow(10, pDecimalPlaces);
    }

    public static float RoundUpFloat(float pInputDouble, int pDecimalPlaces)
    {
        return Mathf.Ceil(pInputDouble * Mathf.Pow(10, pDecimalPlaces)) / Mathf.Pow(10, pDecimalPlaces);
    }

    public static double RoundUpDouble(double pInputDouble, int pDecimalPlaces)
    {
        return Math.Ceiling(pInputDouble * Math.Pow(10, pDecimalPlaces)) / Math.Pow(10, pDecimalPlaces);
    }
}
