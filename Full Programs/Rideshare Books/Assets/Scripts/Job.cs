//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System.Collections.Generic;
using UnityEngine;

public class Job : MonoBehaviour
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private GameObject drivePrefab;
    [SerializeField] private GameObject gasExpencePrefab;
    [SerializeField] private GameObject otherExpencePrefab;
    [SerializeField] private GameObject incomePrefab;
    [SerializeField] private GameObject driveHolder;
    [SerializeField] private GameObject gasExpenceHolder;
    [SerializeField] private GameObject otherExpenceHolder;
    [SerializeField] private GameObject incomeHolder;

    [SerializeField] private string jobName;
    [SerializeField] private List<Drive> drives = new List<Drive>();
    [SerializeField] private List<Expense> gasExpences = new List<Expense>();
    [SerializeField] private List<Expense> otherExpences = new List<Expense>();
    [SerializeField] private List<Income> incomes = new List<Income>();

    [SerializeField] private float mileageRate;

    [SerializeField] private double totalDeductions;
    [SerializeField] private double netIncome;
    [SerializeField] private double grossIncome;
    [SerializeField] private double[] quarterlyMileageDeductions = new double[5];
    [SerializeField] private double[] quarterlyDeductions = new double [5];
    [SerializeField] private double[] quarterlyNetIncome = new double[5];
    [SerializeField] private double[] quarterlyGrossIncome = new double[5];
    [SerializeField] private double totalGasExpences;
    [SerializeField] private double totalOtherExpences;
    [SerializeField] private double totalMiles;
    [SerializeField] private double totalMileageDedution;

    // getters
    public string GetJobName() { return jobName; }
    public List<Drive> GetDriveRecords() { return drives; }
    public List<Expense> GetGas() { return gasExpences; }
    public List<Expense> GetExpenseList() { return otherExpences; }
    public List<Income> GetIncomes() { return incomes; }
    public double GetTotalDeductions() { return totalDeductions; }
    public double GetNetIncome() { return netIncome; }
    public double GetGrossIncome() { return grossIncome; }
    public double GetTotalGasExpences() { return totalGasExpences; }
    public double GetTotalOtherExpences() { return totalOtherExpences; }
    public double GetTotalMiles() { return totalMiles; }
    public double GetTotalMileageDedution() { return totalMileageDedution; }
    public double[] GetQuarterlyNetIncome() { return quarterlyNetIncome; }
    public float GetMileageRate() { return mileageRate; }

    // setters
    public void SetName(string pName) { jobName = pName; }
    public void SetMileageRate(float pRate) { mileageRate = pRate; }

    public Drive AddDrive()
    {
        GameObject go = Instantiate(drivePrefab, driveHolder.transform);
        Drive newDrive = go.GetComponent<Drive>();
        drives.Add(newDrive);
        go.name = go.name + drives.IndexOf(newDrive);
        return newDrive;
    }

    public Expense AddGas()
    {
        GameObject go = Instantiate(gasExpencePrefab, gasExpenceHolder.transform);
        Expense newGas = go.GetComponent<Expense>();
        gasExpences.Add(newGas);
        go.name = go.name + gasExpences.IndexOf(newGas);
        return newGas;
    }

    public Expense AddExpense()
    {
        GameObject go = Instantiate(otherExpencePrefab, otherExpenceHolder.transform);
        Expense newExpence = go.GetComponent<Expense>();
        otherExpences.Add(newExpence);
        go.name = go.name + otherExpences.IndexOf(newExpence);
        return newExpence;
    }

    public Income AddIncome()
    {
        GameObject go = Instantiate(incomePrefab, incomeHolder.transform);
        Income newIncome = go.GetComponent<Income>();
        incomes.Add(newIncome);
        go.name = go.name + incomes.IndexOf(newIncome);
        return newIncome;
    }

    public void ClearDrives()
    {
        for (int i = drives.Count - 1; i >= 0; i--)
        {
            Destroy(drives[i].gameObject);
        }
        drives.Clear();
        drives = new List<Drive>();
    }

    public void ClearGasExpences()
    {
        for (int i = gasExpences.Count - 1; i >= 0; i--)
        {
            Destroy(gasExpences[i].gameObject);
        }
        gasExpences.Clear();
        gasExpences = new List<Expense>();
    }

    public void ClearOtherExpences()
    {
        for (int i = otherExpences.Count - 1; i >= 0; i--)
        {
            Destroy(otherExpences[i].gameObject);
        }
        otherExpences.Clear();
        otherExpences = new List<Expense>();
    }

    public void ClearIncome()
    {
        for (int i = incomes.Count - 1; i >= 0; i--)
        {
            Destroy(incomes[i].gameObject);
        }
        incomes.Clear();
        incomes = new List<Income>();
    }

    public void RemoveDrive(Drive pDrive)
    {
        drives.Remove(pDrive);
        Destroy(pDrive.gameObject);
    }

    public void RemoveGas(Expense pGas)
    {
        gasExpences.Remove(pGas);
        Destroy(pGas.gameObject);
    }

    public void RemoveExpense(Expense pExpence)
    {
        otherExpences.Remove(pExpence);
        Destroy(pExpence.gameObject);
    }

    public void RemoveIncome(Income pIncome)
    {
        incomes.Remove(pIncome);
        Destroy(pIncome.gameObject);
    }

    public void SortDrives()
    {
        drives.Sort();
    }

    public void SortGas()
    {
        gasExpences.Sort();
    }

    public void SortExpenseList()
    {
        otherExpences.Sort();
    }

    public void SortIncome()
    {
        incomes.Sort();
    }

    public void UpdateJob()
    {
        netIncome = 0.0;
        totalDeductions = 0.0;
        totalGasExpences = 0.0;
        totalOtherExpences = 0.0;
        grossIncome = 0.0;
        totalMiles = 0.0;
        totalMileageDedution = 0.0;
        for (int i = 0; i < quarterlyMileageDeductions.Length; i++)
        {
            quarterlyMileageDeductions[i] = 0.0;
        }
        for (int i = 0; i < quarterlyDeductions.Length; i++)
        {
            quarterlyDeductions[i] = 0.0;
        }
        for (int i = 0; i < quarterlyNetIncome.Length; i++)
        {
            quarterlyNetIncome[i] = 0.0;
        }
        for (int i = 0; i < quarterlyGrossIncome.Length; i++)
        {
            quarterlyGrossIncome[i] = 0.0;
        }
        foreach (Income i in incomes)
        {
            double inco = i.GetAmount();
            grossIncome += inco;

            if (i.GetMonth() < 4)
            {
                quarterlyGrossIncome[1] += inco;
            }
            else if (i.GetMonth() < 6)
            {
                quarterlyGrossIncome[2] += inco;
            }
            else if (i.GetMonth() < 9)
            {
                quarterlyGrossIncome[3] += inco;
            }
            else
            {
                quarterlyGrossIncome[4] += inco;
            }
        }
        foreach (Drive d in drives)
        {
            totalMiles += d.GetMiles();
            if (d.GetDeductable())
            {
                double mileage = (mileageRate * 0.01) * d.GetMiles();
                totalDeductions += mileage;
                totalMileageDedution += mileage;

                if (d.GetEndMonth() < 4)
                {
                    quarterlyDeductions[1] += mileage;
                    quarterlyMileageDeductions[1] += mileage;
                }
                else if (d.GetEndMonth() < 6)
                {
                    quarterlyDeductions[2] += mileage;
                    quarterlyMileageDeductions[2] += mileage;
                }
                else if (d.GetEndMonth() < 9)
                {
                    quarterlyDeductions[3] += mileage;
                    quarterlyMileageDeductions[3] += mileage;
                }
                else
                {
                    quarterlyDeductions[4] += mileage;
                    quarterlyMileageDeductions[4] += mileage;
                }
            }
        }
        foreach (Expense e in gasExpences)
        {
            double ded = e.GetCost();
            totalGasExpences += ded;
            if (e.GetDeductable())
            {
                totalDeductions += ded;

                if (e.GetMonth() < 4)
                {
                    quarterlyDeductions[1] += ded;
                }
                else if (e.GetMonth() < 6)
                {
                    quarterlyDeductions[2] += ded;
                }
                else if (e.GetMonth() < 9)
                {
                    quarterlyDeductions[3] += ded;
                }
                else
                {
                    quarterlyDeductions[4] += ded;
                }
            }
        }
        foreach (Expense e in otherExpences)
        {
            double ded = e.GetCost();
            totalOtherExpences += ded;
            if (e.GetDeductable())
            {
                totalDeductions += ded;

                if (e.GetMonth() < 4)
                {
                    quarterlyDeductions[1] += ded;
                }
                else if (e.GetMonth() < 6)
                {
                    quarterlyDeductions[2] += ded;
                }
                else if (e.GetMonth() < 9)
                {
                    quarterlyDeductions[3] += ded;
                }
                else
                {
                    quarterlyDeductions[4] += ded;
                }
            }
        }
        netIncome = MathExtra.RoundUpDouble(grossIncome, 2) - MathExtra.RoundDownDouble(totalDeductions, 2);
        for (int i = 1; i < quarterlyNetIncome.Length; i++)
        {
            quarterlyNetIncome[i] = MathExtra.RoundUpDouble(quarterlyGrossIncome[i], 2) - MathExtra.RoundDownDouble(quarterlyDeductions[i], 2);
        }
    }
}
