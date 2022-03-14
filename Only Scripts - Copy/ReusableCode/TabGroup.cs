//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Custom UI menu tool for grouping menu tabs together so they act as a proper menu, switching which tab is currently open and making sure the others close properly
//This is the script for the whole group of tabs
public class TabGroup : MonoBehaviour
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private List<TabButton> tabButtonList = new List<TabButton>();
    [SerializeField] private Color idleColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color activeColor;
    [SerializeField] private TabButton selectedTab;
    [SerializeField] private List<TabGroup> subGroups;

    public List<TabButton> GetTabButtonList() { return tabButtonList; }
    public TabButton GetSelectedTab() { return selectedTab; }

    public UnityEvent OnTabClicked;

    //Adds a tab to this group
    public void AddTabButton(TabButton pButton)
    {
        tabButtonList.Add(pButton);
    }

    //Removes a tab from this group
    public void RemoveTabButton(TabButton pButton)
    {
        tabButtonList.Remove(pButton);
    }

    //Behaviors of the tab group when the mouse is hovered over a tab button
    public void OnTabEnter(TabButton pButton)
    {
        ResetTabColors();
        if (selectedTab == null || pButton != selectedTab)
        {
            pButton.ChangeColor(hoverColor);
        }
    }

    //Behaviors of the tab group when the mouse exits a tab button
    public void OnTabExit(TabButton pButton)
    {
        ResetTabColors();
    }

    //A tab button of this group was Click
    public void OnTabSelected(TabButton pButton)
    {
        CloseAllTabs();
        selectedTab = pButton;
        ResetTabColors();
        pButton.ChangeColor(activeColor);
        UpdateActivePanel();
        if (OnTabClicked != null)
        {
            OnTabClicked.Invoke();
        }
    }

    //Resets all tab colors, excepted the currently active one
    public void ResetTabColors()
    {
        for (int i = 0; i < tabButtonList.Count; i++)
        {
            TabButton button = tabButtonList[i];
            if (button != selectedTab)
            button.ChangeColor(idleColor);
        }
    }

    //Sets the Active menu panel to match the one tied to the active tab button
    public void UpdateActivePanel()
    {
        for (int i = 0; i < tabButtonList.Count; i++)
        {
            TabButton button = tabButtonList[i];
            if (button == selectedTab)
            {
                button.ShowPanel();
            }
            else
            {
                button.HidePanel();
            }
        }
    }

    //Closes all Tabs
    public void CloseAllTabs()
    {
        if (subGroups.Count > 0)
        {
            for (int i = 0; i < subGroups.Count; i++)
            {
                TabGroup s = subGroups[i];
                s.CloseAllTabs();
            }
        }
        selectedTab = null;
        for (int i = 0; i < tabButtonList.Count; i++)
        {
            TabButton button = tabButtonList[i];
            button.HidePanel();
        }
        ResetTabColors();
    }
}
