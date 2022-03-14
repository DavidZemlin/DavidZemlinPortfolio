//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System.Collections.Generic;
using System.IO;

// Simple save and load system that either takes a List<string> and writes each string as a new line in a file, 
//      or loads each line of a file into a List<string>, where each new line is an element of the list.
public class SaveLoadSimple : SaveLoad
{
    public override void SaveFile(List<string> pLines, string pPath, string pFileName)
    {
        if (pPath.Length > 0 || pPath != null)
        {
            if (!Directory.Exists(pPath))
            {
                Directory.CreateDirectory(pPath);
            }

            string[] lines = new string[pLines.Count];

            for (int i = 0; i < pLines.Count; i++)
            {
                lines[i] = pLines[i];
            }
            File.WriteAllLines(pPath + pFileName + GetFileExtention(), lines);
        }
    }

    public override List<string> LoadFile(string pPath, string pFileName)
    {
        List<string> linesList = new List<string>();
        string[] lines = File.ReadAllLines(pPath + pFileName + GetFileExtention());
        foreach (string s in lines)
        {
            linesList.Add(s);
        }
        return linesList;
    }
}
