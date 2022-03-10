//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MileageMenuHook : MonoBehaviour, IComparable<MileageMenuHook>
{
    private const float MAX_MILEAGE_VARIANCE = 2.0f;

    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private Image startOdometerImage;
    [SerializeField] private Image endOdometerImage;
    [SerializeField] private Image milesImage;
    [SerializeField] private Color warningColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private Drive driveDataRefence;
    [SerializeField] private BookViewer bookViewer;
    [SerializeField] private ScrollViewExpander scrollContainer;

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

    [SerializeField] private Toggle deductableToggle;
    [SerializeField] private TMP_InputField startMonthField;
    [SerializeField] private TMP_InputField startDayField;
    [SerializeField] private TMP_InputField startYearField;
    [SerializeField] private TMP_InputField startHrField;
    [SerializeField] private TMP_InputField startMinField;
    [SerializeField] private TMP_Dropdown startAmPm;
    [SerializeField] private TMP_InputField startOdometerField;
    [SerializeField] private TMP_InputField endMonthField;
    [SerializeField] private TMP_InputField endDayField;
    [SerializeField] private TMP_InputField endYearField;
    [SerializeField] private TMP_InputField endHrField;
    [SerializeField] private TMP_InputField endMinField;
    [SerializeField] private TMP_Dropdown endAmPm;
    [SerializeField] private TMP_InputField endOdometerField;
    [SerializeField] private TMP_InputField milesField;

    // getters
    public Drive GetDriveDataRefrence() { return driveDataRefence; }
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
    public void SetScrollContainer(ScrollViewExpander pScroll) { scrollContainer = pScroll; }
    public void SetDriveDataRefrence(Drive pDrive) { driveDataRefence = pDrive; }
    public void SetBookViewer(BookViewer pViewer) { bookViewer = pViewer; }
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

    // loaders
    public void LoadDeductableField(bool pOn)                      { deductableToggle.isOn = pOn; SetDeductable(pOn); }
    public void LoadStartMonthField(int pMonth)             { startMonthField.text = "" + pMonth; SetStartMonth(pMonth); }
    public void LoadStartDayField(int pDay)                     { startDayField.text = "" + pDay; SetStartDay(pDay); }
    public void LoadStartYearField(int pYear)                 { startYearField.text = "" + pYear; SetStartYear(pYear); }
    public void LoadStartHrField(int pHr)
    {
        int hr = pHr;

        if ( hr < 12)
        {
            endAmPm.value = 0; // this needs testing----------------------------------
        }
        else
        {
            endAmPm.value = 1; // this needs testing----------------------------------
        }

        if (hr == 0)
        {
            hr = 12;
        }
        else if (hr > 12)
        {
            hr = hr - 12;
        }

        startHrField.text = "" + hr;
        SetStartHr(pHr);
    }
    public void LoadStartMinField(int pMin)                     { startMinField.text = "" + pMin; SetStartMin(pMin); }
    public void LoadStartOdometerField(int pOdometer) { startOdometerField.text = "" + pOdometer; SetStartOdometer(pOdometer); }
    public void LoadEndMonthField(int pMonth)                 { endMonthField.text = "" + pMonth; SetEndMonth(pMonth); }
    public void LoadEndDayField(int pDay)                         { endDayField.text = "" + pDay; SetEndDay(pDay); }
    public void LoadEndYearField(int pYear)                     { endYearField.text = "" + pYear; SetEndYear(pYear); }
    public void LoadEndHrField(int pHr)
    {
        int hr = pHr;

        if (hr < 12)
        {
            endAmPm.value = 0; // this needs testing----------------------------------
        }
        else
        {
            endAmPm.value = 1; // this needs testing----------------------------------
        }

        if (hr == 0)
        {
            hr = 12;
        }
        else if (hr > 12)
        {
            hr = hr - 12;
        }

        endHrField.text = "" + hr;
        SetEndHr(pHr);
    }
    public void LoadEndMinField(int pMin)                         { endMinField.text = "" + pMin; SetEndMin(pMin); }
    public void LoadEndOdometerField(int pOdometer)     { endOdometerField.text = "" + pOdometer; SetEndOdometer(pOdometer); }
    public void LoadMiles(float pMiles)                          { milesField.text = "" + pMiles; SetMiles(pMiles); }

    // writers
    public void WriteDeductable()
    {
        deductable = deductableToggle.isOn;
    }

    public void WriteStartMonth()
    {
        if (startMonthField.text.Length > 0)
        {
            int month = int.Parse(startMonthField.text);
            if (month > 12)
            {
                month = 12;
                startMonthField.text = "12";
                startMonthField.MoveToEndOfLine(false, false);
            }
            else if (month < 1)
            {
                month = 1;
                startMonthField.text = "1";
                startMonthField.MoveToEndOfLine(false, false);
            }
            SetStartMonth(month);
        }
        else
        {
            SetStartMonth(1);
        }
    }

    public void WriteStartDay()
    {
        if (startDayField.text.Length > 0)
        {
            int day = int.Parse(startDayField.text);
            if (day > 31)
            {
                day = 31;
                startDayField.text = "31";
                startDayField.MoveToEndOfLine(false, false);
            }
            else if (day < 1)
            {
                day = 1;
                startDayField.text = "1";
                startDayField.MoveToEndOfLine(false, false);
            }
            SetStartDay(day);
        }
        else
        {
            SetStartDay(1);
        }
    }

    public void WriteStartYear()
    {
        if (startYearField.text.Length > 0)
        {
            int year = int.Parse(startYearField.text);
            if (year > 9999)
            {
                year = 9999;
                startYearField.text = "9999";
                startYearField.MoveToEndOfLine(false, false);
            }
            else if (year < 0)
            {
                year = 0;
                startYearField.text = "0";
                startYearField.MoveToEndOfLine(false, false);
            }
            SetStartYear(year);
        }
        else
        {
            SetStartYear(0);
        }
    }

    public void WriteStartHr()
    {
        int hr;
        if (startHrField.text.Length > 0)
        {
            hr = int.Parse(startHrField.text);
            if (hr > 12)
            {
                hr = 12;
                startHrField.text = "12";
                startHrField.MoveToEndOfLine(false, false);
            }
            if (hr < 1)
            {
                hr = 1;
                startHrField.text = "1";
                startHrField.MoveToEndOfLine(false, false);
            }
        }
        else
        {
            hr = 1;
        }

        if (startAmPm.value == 0)
        {
            if (hr == 12)
            {
                hr = 0;
            }
        }
        else
        {
            if (hr < 12)
            {
                hr += 12;
            }
        }
        startHr = hr;
    }

    public void WriteStartMin()
    {
        if (startMinField.text.Length > 0)
        {
            int min = int.Parse(startMinField.text);
            if (min > 59)
            {
                min = 59;
                startMinField.text = "59";
                startMinField.MoveToEndOfLine(false, false);
            }
            else if (min < 0)
            {
                min = 0;
                startMinField.text = "0";
                startMinField.MoveToEndOfLine(false, false);
            }
            SetStartMin(min);
        }
        else
        {
            SetStartMin(0);
        }
    }

    public void WriteStartOdometer()
    {
        if(startOdometerField.text.Length > 0)
        {
            int odo = int.Parse(startOdometerField.text);
            if (odo > 9999999)
            {
                odo = 9999999;
                startOdometerField.text = "9999999";
                startOdometerField.MoveToEndOfLine(false, false);
            }
            else if (odo < 0)
            {
                odo = 0;
                startOdometerField.text = "0";
                startOdometerField.MoveToEndOfLine(false, false);
            }
            SetStartOdometer(odo);
        }
        else
        {
            SetStartOdometer(0);
        }
    }

    public void WriteEndMonth()
    {
        if (endMonthField.text.Length > 0)
        {
            int month = int.Parse(endMonthField.text);
            if (month > 12)
            {
                month = 12;
                endMonthField.text = "12";
                endMonthField.MoveToEndOfLine(false, false);
            }
            else if (month < 1)
            {
                month = 1;
                endMonthField.text = "1";
                endMonthField.MoveToEndOfLine(false, false);
            }
            SetEndMonth(month);
        }
        else
        {
            SetEndMonth(1);
        }
    }

    public void WriteEndDay()
    {
        if (endDayField.text.Length > 0)
        {
            int day = int.Parse(endDayField.text);
            if (day > 31)
            {
                day = 31;
                endDayField.text = "31";
                endDayField.MoveToEndOfLine(false, false);
            }
            else if (day < 1)
            {
                day = 1;
                endDayField.text = "1";
                endDayField.MoveToEndOfLine(false, false);
            }
            SetEndDay(day);
        }
        else
        {
            SetEndDay(1);
        }
    }

    public void WriteEndYear()
    {
        if (endYearField.text.Length > 0)
        {
            int year = int.Parse(endYearField.text);
            if (year > 9999)
            {
                year = 9999;
                endYearField.text = "9999";
                endYearField.MoveToEndOfLine(false, false);
            }
            else if (year < 0)
            {
                year = 0;
                endYearField.text = "0";
                endYearField.MoveToEndOfLine(false, false);
            }
            SetEndYear(year);
        }
        else
        {
            SetEndYear(0);
        }
    }

    public void WriteEndHr()
    {
        int hr;
        if (endHrField.text.Length > 0)
        {
            hr = int.Parse(endHrField.text);
            if (hr > 12)
            {
                hr = 12;
                endHrField.text = "12";
                endHrField.MoveToEndOfLine(false, false);
            }
            if (hr < 1)
            {
                hr = 1;
                endHrField.text = "1";
                endHrField.MoveToEndOfLine(false, false);
            }
        }
        else
        {
            hr = 1;
        }

        if (endAmPm.value == 0)
        {
            if (hr == 12)
            {
                hr = 0;
            }
        }
        else
        {
            if (hr < 12)
            {
                hr += 12;
            }
        }
        endHr = hr;
    }

    public void WriteEndMin()
    {
        if (endMinField.text.Length > 0)
        {
            int min = int.Parse(endMinField.text);
            if (min > 59)
            {
                min = 59;
                endMinField.text = "59";
                endMinField.MoveToEndOfLine(false, false);
            }
            else if (min < 0)
            {
                min = 0;
                endMinField.text = "0";
                endMinField.MoveToEndOfLine(false, false);
            }
            SetEndMin(min);
        }
        else
        {
            SetEndMin(0);
        }
    }

    public void WriteEndOdometer()
    {
        if (endOdometerField.text.Length > 0)
        {
            int odo = int.Parse(endOdometerField.text);
            if (odo > 9999999)
            {
                odo = 9999999;
                endOdometerField.text = "9999999";
                endOdometerField.MoveToEndOfLine(false, false);
            }
            else if (odo < 0)
            {
                odo = 0;
                endOdometerField.text = "0";
                endOdometerField.MoveToEndOfLine(false, false);
            }
            SetEndOdometer(odo);
        }
        else
        {
            SetEndOdometer(0);
        }
    }

    public void WriteMiles()
    {
        if (milesField.text.Length > 0)
        {
            string parser = milesField.text;
            if (parser.StartsWith("."))
            {
                parser = "0" + parser;
            }
            float miles = float.Parse(parser);
            if (miles > 99999.9f)
            {
                miles = 99999.9f;
                milesField.text = "99999.9";
                milesField.MoveToEndOfLine(false, false);
            }
            else if (miles < 0)
            {
                miles = 0.0f;
                milesField.text = "0.0";
                milesField.MoveToEndOfLine(false, false);
            }
            SetMiles(miles);
        }
        else
        {
            SetMiles(0.0f);
        }
    }

    public void CheckMiles()
    {
        if (Mathf.Abs(miles - (endOdometer - startOdometer)) > MAX_MILEAGE_VARIANCE)
        {
            warningMiles(true);
        }
        else
        {
            warningMiles(false);
        }
    }

    public void CheckStartOdometer()
    {
        int index = scrollContainer.GetContents().IndexOf(gameObject);
        int prevOdoEnd = 0;
        if (index > 0)
        {
            GameObject prevDriveObject = scrollContainer.GetContents()[index - 1];
            MileageMenuHook prevDriveScript = prevDriveObject.GetComponent<MileageMenuHook>();
            prevOdoEnd = prevDriveScript.GetEndOdometer();
        }

        if (startOdometer < prevOdoEnd)
        {
            warningStartOdometer(true);
        }
        else
        {
            warningStartOdometer(false);
        }
    }

    public void CheckEndOdometer()
    {
        if (endOdometer < startOdometer)
        {
            warningEndOdometer(true);
        }
        else
        {
            warningEndOdometer(false);
        }
    }

    public void RemoveDrive()
    {
        bookViewer.RemoveDrive(this);
    }

    public void UpdateHeader()
    {
        bookViewer.RecordDrives();
        bookViewer.UpdateHeaderNumbers();
    }

    public void CheckForJump(TMP_InputField pActiveField)
    {
        if (pActiveField.isFocused)
        {
            if (bookViewer.GetAutoJumpDateAndTime())
            {
                if (pActiveField.text.Length > 3)
                {
                    if (pActiveField == startYearField)
                    {
                        WriteStartYear();
                        startMonthField.Select();
                    }

                    else if (pActiveField == endYearField)
                    {
                        WriteEndYear();
                        endMonthField.Select();
                    }
                }
                else if (pActiveField.text.Length > 1)
                {
                    if (pActiveField == startMonthField)
                    {
                        WriteStartMonth();
                        startDayField.Select();
                    }

                    else if (pActiveField == startDayField)
                    {
                        WriteStartDay();
                        startHrField.Select();
                    }

                    else if (pActiveField == startHrField)
                    {
                        WriteStartHr();
                        startMinField.Select();
                    }

                    else if (pActiveField == startMinField)
                    {
                        WriteStartMin();
                        startAmPm.Select();
                    }

                    else if (pActiveField == endMonthField)
                    {
                        WriteEndMonth();
                        endDayField.Select();
                    }

                    else if (pActiveField == endDayField)
                    {
                        WriteEndDay();
                        endHrField.Select();
                    }

                    else if (pActiveField == endHrField)
                    {
                        WriteEndHr();
                        endMinField.Select();
                    }

                    else if (pActiveField == endMinField)
                    {
                        WriteEndMin();
                        endAmPm.Select();
                    }
                }
            }
        }
    }

    public void warningStartOdometer(bool On)
    {
        if (On) startOdometerImage.color = warningColor;
        else startOdometerImage.color = normalColor;
    }

    public void warningEndOdometer(bool On)
    {
        if (On) endOdometerImage.color = warningColor;
        else endOdometerImage.color = normalColor;
    }

    public void warningMiles(bool On)
    {
        if (On) milesImage.color = warningColor;
        else milesImage.color = normalColor;
    }

    public int CompareTo(MileageMenuHook other)
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
