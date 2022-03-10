//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    private const string END_OF_LIST_LINE = "++n++";
    private const string END_OF_SUBLIST_LINE = "++s++";
    private const string TRUE_LINE = "true";
    private const string FALSE_LINE = "false";
    private const string EXPORT_SEPERATOR = "======================================================================================================================================================================================================================";

    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private CurrentSave currentSave;
    [SerializeField] private BookViewer bookViewer;
    [SerializeField] private TaxRates rates;
    [SerializeField] private List<Job> jobs;
    [SerializeField] private YearlyTotals yearlyTotals;
    [SerializeField] private GameObject jobPrefab;
    [SerializeField] private GameObject jobHolder;

    public TaxRates GetTaxRates() { return rates; }
    public List<Job> GetJobs() { return jobs; }
    public YearlyTotals GetYearlyTotals() { return yearlyTotals; }

    public void SaveBook()
    {
        yearlyTotals.UpdateYearlyTotals();

        List<string> newData = new List<string>();
        newData.Add("" + yearlyTotals.GetEmployeeIncome());
        newData.Add("" + yearlyTotals.GetPaidTaxes()[1]);
        newData.Add("" + yearlyTotals.GetPaidTaxes()[2]);
        newData.Add("" + yearlyTotals.GetPaidTaxes()[3]);
        newData.Add("" + yearlyTotals.GetPaidTaxes()[4]);
        newData.Add("" + yearlyTotals.GetPaidTithe());
        newData.Add("" + yearlyTotals.GetPaidGas());
        newData.Add("" + rates.GetMileageRate());
        newData.Add("" + rates.GetSoSecTaxeRate());
        newData.Add("" + rates.GetMedicareTaxRate());
        newData.Add("" + rates.GetTaxYear());
        
        // fed tax brackets
        foreach(Bracket b in rates.GetFedTaxBrackets())
        {
            newData.Add("" + b.GetLimit());
            newData.Add("" + b.GetPercent());
        }
        newData.Add(END_OF_LIST_LINE);

        // state tax Brackets
        foreach (Bracket b in rates.GetStateTaxBrackets())
        {
            newData.Add("" + b.GetLimit());
            newData.Add("" + b.GetPercent());
        }
        newData.Add(END_OF_LIST_LINE);

        // jobs 
        foreach (Job j in jobs)
        {
            newData.Add("" + j.GetJobName());

            // drives
            foreach (Drive d in j.GetDriveRecords())
            {
                if (d.GetDeductable() == true)
                {
                    newData.Add(TRUE_LINE);
                }
                else
                {
                    newData.Add(FALSE_LINE);
                }
                newData.Add("" + d.GetStartMonth());
                newData.Add("" + d.GetStartDay());
                newData.Add("" + d.GetStartYear());
                newData.Add("" + d.GetStartHr());
                newData.Add("" + d.GetStartMin());
                newData.Add("" + d.GetStartOdometer());
                newData.Add("" + d.GetEndMonth());
                newData.Add("" + d.GetEndDay());
                newData.Add("" + d.GetEndYear());
                newData.Add("" + d.GetEndHr());
                newData.Add("" + d.GetEndMin());
                newData.Add("" + d.GetEndOdometer());
                newData.Add("" + d.GetMiles());
            }
            newData.Add(END_OF_SUBLIST_LINE);

            // Gas expenses
            foreach (Expense e in j.GetGas())
            {
                if (e.GetDeductable() == true)
                {
                    newData.Add(TRUE_LINE);
                }
                else
                {
                    newData.Add(FALSE_LINE);
                }
                newData.Add("" + e.GetMonth());
                newData.Add("" + e.GetDay());
                newData.Add("" + e.GetYear());
                newData.Add("" + e.GetCost());
                newData.Add(e.GetNote());
            }
            newData.Add(END_OF_SUBLIST_LINE);

            // other expenses
            foreach (Expense e in j.GetExpenseList())
            {
                if (e.GetDeductable() == true)
                {
                    newData.Add(TRUE_LINE);
                }
                else
                {
                    newData.Add(FALSE_LINE);
                }
                newData.Add("" + e.GetMonth());
                newData.Add("" + e.GetDay());
                newData.Add("" + e.GetYear());
                newData.Add("" + e.GetCost());
                newData.Add(e.GetNote());
            }
            newData.Add(END_OF_SUBLIST_LINE);

            // income
            foreach (Income inc in j.GetIncomes())
            {
                newData.Add("" + inc.GetMonth());
                newData.Add("" + inc.GetDay());
                newData.Add("" + inc.GetYear());
                newData.Add("" + inc.GetAmount());
                newData.Add(inc.GetSource());
            }
            newData.Add(END_OF_SUBLIST_LINE);
        }
        newData.Add(END_OF_LIST_LINE);

        currentSave.SetData(newData);
        currentSave.Save();
    }

    public void CloseBook()
    {
        currentSave.CloseSave();
        rates.ClearAllTaxRates();
        ClearJobs();
        yearlyTotals.ClearAll();
    }

    public void OpenBook(string pBookName)
    {
        currentSave.OpenSave(pBookName);
        
        //Load all info and build book
        List<string> data = currentSave.GetData();
        if (data.Count > 0)
        {
            int lineNumber = 0;

            // income and savings totals
            yearlyTotals.SetEmployeeIncome(double.Parse(data[lineNumber]));
            lineNumber++;
            yearlyTotals.SetPaidTaxes(int.Parse(data[lineNumber]), 1);
            lineNumber++;
            yearlyTotals.SetPaidTaxes(int.Parse(data[lineNumber]), 2);
            lineNumber++;
            yearlyTotals.SetPaidTaxes(int.Parse(data[lineNumber]), 3);
            lineNumber++;
            yearlyTotals.SetPaidTaxes(int.Parse(data[lineNumber]), 4);
            lineNumber++;
            yearlyTotals.SetPaidTithe(int.Parse(data[lineNumber]));
            lineNumber++;
            yearlyTotals.SetPaidGas(double.Parse(data[lineNumber]));
            lineNumber++;

            // tax and mileage rates
            rates.SetMileageRate(float.Parse(data[lineNumber]));
            lineNumber++;
            rates.SetSoSecTaxeRate(float.Parse(data[lineNumber]));
            lineNumber++;
            rates.SetMedicareTaxRate(float.Parse(data[lineNumber]));
            lineNumber++;
            rates.SetTaxYear(int.Parse(data[lineNumber]));
            lineNumber++;

            //federal tax brackets
            bool done = false;
            while (!done)
            {
                if (data[lineNumber].Equals(END_OF_LIST_LINE))
                {
                    done = true;
                    lineNumber++;
                }
                else
                {
                    Bracket nb = rates.AddFedBracket();

                    int limit = int.Parse(data[lineNumber]);
                    lineNumber++;
                    float percent = float.Parse(data[lineNumber]);
                    lineNumber++;

                    nb.SetAll(limit, percent);
                }
            }

            //state tax brackets
            done = false;
            while (!done)
            {
                if (data[lineNumber].Equals(END_OF_LIST_LINE))
                {
                    done = true;
                    lineNumber++;
                }
                else
                {
                    Bracket nb = rates.AddStateBracket();

                    int limit = int.Parse(data[lineNumber]);
                    lineNumber++;
                    float percent = float.Parse(data[lineNumber]);
                    lineNumber++;

                    nb.SetAll(limit, percent);
                }
            }
            //rates.SetStateTaxBrackets(brackets);

            // jobs
            done = false;
            //int jobNumber = 0;
            while (!done)
            {
                if (data[lineNumber].Equals(END_OF_LIST_LINE))
                {
                    done = true;
                    lineNumber++;
                }
                else
                {
                    Job newJob = AddJob(data[lineNumber]);
                    lineNumber++;

                    // drives
                    bool done2 = false;
                    while (!done2)
                    {
                        if (data[lineNumber].Equals(END_OF_SUBLIST_LINE))
                        {
                            done2 = true;
                            lineNumber++;
                        }
                        else
                        {
                            bool de = false;
                            if (data[lineNumber].Equals(TRUE_LINE)) de = true;
                            lineNumber++;
                            int sm = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int sd = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int sy = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int shr = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int smi = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int so = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int em = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int ed = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int ey = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int ehr = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int emi = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int eo = int.Parse(data[lineNumber]);
                            lineNumber++;
                            float mi = float.Parse(data[lineNumber]);
                            lineNumber++;
                            Drive newDrive = newJob.AddDrive();
                            newDrive.SetAll(de, sm, sd, sy, shr, smi, so, em, ed, ey, ehr, emi, eo, mi);
                        }
                    }

                    // Gas
                    done2 = false;
                    while (!done2)
                    {
                        if (data[lineNumber].Equals(END_OF_SUBLIST_LINE))
                        {
                            done2 = true;
                            lineNumber++;
                        }
                        else
                        {
                            bool deductable = false;
                            if (data[lineNumber].Equals(TRUE_LINE)) deductable = true;
                            lineNumber++;
                            int month = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int day = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int year = int.Parse(data[lineNumber]);
                            lineNumber++;
                            double cost = double.Parse(data[lineNumber]);
                            lineNumber++;
                            string note = data[lineNumber];
                            lineNumber++;

                            Expense newGas = newJob.AddGas();
                            newGas.SetAll(deductable, month, day, year, cost, note);
                        }
                    }

                    // Expense
                    done2 = false;
                    while (!done2)
                    {
                        if (data[lineNumber].Equals(END_OF_SUBLIST_LINE))
                        {
                            done2 = true;
                            lineNumber++;
                        }
                        else
                        {
                            bool deductable = false;
                            if (data[lineNumber].Equals(TRUE_LINE)) deductable = true;
                            lineNumber++;
                            int month = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int day = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int year = int.Parse(data[lineNumber]);
                            lineNumber++;
                            double cost = double.Parse(data[lineNumber]);
                            lineNumber++;
                            string note = data[lineNumber];
                            lineNumber++;

                            Expense newExpense = newJob.AddExpense();
                            newExpense.SetAll(deductable, month, day, year, cost, note);
                        }
                    }

                    // Income
                    done2 = false;
                    while (!done2)
                    {
                        if (data[lineNumber].Equals(END_OF_SUBLIST_LINE))
                        {
                            done2 = true;
                            lineNumber++;
                        }
                        else
                        {
                            int month = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int day = int.Parse(data[lineNumber]);
                            lineNumber++;
                            int year = int.Parse(data[lineNumber]);
                            lineNumber++;
                            double amount = double.Parse(data[lineNumber]);
                            lineNumber++;
                            string source = data[lineNumber];
                            lineNumber++;

                            Income newIncome = newJob.AddIncome();
                            newIncome.SetAll(month, day, year, amount, source);
                        }
                    }
                }
            }
        }

        bookViewer.OpenBookViewer();
        currentSave.SaveBackup();
    }

    public void DeleteBook()
    {
        currentSave.DeleteOpenSave();
        bookViewer.CloseBookViewer();
    }

    public void ExportBook()
    {
        List<string> export = ExportableTotalsList();
        export.Add(EXPORT_SEPERATOR);
        export.Add(EXPORT_SEPERATOR);
        export.Add(EXPORT_SEPERATOR);
        foreach (Job j in jobs)
        {
            List<string> income = ExportableIncomeList(j);
            foreach (string inco in income)
            {
                export.Add(inco);
            }
            export.Add(EXPORT_SEPERATOR);
            export.Add(EXPORT_SEPERATOR);
            export.Add(EXPORT_SEPERATOR);
            List<string> drives = ExportableDrivesList(j);
            foreach (string s in drives)
            {
                export.Add(s);
            }
            export.Add(EXPORT_SEPERATOR);
            export.Add(EXPORT_SEPERATOR);
            export.Add(EXPORT_SEPERATOR);
            List<string> expenses = ExportableExpensesList(j);
            foreach (string e in expenses)
            {
                export.Add(e);
            }
            export.Add(EXPORT_SEPERATOR);
            export.Add(EXPORT_SEPERATOR);
            export.Add(EXPORT_SEPERATOR);
            List<string> gasExpenses = ExportableDeductableGasExpenseList(j);
            foreach (string g in gasExpenses)
            {
                export.Add(g);
            }
            export.Add(EXPORT_SEPERATOR);
            export.Add(EXPORT_SEPERATOR);
            export.Add(EXPORT_SEPERATOR);
        }
        currentSave.Export(export);
    }

    public Job AddJob(string pName)
    {
        GameObject go = Instantiate(jobPrefab, jobHolder.transform);
        Job nj = go.GetComponent<Job>();
        nj.SetName(pName);
        nj.SetMileageRate(rates.GetMileageRate());
        jobs.Add(nj);
        return nj;
    }

    public void RemoveJob(Job pJob)
    {
        jobs.Remove(pJob);
        Destroy(pJob.gameObject);
    }

    public void ClearJobs()
    {
        foreach (Job j in jobs)
        {
            Destroy(j.gameObject);
        }
        jobs.Clear();
        jobs = new List<Job>();
    }

    private List<string> ExportableDrivesList(Job pJob)
    {
        List<string> export = new List<string>();
        export.Add(pJob.GetJobName() + ": Drive Log");
        float mileageRate = pJob.GetMileageRate();
        foreach (Drive d in pJob.GetDriveRecords())
        {
            string startDate = "" + d.GetStartMonth() + "/" + d.GetStartDay() + "/" + d.GetStartYear();
            string endDate = "" + d.GetStartMonth() + "/" + d.GetStartDay() + "/" + d.GetStartYear();
            if (startDate.Length < 10)
            {
                startDate = MyExtra.ExtendString(true, startDate, 10, ' ');
            }
            if (endDate.Length < 10)
            {
                endDate = MyExtra.ExtendString(true, endDate, 10, ' ');
            }
            int dStartHr = d.GetStartHr();
            int dStartMn = d.GetStartMin();
            int dEndHr = d.GetEndHr();
            int dEndMn = d.GetEndMin();
            string stratTime = MyExtra.TwentyFourHrToTwelveHr(false, true, true, dStartHr, dStartMn);
            string endTime = MyExtra.TwentyFourHrToTwelveHr(false, true, true, dEndHr, dEndMn);
            string startOdo = "" + d.GetStartOdometer();
            string endOdo = "" + d.GetEndOdometer();
            if (startOdo.Length < 7)
            {
                startOdo = MyExtra.ExtendString(true, startOdo, 7, ' ');
            }
            if (endOdo.Length < 7)
            {
                endOdo = MyExtra.ExtendString(true, endOdo, 7, ' ');
            }
            string miles = "" + d.GetMiles();
            if (!miles.Contains("."))
            {
                miles = miles + ".0";
            }
            if (miles.Length < 7)
            {
                miles = MyExtra.ExtendString(true, miles, 7, ' ');
            }
            string deduction = "";
            bool ded = d.GetDeductable();
            double dedDollars = (mileageRate * 0.01) * d.GetMiles();
            dedDollars = MathExtra.RoundDownDouble(dedDollars, 2);

            string deductible = "";
            if (ded)
            {
                deductible = "True ";
            }
            else
            {
                deductible = "False";
            }

            if (ded)
            {
                deduction = "" + dedDollars;
                if (deduction.Contains("."))
                {
                    if (deduction.IndexOf('.') == deduction.Length - 2)
                    {
                        deduction = deduction + "0";
                    }
                }
                else
                {
                    deduction = deduction + ".00";
                }
            }
            else
            {
                deduction = "0.00";
            }
            if (deduction.Length < 8)
            {
                deduction = MyExtra.ExtendString(true, deduction, 8, ' ');
            }
            export.Add("-------------------------||-----------------------||-----------------------||---------------------||------------------------||----------------------||---------------||--------------------||-----------------------||");
            export.Add(" Start Date = " + startDate + " || End Date = " + endDate + " || Start Time = " + stratTime + " || End Time = " + endTime + " || Start Odometer " + startOdo + " || End Odometer " + endOdo + " || Miles " + miles + " || Deductible = " + deductible + " || Deduction = $" + deduction + " ||");
        }
        return export;
    }

    private List<string> ExportableExpensesList(Job pJob)
    {
        List<string> export = new List<string>();
        export.Add(pJob.GetJobName() + ": Business Expenses");
        foreach (Expense e in pJob.GetExpenseList())
        {
            string date = "" + e.GetMonth() + "/" + e.GetDay() + "/" + e.GetYear();
            if (date.Length < 10)
            {
                date = MyExtra.ExtendString(true, date, 10, ' ');
            }
            string cost = "" + e.GetCost();
            if (cost.Contains("."))
            {
                if (cost.IndexOf('.') == cost.Length - 2)
                {
                    cost = cost + "0";
                }
            }
            else
            {
                cost = cost + ".00";
            }
            if (cost.Length < 9)
            {
                cost = MyExtra.ExtendString(true, cost, 9, ' ');
            }
            string deductible = "";
            bool ded = e.GetDeductable();
            if (ded)
            {
                deductible = "True ";
            }
            else
            {
                deductible = "False";
            }
            string note = e.GetNote();
            export.Add("-------------------||-------------------||--------------------||--------------------------");
            export.Add(" Date = " + date + " || Cost = $" + cost + " || Deductible = " + deductible + " || Note: " + note);
        }
        return export;
    }

    private List<string> ExportableDeductableGasExpenseList(Job pJob)
    {
        List<string> export = new List<string>();
        export.Add(pJob.GetJobName() + ": Deductible Gas Expenses");
        foreach (Expense e in pJob.GetGas())
        {
            if (e.GetDeductable())
            {
                string date = "" + e.GetMonth() + "/" + e.GetDay() + "/" + e.GetYear();
                if (date.Length < 10)
                {
                    date = MyExtra.ExtendString(true, date, 10, ' ');
                }
                string cost = "" + e.GetCost();
                if (cost.Contains("."))
                {
                    if (cost.IndexOf('.') == cost.Length - 2)
                    {
                        cost = cost + "0";
                    }
                }
                else
                {
                    cost = cost + ".00";
                }
                if (cost.Length < 9)
                {
                    cost = MyExtra.ExtendString(true, cost, 9, ' ');
                }
                string note = e.GetNote();
                export.Add("-------------------||-------------------||--------------------------");
                export.Add(" Date = " + date + " || Cost = $" + cost + " || Note: " + note);
            }
        }
        return export;
    }

    private List<string> ExportableIncomeList(Job pJob)
    {
        List<string> export = new List<string>();
        export.Add(pJob.GetJobName() + ": Income");
        foreach (Income inco in pJob.GetIncomes())
        {
            string date = "" + inco.GetMonth() + "/" + inco.GetDay() + "/" + inco.GetYear();
            if (date.Length < 10)
            {
                date = MyExtra.ExtendString(true, date, 10, ' ');
            }
            string pay = "" + inco.GetAmount();
            if (pay.Contains("."))
            {
                if (pay.IndexOf('.') == pay.Length - 2)
                {
                    pay = pay + "0";
                }
            }
            else
            {
                pay = pay + ".00";
            }
            if (pay.Length < 9)
            {
                pay = MyExtra.ExtendString(true, pay, 9, ' ');
            }
            string source = inco.GetSource();
            export.Add("-------------------||------------------||----------------------");
            export.Add(" Date = " + date + " || Pay = $" + pay + " || Source: " + source);
        }
        return export;
    }

    private List<string> ExportableTotalsList()
    {
        List<string> export = new List<string>();
        export.Add(currentSave.GetFileName() + ": Yearly Totals and Taxes");
        string empIncome = "" + yearlyTotals.GetEmployeeIncome();
        if (empIncome.Contains("."))
        {
            if (empIncome.IndexOf('.') == empIncome.Length - 2)
            {
                empIncome = empIncome + "0";
            }
        }
        else
        {
            empIncome = empIncome + ".00";
        }
        if (empIncome.Length < 10)
        {
            empIncome = MyExtra.ExtendString(true, empIncome, 10, ' ');
        }
        string nonEmpIncome = "" + yearlyTotals.GetJobIncome()[0];
        if (nonEmpIncome.Contains("."))
        {
            if (nonEmpIncome.IndexOf('.') == nonEmpIncome.Length - 2)
            {
                nonEmpIncome = nonEmpIncome + "0";
            }
        }
        else
        {
            nonEmpIncome = nonEmpIncome + ".00";
        }
        if (nonEmpIncome.Length < 10)
        {
            nonEmpIncome = MyExtra.ExtendString(true, nonEmpIncome, 10, ' ');
        }
        string fedTax = "" + yearlyTotals.GetFedTax()[0];
        if (fedTax.Length < 7)
        {
            fedTax = MyExtra.ExtendString(true, fedTax, 7, ' ');
        }
        string stateTax = "" + yearlyTotals.GetStateTax()[0];
        if (stateTax.Length < 7)
        {
            stateTax = MyExtra.ExtendString(true, stateTax, 7, ' ');
        }
        export.Add("------------------------------||----------------------------------||-----------------------||---------------------||");
        export.Add(" Employee Income = " + empIncome + " || Non-Employee Income = " + nonEmpIncome + " || Federal Tax = " + fedTax + " || State Tax = " + stateTax + " ||");
        return export;
    }
}
