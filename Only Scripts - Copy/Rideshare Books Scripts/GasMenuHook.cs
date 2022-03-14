//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GasMenuHook : MonoBehaviour, IComparable<GasMenuHook>
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private Expense gasDataRefence;
    [SerializeField] private BookViewer bookViewer;
    [SerializeField] private ScrollViewExpander scrollContainer;

    [SerializeField] private bool deductable;
    [SerializeField] private int month;
    [SerializeField] private int day;
    [SerializeField] private int year;
    [SerializeField] private double cost;
    [SerializeField] private string note;

    [SerializeField] private Toggle deductableToggle;
    [SerializeField] private TMP_InputField monthField;
    [SerializeField] private TMP_InputField dayField;
    [SerializeField] private TMP_InputField yearField;
    [SerializeField] private TMP_InputField costField;
    [SerializeField] private TMP_InputField noteField;

    // getters
    public Expense GetGasDataRefrence() { return gasDataRefence; }
    public bool GetDeductable() { return deductable; }
    public int GetMonth() { return month; }
    public int GetDay() { return day; }
    public int GetYear() { return year; }
    public double GetCost() { return cost; }
    public string GetNote() { return note; }

    // setters
    public void SetScrollContainer(ScrollViewExpander pScroll) { scrollContainer = pScroll; }
    public void SetGasDataRefrence(Expense pGas) { gasDataRefence = pGas; }
    public void SetBookViewer(BookViewer pViewer) { bookViewer = pViewer; }
    public void SetDeductable(bool pDeductable) { deductable = pDeductable; }
    public void SetMonth(int pMonth) { month = pMonth; }
    public void SetDay(int pDay) { day = pDay; }
    public void SetYear(int pYear) { year = pYear; }
    public void SetCost(double pCost) { cost = pCost; }
    public void SetNote(string pNote) { note = pNote; }

    // loaders
    public void LoadDeductableField(bool pOn) { deductableToggle.isOn = pOn; SetDeductable(pOn); }
    public void LoadMonthField(int pMonth)  { monthField.text = "" + pMonth; SetMonth(pMonth); }
    public void LoadDayField(int pDay)          { dayField.text = "" + pDay; SetDay(pDay); }
    public void LoadYearField(int pYear)      { yearField.text = "" + pYear; SetYear(pYear); }
    public void LoadCostField(double pCost)   { costField.text = "" + pCost; SetCost(pCost); }
    public void LoadNoteField(string pNote)        { noteField.text = pNote; SetNote(pNote); }

    // writers
    public void WriteDeductable()
    {
        deductable = deductableToggle.isOn;
    }

    public void WriteMonth()
    {
        if (monthField.text.Length > 0)
        {
            int month = int.Parse(monthField.text);
            if (month > 12)
            {
                month = 12;
                monthField.text = "12";
                monthField.MoveToEndOfLine(false, false);
            }
            else if (month < 1)
            {
                month = 1;
                monthField.text = "1";
                monthField.MoveToEndOfLine(false, false);
            }
            SetMonth(month);
        }
        else
        {
            SetMonth(1);
        }
    }

    public void WriteDay()
    {
        if (dayField.text.Length > 0)
        {
            int day = int.Parse(dayField.text);
            if (day > 31)
            {
                day = 31;
                dayField.text = "31";
                dayField.MoveToEndOfLine(false, false);
            }
            else if (day < 1)
            {
                day = 1;
                dayField.text = "1";
                dayField.MoveToEndOfLine(false, false);
            }
            SetDay(day);
        }
        else
        {
            SetDay(1);
        }
    }

    public void WriteYear()
    {
        if (yearField.text.Length > 0)
        {
            int year = int.Parse(yearField.text);
            if (year > 9999)
            {
                year = 9999;
                yearField.text = "9999";
                yearField.MoveToEndOfLine(false, false);
            }
            else if (year < 0)
            {
                year = 0;
                yearField.text = "0";
                yearField.MoveToEndOfLine(false, false);
            }
            SetYear(year);
        }
        else
        {
            SetYear(0);
        }
    }

    public void WriteCost()
    {
        if (costField.text.Length > 0)
        {
            string parser = costField.text;
            if (parser.StartsWith("."))
            {
                parser = "0" + parser;
            }
            double cos = double.Parse(parser);
            if (cos > 999999.99)
            {
                cos = 999999.99;
                costField.text = "999999.99";
                costField.MoveToEndOfLine(false, false);
            }
            else if (cos < 0)
            {
                costField.text = "0";
                costField.MoveToEndOfLine(false, false);
                cos = 0;
            }
            SetCost(cos);
        }
        else
        {
            SetCost(0);
        }
    }

    public void WriteNote()
    {
        if (noteField.text.Length > 0)
        {
            SetNote(noteField.text);
        }
        else
        {
            SetNote("");
        }
    }

    public void CheckForJump(TMP_InputField pActiveField)
    {
        if (pActiveField.isFocused)
        {
            if (bookViewer.GetAutoJumpDateAndTime())
            {
                if (pActiveField.text.Length > 3)
                {
                    if (pActiveField == yearField)
                    {
                        WriteYear();
                        monthField.Select();
                    }
                }
                else if (pActiveField.text.Length > 1)
                {
                    if (pActiveField == monthField)
                    {
                        WriteMonth();
                        dayField.Select();
                    }

                    else if (pActiveField == dayField)
                    {
                        WriteDay();
                        costField.Select();
                    }
                }
            }
        }
    }

    public void RemoveGas()
    {
        bookViewer.RemoveGas(this);
    }

    public void UpdateHeader()
    {
        bookViewer.RecordGas();
        bookViewer.UpdateHeaderNumbers();
    }

    public int CompareTo(GasMenuHook other)
    {
        if (other == null)
        {
            return 1;
        }
        else
        {
            int myDate = (year * 10000) + (month * 100) + day;
            int otherDate = (other.year * 10000) + (other.month * 100) + other.day;
            return myDate.CompareTo(otherDate);
        }
    }
}

