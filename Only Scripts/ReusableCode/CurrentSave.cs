//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// similar to "Profile" this is the actual program agnostic manager of an individual save file.
public class CurrentSave : MonoBehaviour
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private SaveLoad saver;
    [SerializeField] private SaveLoad exporter;
    [SerializeField] private SaveLoad backupSaver;
    [SerializeField] private Profile profile;
    [SerializeField] private string saveSubfolderName;
    [SerializeField] private string exportSubfolderName;
    [SerializeField] private string backupSubfolderName;

    [SerializeField] private string fileName;
    [SerializeField] private List<string> data;

    public List<string> GetData()
    {
        return data;
    }

    public string GetFileName() { return fileName; }

    public void SetData(List<string> pDataLines)
    {
        data.Clear();
        data = new List<string>();
        foreach (string s in pDataLines)
        {
            data.Add(s);
        }
    }

    // Searches for and opens or creates the file at the given directory
    public void OpenSave(string pFileName)
    {
        if (pFileName == null || pFileName.Length < 1)
        {
            return;
        }
        fileName = pFileName;
        if (saveSubfolderName.Length < 1)
        {
            return;
        }
        else if (File.Exists(profile.GetProfileFolderPath() + saveSubfolderName + "//" + fileName + saver.GetFileExtention()))
        {
            Load();
        }
        else
        {
            Save();
        }
    }

    // Clears all variables
    public void CloseSave()
    {
        fileName = null;
        data.Clear();
        data = new List<string>();
        // Don't forget to add variables here as they are added to this class---------------------------------------------------------------------------------------------------------- 
    }

    // Sets dataLines to the data pulled from the safe file in the directory
    public void Load()
    {
        data = saver.LoadFile(profile.GetProfileFolderPath() + saveSubfolderName + "//", fileName);
    }

    // Saves currently open data to the file in the directory
    public void Save()
    {
        if (profile.GetProfileFilePath() != null || profile.GetProfileFilePath().Length > 0)
        {
            saver.SaveFile(data, profile.GetProfileFolderPath() + saveSubfolderName + "//", fileName);
        }
    }

    // Save a backup copy of the savwe data
    public void SaveBackup()
    {
        string date = "_" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
        backupSaver.SaveFile(data, profile.GetProfileFolderPath() + backupSubfolderName + "//", fileName + date);
    }

    // Export the save data
    public void Export(List<string> pExport)
    {
        string date = "_" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
        exporter.SaveFile(pExport, profile.GetProfileFolderPath() + exportSubfolderName + "//", fileName + date);
    }

    // Removes this save from the profile's list, deletes the save file and then closes the current save
    public void DeleteOpenSave()
    {
        profile.RemoveSavePath(fileName);
        if (File.Exists(profile.GetProfileFolderPath() + saveSubfolderName + "//" + fileName + saver.GetFileExtention()))
        {
            File.Delete(profile.GetProfileFolderPath() + saveSubfolderName + "//" + fileName + saver.GetFileExtention());
        }
    }
}
