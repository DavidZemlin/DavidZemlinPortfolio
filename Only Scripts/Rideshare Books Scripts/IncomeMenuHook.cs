//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System;
using UnityEngine;
using TMPro;

public class IncomeMenuHook : MonoBehaviour, IComparable<IncomeMenuHook>
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private Income incomeDataRefence;
    [SerializeField] private BookViewer bookViewer;
    [SerializeField] private ScrollViewExpander scrollContainer;

    [SerializeField] private int month;
    [SerializeField] private int day;
    [SerializeField] private int year;
    [SerializeField] private double amount;
    [SerializeField] private string source;

    [SerializeField] private TMP_InputField monthField;
    [SerializeField] private TMP_InputField dayField;
    [SerializeField] private TMP_InputField yearField;
    [SerializeField] private TMP_InputField amountField;
    [SerializeField] private TMP_InputField sourceField;

    // getters
    public Income GetIncomeDataRefrence() { return incomeDataRefence; }
    public int GetMonth() { return month; }
    public int GetDay() { return day; }
    public int GetYear() { return year; }
    public double GetAmount() { return amount; }
    public string GetSource() { return source; }

    // setters
    public void SetScrollContainer(ScrollViewExpander pScroll) { scrollContainer = pScroll; }
    public void SetIncomeDataRefrence(Income pIncome) { incomeDataRefence = pIncome; }
    public void SetBookViewer(BookViewer pViewer) { bookViewer = pViewer; }
    public void SetMonth(int pMonth) { month = pMonth; }
    public void SetDay(int pDay) { day = pDay; }
    public void SetYear(int pYear) { year = pYear; }
    public void SetAmount(double pAmount) { amount = pAmount; }
    public void SetSource(string pSource) { source = pSource; }

    // loaders
    public void LoadMonthField(int pMonth)        { monthField.text = "" + pMonth; SetMonth(pMonth); }
    public void LoadDayField(int pDay)                { dayField.text = "" + pDay; SetDay(pDay); }
    public void LoadYearField(int pYear)            { yearField.text = "" + pYear; SetYear(pYear); }
    public void LoadAmountField(double pAmount) { amountField.text = "" + pAmount; SetAmount(pAmount); }
    public void LoadSourceField(string pSource)      { sourceField.text = pSource; SetSource(pSource); }

    // writers
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

    public void WriteAmount()
    {
        if (amountField.text.Length > 0)
        {
            string parser = amountField.text;
            if (parser.StartsWith("."))
            {
                parser = "0" + parser;
            }
            double amount = double.Parse(parser);
            if (amount > 999999.99)
            {
                amount = 999999.99;
                amountField.text = "999999.99";
                amountField.MoveToEndOfLine(false, false);
            }
            else if (amount < 0)
            {
                amountField.text = "0";
                amountField.MoveToEndOfLine(false, false);
                amount = 0;
            }
            SetAmount(amount);
        }
        else
        {
            SetAmount(0);
        }
    }

    public void WriteSource()
    {
        if (sourceField.text.Length > 0)
        {
            SetSource(sourceField.text);
        }
        else
        {
            SetSource("");
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
                        amountField.Select();
                    }
                }
            }
        }
    }

    public void RemoveIncome()
    {
        bookViewer.RemoveIncome(this);
    }

    public void UpdateHeader()
    {
        bookViewer.RecordIncome();
        bookViewer.UpdateHeaderNumbers();
    }

    public int CompareTo(IncomeMenuHook other)
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
