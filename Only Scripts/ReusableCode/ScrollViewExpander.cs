//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is a custom UI script that enables a scroll view component to resize as content is added or removed from it
public class ScrollViewExpander : MonoBehaviour
{
    //Used to select in which direction the scroll view should expand
    private enum Direction
    {
        Horizontal,
        Vertical
    }

    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private Direction direction;
    [SerializeField] private RectTransform container;
    [SerializeField] private ScrollRect scrollRectScript;
    [SerializeField] private List<GameObject> contents = new List<GameObject>();

    private float tallest;
    private float widest;

    public List<GameObject> GetContents() { return contents; }

    private void Awake()
    {
        if (container == null) container = gameObject.GetComponent<RectTransform>();
        if (container.childCount > 0)
        {
            foreach (RectTransform t in container)
            {
                CompareLargest(t);
                AddItem(t.gameObject);
            }
        }
    }

    //Checks if a given RectTransform is the widest or tallest of the contents and records the current widest and tallest size
    public void CompareLargest(RectTransform t)
    {
        if (t.sizeDelta.x > widest) widest = t.sizeDelta.x;
        if (t.sizeDelta.y > tallest) tallest = t.sizeDelta.y;
    }

    //Adds an item to the contents and resizes the scroll view accordingly
    public void AddItem(GameObject pItem)
    {
        if (contents.Contains(pItem))
        {
            return;
        }
        contents.Add(pItem);
        RectTransform rt = pItem.GetComponent<RectTransform>();
        RectTransform crt = container;
        rt.SetParent(crt);
        rt.localScale = Vector3.one;
        CompareLargest(rt);
        if (direction == Direction.Horizontal)
        {
            crt.sizeDelta = new Vector2(widest * crt.childCount, crt.sizeDelta.y);
            rt.anchoredPosition = new Vector3(0 + ((widest * crt.childCount) - (widest / 2)), 0, 0);
        }
        else
        {
            crt.sizeDelta = new Vector2(crt.sizeDelta.x, tallest * crt.childCount);
            rt.anchoredPosition = new Vector3(0, 0 - ((tallest * crt.childCount) - (tallest / 2)), 0);
        }
    }

    //Clears out all contents
    public void ClearContainer()
    {
        contents.Clear();
        contents = new List<GameObject>();
    }

    //Removes an item from the contents and resizes the scroll view accordingly
    public void RemoveItem(GameObject pItem)
    {
        if (contents.Contains(pItem))
        {
            contents.Remove(pItem);
        }
        RepositionItems();
    }

    //Repositions contents and resizes the scroll view
    public void RepositionItems()
    {
        if (direction == Direction.Horizontal)
        {
            container.sizeDelta = new Vector2(widest * container.childCount, container.sizeDelta.y);
            for (int i = 0; i < contents.Count; i++)
            {
                RectTransform rt = contents[i].gameObject.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector3(0 + ((widest * (i + 1)) - (widest / 2)), 0, 0);
            }
        }
        else
        {
            container.sizeDelta = new Vector2(container.sizeDelta.x, tallest * container.childCount);
            for (int i = 0; i < contents.Count; i++)
            {
                RectTransform rt = contents[i].gameObject.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector3(0, 0 - ((tallest * (i + 1)) - (tallest / 2)), 0);
            }
        }
    }

    //Jumps to the last item of a vertical content list
    public void JumpToBottum()
    {
        scrollRectScript.verticalNormalizedPosition = 0.0f;
    }

    //Jumps to the last item of a Horizontal content list
    public void JumpToRight()
    {
        scrollRectScript.verticalNormalizedPosition = 1.0f;
    }
}
