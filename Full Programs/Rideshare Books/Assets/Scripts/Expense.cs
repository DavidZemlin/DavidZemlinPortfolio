//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System;
using UnityEngine;

public class Expense : MonoBehaviour, IComparable<Expense>
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private bool deductable;
    [SerializeField] private int month;
    [SerializeField] private int day;
    [SerializeField] private int year;
    [SerializeField] private string note;
    [SerializeField] private double cost;

    // getters
    public bool GetDeductable() { return deductable; }
    public int GetMonth() { return month; }
    public int GetDay() { return day; }
    public int GetYear() { return year; }
    public string GetNote() { return note; }
    public double GetCost() { return cost; }

    // setters
    public void SetDeductable(bool pDeductable) { deductable = pDeductable; }
    public void SetMonth(int pMonth) { month = pMonth; }
    public void SetDay(int pDay) { day = pDay; }
    public void SetYear(int pYear) { year = pYear; }
    public void SetNote(string pNote) { note = pNote; }
    public void SetCost(double pCost) { cost = pCost; }

    public void SetAll(bool pDeductable, int pMonth, int pDay, int pYear, double pCost, string pNote)
    {
        SetDeductable(pDeductable);
        SetMonth(pMonth);
        SetDay(pDay);
        SetYear(pYear);
        SetCost(pCost);
        SetNote(pNote);
    }

    public int CompareTo(Expense other)
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
