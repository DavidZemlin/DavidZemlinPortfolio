//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System;
using UnityEngine;

public class Drive : MonoBehaviour, IComparable<Drive>
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private bool deductable;
    [SerializeField] private int startMonth;
    [SerializeField] private int startDay;
    [SerializeField] private int startYear;
    [SerializeField] private int startHr;
    [SerializeField] private int startMin;
    [SerializeField] private int startOdometer;
    [SerializeField] private int endMonth;
    [SerializeField] private int endDay;
    [SerializeField] private int endYear;
    [SerializeField] private int endHr;
    [SerializeField] private int endMin;
    [SerializeField] private int endOdometer;
    [SerializeField] private float miles;

    // getters
    public bool GetDeductable() { return deductable; }
    public int GetStartMonth() { return startMonth; }
    public int GetStartDay() { return startDay; }
    public int GetStartYear() { return startYear; }
    public int GetStartHr() { return startHr; }
    public int GetStartMin() { return startMin; }
    public int GetStartOdometer() { return startOdometer; }
    public int GetEndMonth() { return endMonth; }
    public int GetEndDay() { return endDay; }
    public int GetEndYear() { return endYear; }
    public int GetEndHr() { return endHr; }
    public int GetEndMin() { return endMin; }
    public int GetEndOdometer() { return endOdometer; }
    public float GetMiles() { return miles; }

    // setters
    public void SetDeductable(bool pDeductable) { deductable = pDeductable; }
    public void SetStartMonth(int pStartMonth) { startMonth = pStartMonth; }
    public void SetStartDay(int pStartDay) { startDay = pStartDay; }
    public void SetStartYear(int pStartYear) { startYear = pStartYear; }
    public void SetStartHr(int pStartHr) { startHr = pStartHr; }
    public void SetStartMin(int pStartMin) { startMin = pStartMin; }
    public void SetStartOdometer(int pStartOdometer) { startOdometer = pStartOdometer; }
    public void SetEndMonth(int pEndMonth) { endMonth = pEndMonth; }
    public void SetEndDay(int pEndDay) { endDay = pEndDay; }
    public void SetEndYear(int pEndYear) { endYear = pEndYear; }
    public void SetEndHr(int pEndHr) { endHr = pEndHr; }
    public void SetEndMin(int pEndMin) { endMin = pEndMin; }
    public void SetEndOdometer(int pEndOdometer) { endOdometer = pEndOdometer; }
    public void SetMiles(float pMiles) { miles = pMiles; }

    public void SetAll(bool pDeductable, int pStartMonth, int pStartDay, int pStartYear, int pStartHr, int pStartMin, int pStartOdometer, int pEndMonth, int pEndDay, int pEndYear, int pEndHr, int pEndMin, int pEndOdometer, float pMiles)
    {
        SetDeductable(pDeductable);
        SetStartMonth(pStartMonth);
        SetStartDay(pStartDay);
        SetStartYear(pStartYear);
        SetStartHr(pStartHr);
        SetStartMin(pStartMin);
        SetStartOdometer(pStartOdometer);
        SetEndMonth(pEndMonth);
        SetEndDay(pEndDay);
        SetEndYear(pEndYear);
        SetEndHr(pEndHr);
        SetEndMin(pEndMin);
        SetEndOdometer(pEndOdometer);
        SetMiles(pMiles);
    }

    public int CompareTo(Drive other)
    {
        if (other == null)
        {
            return 1;
        }
        else
        {
            int myDate = (startYear * 100000000) + (startMonth * 1000000) + (startDay * 10000) + (startHr * 100) + startMin;
            int otherDate = (other.startYear * 100000000) + (other.startMonth * 1000000) + (other.startDay * 10000) + (other.startHr * 100) + other.startMin;
            return myDate.CompareTo(otherDate);
        }
    }
}
