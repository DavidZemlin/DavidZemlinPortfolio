//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System.Collections.Generic;
using System.IO;
using UnityEngine;
 
//The Profile is used to save user data, including a collection of saves assigned to one user
internal class Profile : MonoBehaviour
{
    private const string PROFILE_FILE = "bookList";
    private const string DEFUALT_PROFILE_FOLDER = "savedData\\";

    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private SaveLoad saver;

    [SerializeField] private string profileFolderPath;
    [SerializeField] private List<string> savePaths; 


    public string GetProfileFilePath()
    {
        if (profileFolderPath == null || profileFolderPath.Length < 1) return null;
        return profileFolderPath + PROFILE_FILE;
    }
    public string GetProfileFolderPath()
    {
        return profileFolderPath;
    }
    public List<string> GetSavePaths()
    {
        return savePaths;
    }

    public void SetProfileFilePath(string pPath)
    {
        if (pPath == null)
        {
            profileFolderPath = null;
            return;
        }
        if (pPath.Length < 1)
        {
            profileFolderPath = null;
            return;
        }
        profileFolderPath = pPath;
        if (!profileFolderPath.EndsWith("\\") && !profileFolderPath.EndsWith("/"))
        {
            profileFolderPath = profileFolderPath + "\\";
        }
        PlayerPrefs.SetString("ProfileLocation", profileFolderPath);
    }
    public void SetProfileFilePathToDefault()
    {
        profileFolderPath = DEFUALT_PROFILE_FOLDER;
        PlayerPrefs.SetString("ProfileLocation", profileFolderPath);
    }

    // Returns true if a file matching the profile path and file name exists
    public bool HasLocatedProfileFile()
    {
        if (profileFolderPath == null || profileFolderPath.Length < 1)
        {
            return false;
        }
        else if (!File.Exists(profileFolderPath + PROFILE_FILE + saver.GetFileExtention()))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Attempts to open and load the profile data file; sets file path to null if no file is found
    public void OpenProfile()
    {
        if (PlayerPrefs.GetString("ProfileLocation") != null)
        {
            SetProfileFilePath(PlayerPrefs.GetString("ProfileLocation"));
            if (HasLocatedProfileFile())
            {
                LoadProfileData();
            }
            else
            {
                SetProfileFilePath(null);
            }
        }
        else
        {
            SetProfileFilePath(null);
        }
    }

    // Clears profile data variables; this method saves the open data first
    public void CloseProfile()
    {
        SaveProfileData();
        profileFolderPath = null;
        savePaths.Clear();
        savePaths = new List<string>();
    }

    // Adds a save data file to the list of files belonging to this profile
    public void AddSavePath(string pPath)
    {
        savePaths.Add(pPath);
    }

    // Removes a save data file path from the list this does NOT delete the file associated with it
    public void RemoveSavePath(string pPath)
    {
        savePaths.Remove(pPath);
    }

    // Gets a list of all save data files belonging to this profile
    private void LoadProfileData()
    {
        savePaths = saver.LoadFile(profileFolderPath, PROFILE_FILE);
    }

    // Saves the profile data (a list of all save data files belonging to this profile)
    public void SaveProfileData()
    {
        if (profileFolderPath != null && profileFolderPath.Length > 0)
        {
            saver.SaveFile(savePaths, profileFolderPath, PROFILE_FILE);
        }
    }
}
