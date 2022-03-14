//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Organizes and manages information from the profile for use in the navigation window
public class Library : MonoBehaviour
{
    private const int MAX_PER_PAGE = 10;

    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private Profile profile;
    [SerializeField] private Book currentBook;
    [SerializeField] private TMP_InputField newBookName;
    [SerializeField] private TMP_Text[] bookButtonLables;
    [SerializeField] private GameObject[] bookButtonObjects;
    [SerializeField] private GameObject prevButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject fileNameAlreadyExistsPanel;
    [SerializeField] private GameObject libraryPanel;

    [SerializeField] private List<int> booksOnThisPage;
    [SerializeField] private int pageNumber = 0;

    // Opens the library for navigation
    public void OpenLibrary()
    {
        GeneratePage();
    }

    // Closes Library, clearing all variables
    public void CloseLibrary()
    {
        foreach (TMP_Text l in bookButtonLables)
        {
            l.SetText("");
        }
        foreach (GameObject o in bookButtonObjects)
        {
            o.SetActive(false);
        }
        prevButton.SetActive(false);
        nextButton.SetActive(false);
        booksOnThisPage.Clear();
        booksOnThisPage = new List<int>();
        pageNumber = 0;
        HidePage();
    }

    // Adds a book to the list of saves in the profile and library
    public void CreateBook()
    {
        if (profile.GetSavePaths().Contains(newBookName.textComponent.text))
        {
            fileNameAlreadyExistsPanel.SetActive(true);
        }
        else
        {
            profile.AddSavePath(newBookName.textComponent.text);
            newBookName.textComponent.SetText("");
            pageNumber = (profile.GetSavePaths().Count + -1) / MAX_PER_PAGE;
            libraryPanel.SetActive(true);
        }
        GeneratePage();
    }

    // sent from the book menu when the book is being closed, clears current book variable
    public void CloseBook()
    {

    }

    public void OpenBook(int pButtonNumber)
    {
        currentBook.OpenBook(profile.GetSavePaths()[booksOnThisPage[pButtonNumber]]);
    }

    // Sets up a page to list available books in the library
    private void GeneratePage()
    {
        HidePage();
        if (profile.GetSavePaths().Count - (pageNumber * MAX_PER_PAGE) > MAX_PER_PAGE)
        {
            nextButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(false);
        }

        if (pageNumber > 0)
        {
            prevButton.SetActive(true);
        }
        else
        {
            prevButton.SetActive(false);
        }

        booksOnThisPage = new List<int>();
        for (int i = 0; i < MAX_PER_PAGE; i++)
        {
            int slotNumber = i + (MAX_PER_PAGE * pageNumber);
            if (slotNumber < profile.GetSavePaths().Count)
            {
                booksOnThisPage.Add(slotNumber);
            }
        }
        ShowPage();
    }

    // Displays the current library page
    private void ShowPage()
    {
        for (int i = 0; i < booksOnThisPage.Count; i++)
        {
            bookButtonObjects[i].SetActive(true);
            bookButtonLables[i].SetText(profile.GetSavePaths()[booksOnThisPage[i]]);
        }
    }

    // Hides the current Library Page
    private void HidePage()
    {
        for (int i = 0; i < booksOnThisPage.Count; i++)
        {
            bookButtonObjects[i].SetActive(false);
        }
    }

    // Switches to the next page
    public void nextPage()
    {
        pageNumber++;
        GeneratePage();
    }

    // Switches to the previous page
    public void prevPage()
    {
        pageNumber--;
        GeneratePage();
    }
}
