//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System;
using UnityEngine;

public class Income : MonoBehaviour, IComparable<Income>
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private int month;
    [SerializeField] private int day;
    [SerializeField] private int year;
    [SerializeField] private string source;
    [SerializeField] private double amount;

    // getters
    public int GetMonth() { return month; }
    public int GetDay() { return day; }
    public int GetYear() { return year; }
    public string GetSource() { return source; }
    public double GetAmount() { return amount; }

    // setters
    public void SetMonth(int pMonth) { month = pMonth; }
    public void SetDay(int pDay) { day = pDay; }
    public void SetYear(int pYear) { year = pYear; }
    public void SetSource(string pSource) { source = pSource; }
    public void SetAmount(double pAmount) { amount = pAmount; }

    public void SetAll(int pMonth, int pDay, int pYear, double pAmount, string pSource)
    {
        SetMonth(pMonth);
        SetDay(pDay);
        SetYear(pYear);
        SetAmount(pAmount);
        SetSource(pSource);
    }

    public int CompareTo(Income other)
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
