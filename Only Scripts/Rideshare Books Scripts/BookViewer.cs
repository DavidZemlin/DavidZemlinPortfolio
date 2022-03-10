//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BookViewer : MonoBehaviour
{
    private const string DELETE_PASSWORD = "delete!";

    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private Book book;
    [SerializeField] private TabGroup mainTabGroup;
    [SerializeField] private GameObject needBracketsText;
    [SerializeField] private GameObject jobsTab;
    [SerializeField] private GameObject totalsTab;
    [SerializeField] private GameObject exportButton;

    [Header("Tax Rates")]
    [SerializeField] private GameObject bracketPrefab;
    [SerializeField] private ScrollViewExpander fedBracketContainer;
    [SerializeField] private ScrollViewExpander stateBracketContainer;
    [SerializeField] private List<BracketMenuHook> menuFedBrackets;
    [SerializeField] private List<BracketMenuHook> menuStateBrackets;
    [SerializeField] private Image[] incompleateWarnings;
    [SerializeField] private TMP_Text selfEmploymentTaxTotal;
    [SerializeField] private TMP_InputField ssTaxField;
    [SerializeField] private TMP_InputField medicareTaxField;
    [SerializeField] private TMP_InputField mileageRateField;
    [SerializeField] private TMP_InputField taxYearField;

    [Header("Jobs")]
    [SerializeField] private Job currentJob;
    [SerializeField] private JobMenuHook currentJobMenuHook;

    [SerializeField] private List<JobMenuHook> menuJobs;
    [SerializeField] private TabGroup tabsInJobMenu;
    [SerializeField] private TMP_InputField jobDeleteField;
    [SerializeField] private TMP_InputField newJobNameField;
    [SerializeField] private GameObject jobPanel;
    [SerializeField] private GameObject jobTabPrafab;
    [SerializeField] private ScrollViewExpander jobTabContainer;

    [SerializeField] private TMP_Text jobName;
    [SerializeField] private TMP_Text jobGross;
    [SerializeField] private TMP_Text jobDeductions;
    [SerializeField] private TMP_Text jobNet;

    [Header("Drives")]
    [SerializeField] private GameObject drivePrefab;
    [SerializeField] private List<MileageMenuHook> menuDriveList;
    [SerializeField] private ScrollViewExpander driveContainer;

    [Header("Gas")]
    [SerializeField] private GameObject gasPrefab;
    [SerializeField] private List<GasMenuHook> menuGasList;
    [SerializeField] private ScrollViewExpander gasContainer;

    [Header("Expenses")]
    [SerializeField] private GameObject expensePrefab;
    [SerializeField] private List<ExpenseMenuHook> menuExpenseList;
    [SerializeField] private ScrollViewExpander expenseContainer;

    [Header("Income")]
    [SerializeField] private GameObject incomePrefab;
    [SerializeField] private List<IncomeMenuHook> menuIncomeList;
    [SerializeField] private ScrollViewExpander incomeContainer;

    [Header("YearlyTotals")]
    [SerializeField] private TMP_Text[] jobIncomeText = new TMP_Text[5];  // 0 is the total
    [SerializeField] private TMP_Text[] fedralTaxText = new TMP_Text[5];  // 0 is the total
    [SerializeField] private TMP_Text[] stateTaxText = new TMP_Text[5];   // 0 is the total
    [SerializeField] private TMP_Text piadTaxesTotalText;
    [SerializeField] private TMP_Text piadTaxesTotalBarText;
    [SerializeField] private TMP_Text piadGasText;
    [SerializeField] private TMP_Text piadTitheText;
    [SerializeField] private TMP_Text taxDueText;
    [SerializeField] private TMP_Text gasDueText;
    [SerializeField] private TMP_Text titheDueText;
    [SerializeField] private TMP_InputField[] paidTaxesField = new TMP_InputField[5]; // 0 is unused
    [SerializeField] private TMP_InputField paidGasField;
    [SerializeField] private TMP_InputField paidTitheField;
    [SerializeField] private TMP_InputField employeeIncomeField;
    [SerializeField] private Color normalBgColor;
    [SerializeField] private Color normalTextColor;
    [SerializeField] private Color normalImageColor;
    [SerializeField] private Color warningBgColor;
    [SerializeField] private Color warningTextColor;
    [SerializeField] private Color warningImageColor;
    [SerializeField] private Image paymentOutstanding;
    [SerializeField] private int taxDue;
    [SerializeField] private double gasDue;
    [SerializeField] private int titheDue;

    [SerializeField] private bool autoJumpDateAndTime;
    [SerializeField] private Toggle autoJumpDateAndTimeToggle;

    public void Awake()
    {
        if (PlayerPrefs.GetInt("AutoJumpDateAndTime") == 1)
        {
            autoJumpDateAndTime = true;
            autoJumpDateAndTimeToggle.isOn = true;
        }
    }

    public bool GetAutoJumpDateAndTime() { return autoJumpDateAndTime; }

    public void SetCurrentJob(Job pJob) { currentJob = pJob; }
    public void SetCurrentJobMenuHook(JobMenuHook pJob) { currentJobMenuHook = pJob; }
    public void SetAutoJumpDateAndTime()
    {
        autoJumpDateAndTime = autoJumpDateAndTimeToggle.isOn;
        int state = 0;
        if (autoJumpDateAndTime) state = 1;
        PlayerPrefs.SetInt("AutoJumpDateAndTime", state);
    }

    private bool open;

    public void OnApplicationQuit()
    {
        if (open)
        {
            mainTabGroup.CloseAllTabs();
            SaveBook();
            CloseBookViewer();
        }
    }

    public void OpenBookViewer()
    {
        open = true;
        LoadFedBracketsFromData();
        LoadStateBracketsFromData();
        LoadTaxMiscFromData();
        foreach(Job j in book.GetJobs())
        {
            LoadJob(j);
        }
        LoadYearlyTotals();
    }

    public void CloseBookViewer()
    {
        open = false;
        mainTabGroup.CloseAllTabs();
        ShowJobsAndTotalsTabs(false);
        book.CloseBook();

        // clear tax tab data
        foreach (BracketMenuHook b in menuFedBrackets)
        {
            Destroy(b.gameObject);
        }
        menuFedBrackets.Clear();
        menuFedBrackets = new List<BracketMenuHook>();
        foreach (BracketMenuHook b in menuStateBrackets)
        {
            Destroy(b.gameObject);
        }
        menuStateBrackets.Clear();
        menuStateBrackets = new List<BracketMenuHook>();
        ssTaxField.text = "";
        medicareTaxField.text = "";
        mileageRateField.text = "";

        // jobs tab data
        CloseJob();
        ClearAllJobsInMenu();
    }

    public void SaveBook()
    {
        book.SaveBook();
    }

    public void ExportBook()
    {
        book.ExportBook();
    }

    // Yearly Totals
    public void RecordEmployeeIncome()
    {
        double ei;
        if (employeeIncomeField.text.Length > 0)
        {
            string parser = employeeIncomeField.text;
            if (parser.StartsWith("."))
            {
                parser = "0" + parser;
            }
            double eif = double.Parse(parser);
            if (eif > 999999999.99)
            {
                eif = 999999999.99;
                employeeIncomeField.text = "999999999.99";
                employeeIncomeField.MoveToEndOfLine(false, false);
            }
            else if (eif < 0.0)
            {
                eif = 0.0;
                employeeIncomeField.text = "0";
                employeeIncomeField.MoveToEndOfLine(false, false);
            }
            ei = eif;
        }
        else
        {
            ei = 0.0;
        }

        book.GetYearlyTotals().SetEmployeeIncome(ei);
    }

    public void PayTax()
    {
        YearlyTotals yt = book.GetYearlyTotals();
        for (int i = 1; i < 5; i++)
        {
            int due = yt.GetFedTax()[i] + yt.GetStateTax()[i];
            if (yt.GetPaidTaxes()[i] < due)
            {
                yt.SetPaidTaxes(due, i);
                paidTaxesField[i].text = "" + yt.GetPaidTaxes()[i];
                paidTaxesField[i].image.color = normalBgColor;
            }
        }
    }

    public void PayGas()
    {
        YearlyTotals yt = book.GetYearlyTotals();
        if (gasDue > 0.0)
        {
            double paid = yt.GetPaidGas();
            yt.SetPaidGas(gasDue + paid);
            paidGasField.text = "" + yt.GetPaidGas();
            paidGasField.image.color = normalBgColor;
        }
    }

    public void PayTithe()
    {
        YearlyTotals yt = book.GetYearlyTotals();
        if (titheDue > 0.0)
        {
            int paid = yt.GetPaidTithe();
            yt.SetPaidTithe(titheDue + paid);
            paidTitheField.text = "" + yt.GetPaidTithe();
            paidTitheField.image.color = normalBgColor;
        }
    }

    public void RecordPaidTax(int pQuarter)
    {
        int pt;
        if (paidTaxesField[pQuarter].text.Length > 0)
        {
            int ptf = int.Parse(paidTaxesField[pQuarter].text);
            if (ptf > 999999999)
            {
                ptf = 999999999;
                paidTaxesField[pQuarter].text = "999999999";
                paidTaxesField[pQuarter].MoveToEndOfLine(false, false);
            }
            else if (ptf < 0)
            {
                ptf = 0;
                paidTaxesField[pQuarter].text = "0";
                paidTaxesField[pQuarter].MoveToEndOfLine(false, false);
            }
            pt = ptf;
        }
        else
        {
            pt = 0;
        }

        book.GetYearlyTotals().SetPaidTaxes(pt, pQuarter);
    }

    public void RecordPaidGas()
    {
        double pg;
        if (paidGasField.text.Length > 0)
        {
            string parser = paidGasField.text;
            if (parser.StartsWith("."))
            {
                parser = "0" + parser;
            }
            double pgf = double.Parse(parser);
            if (pgf > 999999999.99)
            {
                pgf = 999999999.99;
                paidGasField.text = "999999999.99";
                paidGasField.MoveToEndOfLine(false, false);
            }
            else if (pgf < 0.0)
            {
                pgf = 0.0;
                paidGasField.text = "0";
                paidGasField.MoveToEndOfLine(false, false);
            }
            pg = pgf;
        }
        else
        {
            pg = 0.0;
        }

        book.GetYearlyTotals().SetPaidGas(pg);
    }

    public void RecordPaidTithe()
    {
        int pt;
        if (paidTitheField.text.Length > 0)
        {
            int ptf = int.Parse(paidTitheField.text);
            if (ptf > 999999999)
            {
                ptf = 999999999;
                paidTitheField.text = "999999999";
                paidTitheField.MoveToEndOfLine(false, false);
            }
            else if (ptf < 0)
            {
                ptf = 0;
                paidTitheField.text = "0";
                paidTitheField.MoveToEndOfLine(false, false);
            }
            pt = ptf;
        }
        else
        {
            pt = 0;
        }

        book.GetYearlyTotals().SetPaidTithe(pt);
    }

    public void LoadYearlyTotals()
    {
        taxDue = 0;
        gasDue = 0.0;
        titheDue = 0;
        paymentOutstanding.color = normalImageColor;

        YearlyTotals yt = book.GetYearlyTotals();
        yt.UpdateYearlyTotals();
        exportButton.SetActive(true);

        employeeIncomeField.text = "" + yt.GetEmployeeIncome();
        paidGasField.text = "" + yt.GetPaidGas();
        paidTitheField.text = "" + yt.GetPaidTithe();

        piadTaxesTotalBarText.text = "" + yt.GetPaidTaxes()[0];
        piadGasText.text = "" + yt.GetPaidGas();
        piadTitheText.text = "" + yt.GetPaidTithe();

        piadTaxesTotalText.text = "" + yt.GetPaidTaxes()[0];
        paidTaxesField[1].text = "" + yt.GetPaidTaxes()[1];
        paidTaxesField[2].text = "" + yt.GetPaidTaxes()[2];
        paidTaxesField[3].text = "" + yt.GetPaidTaxes()[3];
        paidTaxesField[4].text = "" + yt.GetPaidTaxes()[4];

        jobIncomeText[0].text = "" + MathExtra.RoundUpDouble(yt.GetJobIncome()[0], 2);
        jobIncomeText[1].text = "" + MathExtra.RoundUpDouble(yt.GetJobIncome()[1], 2);
        jobIncomeText[2].text = "" + MathExtra.RoundUpDouble(yt.GetJobIncome()[2], 2);
        jobIncomeText[3].text = "" + MathExtra.RoundUpDouble(yt.GetJobIncome()[3], 2);
        jobIncomeText[4].text = "" + MathExtra.RoundUpDouble(yt.GetJobIncome()[4], 2);

        fedralTaxText[0].text = "" + yt.GetFedTax()[0];
        fedralTaxText[1].text = "" + yt.GetFedTax()[1];
        fedralTaxText[2].text = "" + yt.GetFedTax()[2];
        fedralTaxText[3].text = "" + yt.GetFedTax()[3];
        fedralTaxText[4].text = "" + yt.GetFedTax()[4];

        stateTaxText[0].text = "" + yt.GetStateTax()[0];
        stateTaxText[1].text = "" + yt.GetStateTax()[1];
        stateTaxText[2].text = "" + yt.GetStateTax()[2];
        stateTaxText[3].text = "" + yt.GetStateTax()[3];
        stateTaxText[4].text = "" + yt.GetStateTax()[4];

        CheckPaidTaxes();

        double gasTotal = 0.0;
        foreach (Job j in book.GetJobs())
        {
            gasTotal += j.GetTotalGasExpences();
        }

        int titheTotal =(int) Math.Ceiling((((int) Math.Ceiling(yt.GetJobIncome()[0])) - yt.GetFedTax()[0] - yt.GetStateTax()[0]) * 0.1);

        taxDue = yt.GetFedTax()[0] + yt.GetStateTax()[0] - yt.GetPaidTaxes()[0];
        gasDue = MathExtra.RoundDownDouble(gasTotal, 2) - yt.GetPaidGas();
        titheDue = titheTotal - yt.GetPaidTithe();

        taxDueText.color = normalTextColor;
        gasDueText.color = normalTextColor;
        titheDueText.color = normalTextColor;
        paidGasField.image.color = normalBgColor;
        paidTitheField.image.color = normalBgColor;

        if (taxDue > 0)
        {
            taxDueText.color = warningTextColor;
            paymentOutstanding.color = warningImageColor;
        }
        if (gasDue > 0.0)
        {
            gasDueText.color = warningTextColor;
            paidGasField.image.color = warningBgColor;
            paymentOutstanding.color = warningImageColor;
        }
        if (titheDue > 0.0)
        {
            titheDueText.color = warningTextColor;
            paidTitheField.image.color = warningBgColor;
            paymentOutstanding.color = warningImageColor;
        }

        taxDueText.text = "" + taxDue;
        gasDueText.text = "" + gasDue;
        titheDueText.text = "" + titheDue;
    }

    private void CheckPaidTaxes()
    {
        YearlyTotals yt = book.GetYearlyTotals();
        for (int i = 1; i < 5; i++)
        {
            if (yt.GetPaidTaxes()[i] < yt.GetFedTax()[i] + yt.GetStateTax()[i])
            {
                paidTaxesField[i].image.color = warningBgColor;
            }
            else
            {
                paidTaxesField[i].image.color = normalBgColor;
            }
        }
    }

    // Tax Rates Menu

    public void SetTaxRateStatusFlag(int pFlagIndex, Color pColor)
    {
        incompleateWarnings[pFlagIndex].color = pColor;
        ShowJobsAndTotalsTabs(menuFedBrackets.Count > 0 && menuStateBrackets.Count > 0);
    }

    public void ShowJobsAndTotalsTabs(bool pYesNo)
    {
        jobsTab.SetActive(pYesNo);
        totalsTab.SetActive(pYesNo);
        needBracketsText.SetActive(!pYesNo);
    }

    public void RecordSocSecTaxRate()
    {
        float ss;
        if (ssTaxField.text.Length > 0)
        {
            string parser = ssTaxField.text;
            if (parser.StartsWith("."))
            {
                parser = "0" + parser;
            }
            float ssf = float.Parse(parser);
            if (ssf > 100.0f)
            {
                ssf = 100.0f;
                ssTaxField.text = "100";
                ssTaxField.MoveToEndOfLine(false, false);
            }
            else if (ssf < 0.0f)
            {
                ssf = 0.0f;
                ssTaxField.text = "0";
                ssTaxField.MoveToEndOfLine(false, false);
            }
            ss = ssf;
        }
        else
        {
            ss = 0.0f;
        }

        book.GetTaxRates().SetSoSecTaxeRate(ss);
    }

    public void RecordMedicareTaxRate()
    {
        float med;

        if (medicareTaxField.text.Length > 0)
        {
            string parser = medicareTaxField.text;
            if (parser.StartsWith("."))
            {
                parser = "0" + parser;
            }
            float medf = float.Parse(parser);
            if (medf > 100.0f)
            {
                medf = 100.0f;
                medicareTaxField.text = "100";
                medicareTaxField.MoveToEndOfLine(false, false);
            }
            else if (medf < 0.0f)
            {
                medf = 0.0f;
                medicareTaxField.text = "0";
                medicareTaxField.MoveToEndOfLine(false, false);
            }
            med = medf;
        }
        else
        {
            med = 0.0f;
        }

        book.GetTaxRates().SetMedicareTaxRate(med);
    }

    public void UpdateSelfEmploymentTaxRates()
    {
        float seTax = book.GetTaxRates().SelfEmploymentTax();
        selfEmploymentTaxTotal.text = "Self Employment Tax = " + seTax + "%";

        if (seTax > 0)
        {
            SetTaxRateStatusFlag(2, Color.green);
        }
        else
        {
            SetTaxRateStatusFlag(2, Color.red);
        }
    }

    public void RecordMileageRate()
    {
        float value = 0.0f;
        if (mileageRateField.text.Length > 0)
        {
            string parser = mileageRateField.text;
            if (parser.StartsWith("."))
            {
                parser = "0" + parser;
            }
            float mr = float.Parse(parser);
            if (mr > 999.9f)
            {
                mr = 999.9f;
                mileageRateField.text = "999.9";
                mileageRateField.MoveToEndOfLine(false, false);
            }
            else if (mr < 0.0f)
            {
                mr = 0.0f;
                mileageRateField.text = "0";
                mileageRateField.MoveToEndOfLine(false, false);
            }
            book.GetTaxRates().SetMileageRate(mr);
            value = mr;
        }
        else
        {
            book.GetTaxRates().SetMileageRate(0.0f);
            value = 0.0f;
        }

        if (value > 0.0f)
        {
            SetTaxRateStatusFlag(3, Color.green);
        }
        else
        {
            SetTaxRateStatusFlag(3, Color.red);
        }
    }

    public void RecordTaxYear()
    {
        int value = 0;
        if (taxYearField.text.Length > 0)
        {
            int year = int.Parse(taxYearField.text);
            if (year > 9999)
            {
                year = 9999;
                taxYearField.text = "999999999";
                taxYearField.MoveToEndOfLine(false, false);
            }
            else if (year < 0)
            {
                year = 0;
                taxYearField.text = "0";
                taxYearField.MoveToEndOfLine(false, false);
            }
            book.GetTaxRates().SetTaxYear(year);
            value = year;
        }
        else
        {
            book.GetTaxRates().SetTaxYear(0);
        }

        if (value > 0)
        {
            SetTaxRateStatusFlag(4, Color.green);
        }
        else
        {
            SetTaxRateStatusFlag(4, Color.red);
        }
    }

    public void LoadTaxMiscFromData()
    {
        taxYearField.text = "" + book.GetTaxRates().GetTaxYear();
        ssTaxField.text = "" + book.GetTaxRates().GetSoSecTaxeRate();
        medicareTaxField.text = "" + book.GetTaxRates().GetMedicareTaxRate();
        mileageRateField.text = "" + book.GetTaxRates().GetMileageRate();
    }

    // Fed Tax Brackets
    public void AddNewFedBracket()
    {
        Bracket fedBracketData = book.GetTaxRates().AddFedBracket();
        GameObject newBracket = Instantiate(bracketPrefab);
        BracketMenuHook bracketHook = newBracket.GetComponent<BracketMenuHook>();
        menuFedBrackets.Add(bracketHook);
        fedBracketContainer.AddItem(newBracket);
        bracketHook.SetScrollContainer(fedBracketContainer);
        bracketHook.SetBracketDataRefrence(fedBracketData);
        bracketHook.SetBookViewer(this);
        SetTaxRateStatusFlag(0, Color.green);
    }

    public void RemoveFedBracket()
    {
        if (menuFedBrackets.Count > 0)
        {
            BracketMenuHook bracketToRemove = menuFedBrackets[menuFedBrackets.Count - 1];
            book.GetTaxRates().RemoveFedBracket(menuFedBrackets[menuFedBrackets.Count - 1].GetBracketDataRefrence());
            fedBracketContainer.RemoveItem(bracketToRemove.gameObject);
            menuFedBrackets.Remove(bracketToRemove);
            Destroy(bracketToRemove.gameObject);
        }
        else
        {
            SetTaxRateStatusFlag(0, Color.red);
        }
    }

    public void RecordFedBracket()
    {
        foreach (BracketMenuHook fb in menuFedBrackets)
        {
            int limit = fb.GetLimit();
            float precent = fb.GetPercent();

            fb.GetBracketDataRefrence().SetAll(limit, precent);
        }
    }

    public void LoadFedBracketsFromData()
    {
        foreach (Bracket brac in book.GetTaxRates().GetFedTaxBrackets())
        {
            GameObject newBracket = Instantiate(bracketPrefab);
            BracketMenuHook bracketHook = newBracket.GetComponent<BracketMenuHook>();
            menuFedBrackets.Add(bracketHook);
            fedBracketContainer.AddItem(newBracket);
            bracketHook.SetScrollContainer(fedBracketContainer);
            bracketHook.SetBracketDataRefrence(brac);
            bracketHook.SetBookViewer(this);

            int limit = brac.GetLimit();
            float percent = brac.GetPercent();

            bracketHook.LoadLimitField(limit);
            bracketHook.LoadPercentField(percent);

            if (menuFedBrackets.Count > 0)
            {
                SetTaxRateStatusFlag(0, Color.green);
            }
            else
            {
                SetTaxRateStatusFlag(0, Color.red);
            }
        }
    }

    public void ClearFedBracketListInMenu()
    {
        foreach (BracketMenuHook mb in menuFedBrackets)
        {
            fedBracketContainer.RemoveItem(mb.gameObject);
            Destroy(mb.gameObject);
        }
        menuFedBrackets.Clear();
        menuFedBrackets = new List<BracketMenuHook>();
        SetTaxRateStatusFlag(0, Color.red);
    }

    public void SortFedBrackets()
    {
        book.GetTaxRates().SortFedBrackets();
        menuFedBrackets.Sort();
        fedBracketContainer.ClearContainer();
        foreach (BracketMenuHook bm in menuFedBrackets)
        {
            fedBracketContainer.AddItem(bm.gameObject);
        }
        fedBracketContainer.RepositionItems();
    }

    // State Tax Brackets
    public void AddNewStateBracket()
    {
        Bracket stateBracketData = book.GetTaxRates().AddStateBracket();
        GameObject newBracket = Instantiate(bracketPrefab);
        BracketMenuHook bracketHook = newBracket.GetComponent<BracketMenuHook>();
        menuStateBrackets.Add(bracketHook);
        stateBracketContainer.AddItem(newBracket);
        bracketHook.SetScrollContainer(stateBracketContainer);
        bracketHook.SetBracketDataRefrence(stateBracketData);
        bracketHook.SetBookViewer(this);
        SetTaxRateStatusFlag(1, Color.green);
    }

    public void RemoveStateBracket()
    {
        if (menuStateBrackets.Count > 0)
        {
            BracketMenuHook bracketToRemove = menuStateBrackets[menuStateBrackets.Count - 1];
            book.GetTaxRates().RemoveStateBracket(bracketToRemove.GetBracketDataRefrence());
            stateBracketContainer.RemoveItem(bracketToRemove.gameObject);
            menuStateBrackets.Remove(bracketToRemove);
            Destroy(bracketToRemove.gameObject);
        }
        else
        {
            SetTaxRateStatusFlag(1, Color.red);
        }
    }

    public void RecordStateBracket()
    {
        foreach (BracketMenuHook fb in menuStateBrackets)
        {
            int limit = fb.GetLimit();
            float precent = fb.GetPercent();

            fb.GetBracketDataRefrence().SetAll(limit, precent);
        }
    }

    public void LoadStateBracketsFromData()
    {
        foreach (Bracket brac in book.GetTaxRates().GetStateTaxBrackets())
        {
            GameObject newBracket = Instantiate(bracketPrefab);
            BracketMenuHook bracketHook = newBracket.GetComponent<BracketMenuHook>();
            menuStateBrackets.Add(bracketHook);
            stateBracketContainer.AddItem(newBracket);
            bracketHook.SetScrollContainer(stateBracketContainer);
            bracketHook.SetBracketDataRefrence(brac);
            bracketHook.SetBookViewer(this);

            int limit = brac.GetLimit();
            float percent = brac.GetPercent();

            bracketHook.LoadLimitField(limit);
            bracketHook.LoadPercentField(percent);
        }

        if (menuStateBrackets.Count > 0)
        {
            SetTaxRateStatusFlag(1, Color.green);
        }
        else
        {
            SetTaxRateStatusFlag(1, Color.red);
        }
    }

    public void ClearStateBracketListInMenu()
    {
        foreach (BracketMenuHook mb in menuStateBrackets)
        {
            stateBracketContainer.RemoveItem(mb.gameObject);
            Destroy(mb.gameObject);
        }
        menuStateBrackets.Clear();
        menuStateBrackets = new List<BracketMenuHook>();
        SetTaxRateStatusFlag(1, Color.red);
    }

    public void SortStateBrackets()
    {
        book.GetTaxRates().SortStateBrackets();
        menuStateBrackets.Sort();
        stateBracketContainer.ClearContainer();
        foreach (BracketMenuHook bm in menuStateBrackets)
        {
            stateBracketContainer.AddItem(bm.gameObject);
        }
        stateBracketContainer.RepositionItems();
    }

    // Job Data Entry
    public void ClearAllJobsInMenu()
    {
        for (int i = 0; i < menuJobs.Count; i++)
        {
            JobMenuHook jmh = menuJobs[i];
            jobTabContainer.RemoveItem(jmh.gameObject);
            TabButton tab = jmh.GetTab();
            tab.LeaveGroup();
            Destroy(menuJobs[i].gameObject);
        }

        menuJobs.Clear();
        menuJobs = new List<JobMenuHook>();
    }

    public void CreatNewJob()
    {
        if (newJobNameField.text.Length > 0)
        {
            // create book data
            Job jobData = book.AddJob(newJobNameField.text);
            LoadJob(jobData);
        }
    }

    public void LoadJob(Job pJobData)
    {
        // create menu data
        GameObject go = Instantiate(jobTabPrafab, jobTabContainer.gameObject.transform);
        JobMenuHook nj = go.GetComponent<JobMenuHook>();

        // add to job tab bar
        jobTabContainer.AddItem(go);

        // assign the tab to its tab group
        TabButton tb = nj.GetTab();
        tb.SetTabGroup(jobTabContainer.gameObject.GetComponent<TabGroup>());
        tb.AddToGroup();

        // set job menu variables
        nj.SetName(pJobData.GetJobName());
        nj.SetTabTitleText(nj.GetJobName());
        nj.SetJobData(pJobData);
        menuJobs.Add(nj);
        newJobNameField.text = "";
    }

    public void SetActiveJob(Job pJobData, JobMenuHook pJobMenuHook, bool pShowPanel)
    {
        CloseJob();
        SetCurrentJob(pJobData);
        SetCurrentJobMenuHook(pJobMenuHook);
        ShowJobPanel(pShowPanel);
        UpdateHeaderName();
        UpdateHeaderNumbers();
        // load all drives
        // load all gas
        // load all income
        // load all expenses
    }

    public void ShowJobPanel(bool pOnOff)
    {
        jobPanel.SetActive(pOnOff);
    }

    public void CloseJob()
    {
        if (currentJob != null)
        {
            tabsInJobMenu.CloseAllTabs();
            ClearDriveListInMenu();
            ClearGasListInMenu();
            ClearExpenseListInMenu();
            ClearIncomeListInMenu();
            currentJob = null;
            currentJobMenuHook = null;
        }
    }

    public void DeleteJob()
    {
        if (jobDeleteField.text.Equals(DELETE_PASSWORD))
        {
            currentJobMenuHook.GetTab().LeaveGroup();
            jobTabContainer.RemoveItem(currentJobMenuHook.gameObject);
            menuJobs.Remove(currentJobMenuHook);
            Destroy(currentJobMenuHook.gameObject);
            book.RemoveJob(currentJob);
            jobDeleteField.text = "";
            ShowJobPanel(false);
        }
    }

    public void UpdateHeaderName()
    {
        jobName.text = currentJob.GetJobName();
    }

    public void UpdateHeaderNumbers()
    {
        currentJob.UpdateJob();
        exportButton.SetActive(true);

        double gross = MathExtra.RoundDownDouble(currentJob.GetGrossIncome(), 2);
        double deductions = MathExtra.RoundDownDouble(currentJob.GetTotalDeductions(), 2);
        double net = MathExtra.RoundDownDouble(currentJob.GetNetIncome(), 2);
        

        jobGross.text = gross.ToString("F2");
        jobDeductions.text = deductions.ToString("F2");
        jobNet.text = net.ToString("F2");
    }

    // Drive Data Entry

    public void AddNewDrive()
    {
        Drive driveData = currentJob.AddDrive();
        GameObject newDrive = Instantiate(drivePrefab);
        MileageMenuHook mileHook = newDrive.GetComponent<MileageMenuHook>();
        menuDriveList.Add(mileHook);
        driveContainer.AddItem(newDrive);
        mileHook.SetScrollContainer(driveContainer);
        mileHook.SetDriveDataRefrence(driveData);
        mileHook.SetBookViewer(this);
        mileHook.WriteDeductable();
        mileHook.LoadStartYearField(book.GetTaxRates().GetTaxYear());
        mileHook.LoadEndYearField(book.GetTaxRates().GetTaxYear());
        mileHook.warningStartOdometer(true);
        mileHook.warningEndOdometer(true);
        mileHook.warningMiles(true);
    }

    public void RemoveDrive(MileageMenuHook pDriveMenu)
    {
        if (menuDriveList.Count > 0)
        {
            currentJob.RemoveDrive(pDriveMenu.GetDriveDataRefrence());
            driveContainer.RemoveItem(pDriveMenu.gameObject);
            menuDriveList.Remove(pDriveMenu);
            Destroy(pDriveMenu.gameObject);
        }
    }

    public void RecordDrives()
    {
        foreach (MileageMenuHook mh in menuDriveList)
        {
            bool ded = mh.GetDeductable();
            int sMM = mh.GetStartMonth();
            int sDD = mh.GetStartDay();
            int sYY = mh.GetStartYear();
            int sHr = mh.GetStartHr();
            int sMn = mh.GetStartMin();
            int sOd = mh.GetStartOdometer();
            int eMM = mh.GetEndMonth();
            int eDD = mh.GetEndDay();
            int eYY = mh.GetEndYear();
            int eHr = mh.GetEndHr();
            int eMn = mh.GetEndMin();
            int eOd = mh.GetEndOdometer();
            float mi = mh.GetMiles();

            mh.GetDriveDataRefrence().SetAll(ded, sMM, sDD, sYY, sHr, sMn, sOd, eMM, eDD, eYY, eHr, eMn, eOd, mi);
        }
    }

    public void LoadDrivesFromData()
    {
        foreach (Drive m in currentJob.GetDriveRecords())
        {
            GameObject newDrive = Instantiate(drivePrefab);
            MileageMenuHook mileHook = newDrive.GetComponent<MileageMenuHook>();
            menuDriveList.Add(mileHook);
            driveContainer.AddItem(newDrive);
            mileHook.SetScrollContainer(driveContainer);
            mileHook.SetDriveDataRefrence(m);
            mileHook.SetBookViewer(this);

            bool ded = m.GetDeductable();
            int sMM = m.GetStartMonth();
            int sDD = m.GetStartDay();
            int sYY = m.GetStartYear();
            int sHr = m.GetStartHr();
            int sMn = m.GetStartMin();
            int sOd = m.GetStartOdometer();
            int eMM = m.GetEndMonth();
            int eDD = m.GetEndDay();
            int eYY = m.GetEndYear();
            int eHr = m.GetEndHr();
            int eMn = m.GetEndMin();
            int eOd = m.GetEndOdometer();
            float mi = m.GetMiles();

            mileHook.LoadDeductableField(ded);
            mileHook.LoadStartMonthField(sMM);
            mileHook.LoadStartDayField(sDD);
            mileHook.LoadStartYearField(sYY);
            mileHook.LoadStartHrField(sHr);
            mileHook.LoadStartMinField(sMn);
            mileHook.LoadStartOdometerField(sOd);
            mileHook.LoadEndMonthField(eMM);
            mileHook.LoadEndDayField(eDD);
            mileHook.LoadEndYearField(eYY);
            mileHook.LoadEndHrField(eHr);
            mileHook.LoadEndMinField(eMn);
            mileHook.LoadEndOdometerField(eOd);
            mileHook.LoadMiles(mi);
        }
    }

    public void ClearDriveListInMenu()
    {
        foreach (MileageMenuHook mh in menuDriveList)
        {
            driveContainer.RemoveItem(mh.gameObject);
            Destroy(mh.gameObject);
        }
        menuDriveList.Clear();
        menuDriveList = new List<MileageMenuHook>();
    }

    public void CheckDriveEntrys()
    {
        int prevOdoEnd = 0;
        foreach(MileageMenuHook md in menuDriveList)
        {
            md.CheckMiles();
            md.CheckEndOdometer();
            md.CheckStartOdometer();
            prevOdoEnd = md.GetEndOdometer();
        }
    }

    public void SortDrives()
    {
        currentJob.SortDrives();
        menuDriveList.Sort();
        driveContainer.ClearContainer();
        foreach(MileageMenuHook md in menuDriveList)
        {
            driveContainer.AddItem(md.gameObject);
        }
        driveContainer.RepositionItems();
        CheckDriveEntrys();
    }

    // Gas Data Entry

    public void AddNewGas()
    {
        Expense gasData = currentJob.AddGas();
        GameObject newGas = Instantiate(gasPrefab);
        GasMenuHook gasHook = newGas.GetComponent<GasMenuHook>();
        menuGasList.Add(gasHook);
        gasContainer.AddItem(newGas);
        gasHook.SetScrollContainer(gasContainer);
        gasHook.SetGasDataRefrence(gasData);
        gasHook.SetBookViewer(this);
        gasHook.WriteDeductable();
        gasHook.LoadYearField(book.GetTaxRates().GetTaxYear());
    }

    public void RemoveGas(GasMenuHook pGasMenu)
    {
        if (menuGasList.Count > 0)
        {
            currentJob.RemoveGas(pGasMenu.GetGasDataRefrence());
            gasContainer.RemoveItem(pGasMenu.gameObject);
            menuGasList.Remove(pGasMenu);
            Destroy(pGasMenu.gameObject);
        }
    }

    public void RecordGas()
    {
        foreach (GasMenuHook gh in menuGasList)
        {
            bool deduuctable = gh.GetDeductable();
            int month = gh.GetMonth();
            int day = gh.GetDay();
            int year = gh.GetYear();
            double cost = gh.GetCost();
            string note = gh.GetNote();

            gh.GetGasDataRefrence().SetAll(deduuctable, month, day, year, cost, note);
        }
    }

    public void LoadGasFromData()
    {
        foreach (Expense g in currentJob.GetGas())
        {
            GameObject newGas = Instantiate(gasPrefab);
            GasMenuHook gasHook = newGas.GetComponent<GasMenuHook>();
            menuGasList.Add(gasHook);
            gasContainer.AddItem(newGas);
            gasHook.SetScrollContainer(gasContainer);
            gasHook.SetGasDataRefrence(g);
            gasHook.SetBookViewer(this);

            bool deductable = g.GetDeductable();
            int month = g.GetMonth();
            int day = g.GetDay();
            int year = g.GetYear();
            double cost = g.GetCost();
            string note = g.GetNote();

            gasHook.LoadDeductableField(deductable);
            gasHook.LoadMonthField(month);
            gasHook.LoadDayField(day);
            gasHook.LoadYearField(year);
            gasHook.LoadCostField(cost);
            gasHook.LoadNoteField(note);
        }
    }

    public void ClearGasListInMenu()
    {
        foreach (GasMenuHook mg in menuGasList)
        {
            gasContainer.RemoveItem(mg.gameObject);
            Destroy(mg.gameObject);
        }
        menuGasList.Clear();
        menuGasList = new List<GasMenuHook>();
    }

    public void SortGas()
    {
        currentJob.SortGas();
        menuGasList.Sort();
        gasContainer.ClearContainer();
        foreach (GasMenuHook mg in menuGasList)
        {
            gasContainer.AddItem(mg.gameObject);
        }
        gasContainer.RepositionItems();
    }

    // Expense Data Entry
    
    public void AddNewExpense()
    {
        Expense ExpenseData = currentJob.AddExpense();
        GameObject newExpense = Instantiate(expensePrefab);
        ExpenseMenuHook expenseHook = newExpense.GetComponent<ExpenseMenuHook>();
        menuExpenseList.Add(expenseHook);
        expenseContainer.AddItem(newExpense);
        expenseHook.SetScrollContainer(expenseContainer);
        expenseHook.SetExpenseDataRefrence(ExpenseData);
        expenseHook.SetBookViewer(this);
        expenseHook.WriteDeductable();
        expenseHook.LoadYearField(book.GetTaxRates().GetTaxYear());
    }

    public void RemoveExpense(ExpenseMenuHook pExpenseMenu)
    {
        if (menuExpenseList.Count > 0)
        {
            currentJob.RemoveExpense(pExpenseMenu.GetExpenseDataRefrence());
            expenseContainer.RemoveItem(pExpenseMenu.gameObject);
            menuExpenseList.Remove(pExpenseMenu);
            Destroy(pExpenseMenu.gameObject);
        }
    }

    public void RecordExpense()
    {
        foreach (ExpenseMenuHook eh in menuExpenseList)
        {
            bool deduuctable = eh.GetDeductable();
            int month = eh.GetMonth();
            int day = eh.GetDay();
            int year = eh.GetYear();
            double cost = eh.GetCost();
            string note = eh.GetNote();

            eh.GetExpenseDataRefrence().SetAll(deduuctable, month, day, year, cost, note);
        }
    }

    public void LoadExpenseFromData()
    {
        foreach (Expense e in currentJob.GetExpenseList())
        {
            GameObject newExpense = Instantiate(expensePrefab);
            ExpenseMenuHook expenseHook = newExpense.GetComponent<ExpenseMenuHook>();
            menuExpenseList.Add(expenseHook);
            expenseContainer.AddItem(newExpense);
            expenseHook.SetScrollContainer(expenseContainer);
            expenseHook.SetExpenseDataRefrence(e);
            expenseHook.SetBookViewer(this);

            bool deductable = e.GetDeductable();
            int month = e.GetMonth();
            int day = e.GetDay();
            int year = e.GetYear();
            double cost = e.GetCost();
            string note = e.GetNote();

            expenseHook.LoadDeductableField(deductable);
            expenseHook.LoadMonthField(month);
            expenseHook.LoadDayField(day);
            expenseHook.LoadYearField(year);
            expenseHook.LoadCostField(cost);
            expenseHook.LoadNoteField(note);
        }
    }

    public void ClearExpenseListInMenu()
    {
        foreach (ExpenseMenuHook me in menuExpenseList)
        {
            expenseContainer.RemoveItem(me.gameObject);
            Destroy(me.gameObject);
        }
        menuExpenseList.Clear();
        menuExpenseList = new List<ExpenseMenuHook>();
    }

    public void SortExpenses()
    {
        currentJob.SortExpenseList();
        menuExpenseList.Sort();
        expenseContainer.ClearContainer();
        foreach (ExpenseMenuHook me in menuExpenseList)
        {
            expenseContainer.AddItem(me.gameObject);
        }
        expenseContainer.RepositionItems();
    }

    // Income Data Entry

    public void AddNewIncome()
    {
        Income incomeData = currentJob.AddIncome();
        GameObject newIncome = Instantiate(incomePrefab);
        IncomeMenuHook incomeHook = newIncome.GetComponent<IncomeMenuHook>();
        menuIncomeList.Add(incomeHook);
        incomeContainer.AddItem(newIncome);
        incomeHook.SetScrollContainer(incomeContainer);
        incomeHook.SetIncomeDataRefrence(incomeData);
        incomeHook.SetBookViewer(this);
        incomeHook.LoadYearField(book.GetTaxRates().GetTaxYear());
    }

    public void RemoveIncome(IncomeMenuHook pIncomeMenu)
    {
        if (menuIncomeList.Count > 0)
        {
            currentJob.RemoveIncome(pIncomeMenu.GetIncomeDataRefrence());
            incomeContainer.RemoveItem(pIncomeMenu.gameObject);
            menuIncomeList.Remove(pIncomeMenu);
            Destroy(pIncomeMenu.gameObject);
        }
    }

    public void RecordIncome()
    {
        foreach (IncomeMenuHook ih in menuIncomeList)
        {
            int month = ih.GetMonth();
            int day = ih.GetDay();
            int year = ih.GetYear();
            double amount = ih.GetAmount();
            string source = ih.GetSource();

            ih.GetIncomeDataRefrence().SetAll(month, day, year, amount, source);
        }
    }

    public void LoadIncomeFromData()
    {
        foreach (Income inco in currentJob.GetIncomes())
        {
            GameObject newIncome = Instantiate(incomePrefab);
            IncomeMenuHook incomeHook = newIncome.GetComponent<IncomeMenuHook>();
            menuIncomeList.Add(incomeHook);
            incomeContainer.AddItem(newIncome);
            incomeHook.SetScrollContainer(incomeContainer);
            incomeHook.SetIncomeDataRefrence(inco);
            incomeHook.SetBookViewer(this);

            int month = inco.GetMonth();
            int day = inco.GetDay();
            int year = inco.GetYear();
            double amount = inco.GetAmount();
            string source = inco.GetSource();

            incomeHook.LoadMonthField(month);
            incomeHook.LoadDayField(day);
            incomeHook.LoadYearField(year);
            incomeHook.LoadAmountField(amount);
            incomeHook.LoadSourceField(source);
        }
    }

    public void ClearIncomeListInMenu()
    {
        foreach (IncomeMenuHook mi in menuIncomeList)
        {
            incomeContainer.RemoveItem(mi.gameObject);
            Destroy(mi.gameObject);
        }
        menuIncomeList.Clear();
        menuIncomeList = new List<IncomeMenuHook>();
    }

    public void SortIncome()
    {
        currentJob.SortIncome();
        menuIncomeList.Sort();
        incomeContainer.ClearContainer();
        foreach (IncomeMenuHook mi in menuIncomeList)
        {
            incomeContainer.AddItem(mi.gameObject);
        }
        incomeContainer.RepositionItems();
    }
}
