//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Handles when the program should attempt to load the user profiles and open their libraries
public class ProfileLoader : MonoBehaviour
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private GameObject emptyLibraryPanel;
    [SerializeField] private GameObject libraryPanel;
    [SerializeField] private Button confirmLibraryButton;
    [SerializeField] private Library libraryScript;
    [SerializeField] private Profile profile;
    [SerializeField] private TMP_Text libraryPathText;
    [SerializeField] private TMP_Text warningText;
    [SerializeField] private TMP_InputField profilePathField;

    private void Awake()
    {
        // Attempt to open the profile
        profile.OpenProfile();

        // If profile data is found, open the library; if not, open the profile selection panel
        if (!profile.HasLocatedProfileFile())
        {
            ShowEmptyLibraryPanel();
        }
        else
        {
            ShowLibraryPanel();
        }
    }

    private void OnApplicationQuit()
    {
        profile.SaveProfileData();
    }

    // Selects the folder in which to save and load profile data from
    public void SelectLibraryPath()
    {
        profile.SetProfileFilePath(profilePathField.text);
        UpdateLibraryPathText();
        UpdateLibraryConfirmButton();
    }

    // Sets the folder in which to save and load profile data from to be the default folder
    public void SetLibraryFolderToDefualt()
    {
        profile.SetProfileFilePathToDefault();
        UpdateLibraryPathText();
        UpdateLibraryConfirmButton();
    }

    // Shows the panel for selecting or creating profile data
    private void ShowEmptyLibraryPanel()
    {
        emptyLibraryPanel.SetActive(true);
    }

    // Shows the panel that displays open library data
    private void ShowLibraryPanel()
    {
        libraryPanel.SetActive(true);
        libraryScript.OpenLibrary();
    }

    // Updates the texts that displays the selected folder for saving and loading profile data
    public void UpdateLibraryPathText()
    {
        libraryPathText.SetText(profile.GetProfileFolderPath());
    }

    // Sets the active state of the library confirm button, based on weather a valid folder is currently selected
    public void UpdateLibraryConfirmButton()
    {
        if (profile.GetProfileFilePath() == null)
        {
            confirmLibraryButton.interactable = false;
            warningText.gameObject.SetActive(true);
        }
        else
        {
            confirmLibraryButton.interactable = true;
            warningText.gameObject.SetActive(false);
        }
    }
}
