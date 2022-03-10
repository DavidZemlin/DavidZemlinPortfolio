//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System.Collections.Generic;
using UnityEngine;

// abstract superclass for specific SaveLoad subclasses. Saves or loads data in a file
public abstract class SaveLoad : MonoBehaviour
{
    public enum FileType
    {
        Txt,
        Ini,
        Dat,
        Book
    }

    public FileType fileType = new FileType();

    public void SetFileType(FileType pFileType)
    {
        fileType = pFileType;
    }

    public string GetFileExtention()
    {
        string result = null;
        if (fileType == FileType.Txt) result = ".txt";
        else if (fileType == FileType.Ini) result = ".ini";
        else if (fileType == FileType.Dat) result = ".dat";
        else if (fileType == FileType.Book) result = ".book";
        return result;
    }

    public abstract void SaveFile(List<string> pLines, string pPath, string pFileName);

    public abstract List<string> LoadFile(string pPath, string pFileName);
}
