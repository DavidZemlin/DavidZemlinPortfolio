//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

//Custom UI menu tool for grouping menu tabs together so they act as a proper menu, switching which tab is currently open and making sure the others close properly
//This is the script for the Tabs themselves
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private TabGroup tabGroup;
    [SerializeField] private Image buttonImage;
    [SerializeField] private GameObject tabPanel;
    [SerializeField] private int tabIndex;
    private bool isActive;

    public UnityEvent OnPanelShown;
    public UnityEvent OnPanelHidden;

    public int GetTabIndex() { return tabIndex; }

    public void SetTabGroup(TabGroup pGroup) { tabGroup = pGroup; }

    void Awake()
    {
        if (tabGroup != null) AddToGroup();
    }

    //Add this button to the tab group referenced in the variable
    public void AddToGroup()
    {
        tabGroup.AddTabButton(this);
        tabIndex = tabGroup.GetTabButtonList().IndexOf(this);
    }

    //Remove this button from the tab group referenced in the variable
    public void LeaveGroup()
    {
        tabGroup.RemoveTabButton(this);
        tabIndex = -1;
    }

    //If this button is clicked, tell the tab group
    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    //If this button is hovered over, tell the tab group
    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    //If this button is no longer hovered over, tell the tab group
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    //Change color of this tab button
    public void ChangeColor(Color pColor)
    {
        buttonImage.color = pColor;
    }

    //Show menu Panel assigned to this button
    public void ShowPanel()
    {
        if (!isActive)
        {
            isActive = true;
            if (tabPanel != null)
            {
                tabPanel.SetActive(true);
            }
            if (OnPanelShown != null)
            {
                OnPanelShown.Invoke();
            }
        }
    }

    //Hide menu Panel assigned to this button
    public void HidePanel()
    {
        if (isActive)
        {
            isActive = false;
            if (tabPanel != null)
            {
                tabPanel.SetActive(false);
            }
            if (OnPanelHidden != null)
            {
                OnPanelHidden.Invoke();
            }
        }
    }
}
