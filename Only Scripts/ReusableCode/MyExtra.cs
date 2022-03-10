//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

//This is a personal tool box of custom static functions I find useful
public static class MyExtra
{
    //Converts time from a 24 hour format to a 12 hour format
    public static string TwentyFourHrToTwelveHr(bool showLeadingZero, bool consistentSpacing, bool spaceBeforeAm, int hr, int min)
    {
        string output = "";
        string hrString = "";
        string minString = "";
        string amString = "";
        string amSpace = "";
        bool am;

        if (hr == 0)
        {
            hr = 12;
            am = true;
        }
        else if (hr < 12)
        {
            am = true;
        }
        else if (hr == 12)
        {
           am = false;
        }
        else
        {
            hr -= 12;
            am = false;
        }

        if (hr < 10)
        {
            if (showLeadingZero)
            {
                hrString = "0" + hr;
            }
            else
            {
                if(consistentSpacing)
                {
                    hrString = " " + hr;
                }
                else
                {
                    hrString = "" + hr;
                }
            }
        }
        else
        {
            hrString = "" + hr;
        }

        if (min < 10)
        {
            minString = "0" + min;
        }
        else
        {
            minString = "" + min;
        }

        if (spaceBeforeAm)
        {
            amSpace = " ";
        }
        else
        {
            amSpace = "";
        }

        if (am)
        {
            amString = "am";
        }
        else
        {
            amString = "pm";
        }

        output = hrString + ":" + minString + amSpace + amString;
        return output;
    }

    //Adds filler characters to a sting to make the string be a specific length
    public static string ExtendString(bool extrendFromFront, string stringToExtend, int newLength, char fillerChar)
    {
        string newString = stringToExtend;
        for (int i = 0; i < newLength - stringToExtend.Length; i++)
        {
            if (extrendFromFront)
            {
                newString = char.ToString(fillerChar) + newString;
            }
            else
            {
                newString = newString + char.ToString(fillerChar);
            }
        }
        return newString;
    }
}
