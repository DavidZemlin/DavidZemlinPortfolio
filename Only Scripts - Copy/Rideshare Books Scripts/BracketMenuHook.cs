//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System;
using TMPro;
using UnityEngine;

public class BracketMenuHook : MonoBehaviour, IComparable<BracketMenuHook>
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private int limit;
    [SerializeField] private float percent;
    [SerializeField] private Bracket bracketDataRefrence;
    [SerializeField] private TMP_InputField percentField;
    [SerializeField] private TMP_InputField limitField;
    [SerializeField] private ScrollViewExpander scrollContainer;
    [SerializeField] private BookViewer bookViewer;

    public Bracket GetBracketDataRefrence() { return bracketDataRefrence; }
    public int GetLimit() { return limit; }
    public float GetPercent() { return percent; }

    public void SetScrollContainer(ScrollViewExpander pScroll) { scrollContainer = pScroll; }
    public void SetBracketDataRefrence(Bracket pData) { bracketDataRefrence = pData; }
    public void SetBookViewer(BookViewer pViewer) { bookViewer = pViewer; }
    public void SetLimit(int pLimit) { limit = pLimit; }
    public void SetPercent(float pPercent) { percent = pPercent; }

    public void LoadLimitField(int pLimit) { limitField.text = "" + pLimit; SetLimit(pLimit); }
    public void LoadPercentField(float pPercent) { percentField.text = "" + pPercent; SetPercent(pPercent); }

    public void writeLimitField()
    {
        if (limitField.text.Length > 0)
        {
            int lim = int.Parse(limitField.text);
            if (lim > 999999999)
            {
                lim = 999999999;
                limitField.text = "999999999";
                limitField.MoveToEndOfLine(false, false);
            }
            else if (lim < 0)
            {
                lim = 0;
                limitField.text = "0";
                limitField.MoveToEndOfLine(false, false);
            }
            SetLimit(lim);
        }
        else
        {
            SetLimit(0);
        }
    }
    public void writePercentField()
    {
        if (percentField.text.Length > 0)
        {
            string parser = percentField.text;
            if (parser.StartsWith("."))
            {
                parser = "0" + parser;
            }
            float per = float.Parse(parser);
            if (per > 100.0f)
            {
                per = 100.0f;
                percentField.text = "100";
                percentField.MoveToEndOfLine(false, false);
            }
            else if (per < 0.0f)
            {
                per = 0.0f;
                percentField.text = "0";
                percentField.MoveToEndOfLine(false, false);
            }
            SetPercent(per);
        }
        else
        {
            SetPercent(0.0f);
        }
    }

    public void RecordAllBrackets()
    {
        bookViewer.RecordFedBracket();
        bookViewer.RecordStateBracket();
    }

    public int CompareTo(BracketMenuHook other)
    {
        if (other == null)
        {
            return 1;
        }
        else
        {
            return limit.CompareTo(other.GetLimit());
        }
    }
}
