//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.
 
using System.Collections.Generic;
using UnityEngine;
using System;

//Calculates yearly and quarterly tax, tithe and gas payments due
public class YearlyTotals : MonoBehaviour
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private Book book;
    [SerializeField] private double employeeIncome;
    [SerializeField] private double paidGas;
    [SerializeField] private int paidTithe;
    [SerializeField] private double[] jobIncome = new double[5]; // 0 is total
    [SerializeField] private int[] fedTax = new int[5];          // 0 is total
    [SerializeField] private int[] stateTax = new int[5];        // 0 is total
    [SerializeField] private int[] paidTaxes = new int[5];       // 0 is total
    [SerializeField] private List<double> fedTaxBracketRemaining;
    [SerializeField] private List<double> stateTaxBracketRemaining;
    [SerializeField] private int currentFedBracket;
    [SerializeField] private int currentStateBracket;

    public double GetEmployeeIncome() { return employeeIncome; }
    public double[] GetJobIncome() { return jobIncome; }
    public int[] GetFedTax() { return fedTax; }
    public int[] GetStateTax() { return stateTax; }
    public int[] GetPaidTaxes() { return paidTaxes; }
    public double GetPaidGas() { return paidGas; }
    public int GetPaidTithe() { return paidTithe; }

    public void SetEmployeeIncome(double pAmount) { employeeIncome = pAmount; }
    public void SetFedTax(int pAmount, int pQuarter) { fedTax[pQuarter] = pAmount; }
    public void SetStateTax(int pAmount, int pQuarter) { stateTax[pQuarter] = pAmount; }
    public void SetPaidTaxes(int pAmount, int pQuarter) { paidTaxes[pQuarter] = pAmount; }
    public void SetPaidGas(double pAmount) { paidGas = MathExtra.RoundUpDouble(pAmount, 2); }
    public void SetPaidTithe(int pAmount) { paidTithe = pAmount; }

    //Clears all Data entered in yearly totals
    public void ClearAll()
    {
        employeeIncome = 0.0;
        for (int i = 0; i < jobIncome.Length; i++) { jobIncome[i] = 0.0; }
        for (int i = 0; i < fedTax.Length; i++) { SetFedTax(0, i); }
        for (int i = 0; i < stateTax.Length; i++) { SetStateTax(0, i); }
        for (int i = 0; i < paidTaxes.Length; i++) { SetPaidTaxes(0, i); }
        fedTaxBracketRemaining.Clear();
        fedTaxBracketRemaining = new List<double>();
        stateTaxBracketRemaining.Clear();
        stateTaxBracketRemaining = new List<double>();
    }

    //Updates all totals based on current data from jobs and tax rates
    public void UpdateYearlyTotals()
    {
        InitializeRemainingBrackets();
        foreach (Job j in book.GetJobs())
        {
            j.UpdateJob();
        }
        // Q0
        if (fedTaxBracketRemaining.Count > 0) CalculateFedTax(employeeIncome);
        if (stateTaxBracketRemaining.Count > 0) CalculateStateTax(employeeIncome);

        // Q1 - Q4
        for (int i = 1; i < 5; i++)
        {
            jobIncome[i] = 0.0;
            foreach (Job j in book.GetJobs())
            {
                jobIncome[i] += j.GetQuarterlyNetIncome()[i];
            }
            if (fedTaxBracketRemaining.Count > 0) SetFedTax((int)Math.Ceiling(CalculateFedTax(jobIncome[i])), i);
            if (stateTaxBracketRemaining.Count > 0) SetStateTax((int)Math.Ceiling(CalculateStateTax(jobIncome[i])), i);
        }
        // totals
        int[] fed = GetFedTax();
        int[] state = GetStateTax();
        jobIncome[0] = jobIncome[1] + jobIncome[2] + jobIncome[3] + jobIncome[4];
        SetFedTax(fed[1] + fed[2] + fed[3] + fed[4], 0);
        SetStateTax(state[1] + state[2] + state[3] + state[4], 0);
        UpdateTotalPaid();
    }
    
    //Updates to total of what has been entered as budgeted/paid
    public void UpdateTotalPaid()
    {
        int[] paid = GetPaidTaxes();
        SetPaidTaxes(paid[1] + paid[2] + paid[3] + paid[4], 0);
    }

    //Clears and sets up variables for calculating total taxes due
    private void InitializeRemainingBrackets()
    {
        fedTaxBracketRemaining.Clear();
        stateTaxBracketRemaining.Clear();
        fedTaxBracketRemaining = new List<double>();
        stateTaxBracketRemaining = new List<double>();
        currentFedBracket = 0;
        currentStateBracket = 0;
        List<Bracket> fedBracks = book.GetTaxRates().GetFedTaxBrackets();
        List<Bracket> stateBracks = book.GetTaxRates().GetStateTaxBrackets();
        double prevLimit = 0.0;
        foreach (Bracket fb in fedBracks)
        {
            double newLimit = fb.GetLimit();
            double adjustedLimit = newLimit - prevLimit;
            if (adjustedLimit < 0.0) adjustedLimit = 0.0;
            fedTaxBracketRemaining.Add(adjustedLimit);
            prevLimit = newLimit;
        }
        prevLimit = 0.0;

        foreach (Bracket sb in stateBracks)
        {
            double newLimit = sb.GetLimit();
            double adjustedLimit = newLimit - prevLimit;
            if (adjustedLimit < 0.0) adjustedLimit = 0.0;
            stateTaxBracketRemaining.Add(adjustedLimit);
            prevLimit = newLimit;
        }
    }

    private double CalculateFedTax(double pInput)
    {
        double taxDue = 0.0;
        double qir = pInput; // qir = Quarterly Income Remaining
        float selfEmploymentTax = book.GetTaxRates().SelfEmploymentTax();
        bool done = false;
        while (!done)
        {
            double tbr = fedTaxBracketRemaining[currentFedBracket]; // tbr = Tax Bracket Remaining
            double used = 0.0;
            if (tbr >= qir)
            {
                used = qir;
            }
            else
            {
                used = tbr;
            }
            fedTaxBracketRemaining[currentFedBracket] -= used;
            float rate = (book.GetTaxRates().GetFedTaxBrackets()[currentFedBracket].GetPercent() + selfEmploymentTax) * 0.01f;
            taxDue += used * rate;

            qir -= used;
            if (qir > 0.0)
            {
                if (currentFedBracket + 1 >= fedTaxBracketRemaining.Count)
                {
                    taxDue += qir * rate;
                    done = true;
                }
                else
                {
                    currentFedBracket++;
                }
            }
            else
            {
                done = true;
            }
        }
        return taxDue;
    }

    private double CalculateStateTax(double pInput)
    {
        double taxDue = 0.0;
        double qir = pInput; // qir = Quarterly Income Remaining
        bool done = false;
        while (!done)
        {
            double tbr = stateTaxBracketRemaining[currentStateBracket]; // tbr = Tax Bracket Remaining
            double used;
            if (tbr >= qir)
            {
                used = qir;
            }
            else
            {
                used = tbr;
            }
            stateTaxBracketRemaining[currentStateBracket] -= used;
            float rate = book.GetTaxRates().GetStateTaxBrackets()[currentStateBracket].GetPercent() * 0.01f;
            taxDue += used * rate;

            qir -= used;
            if (qir > 0.0)
            {
                if (currentStateBracket + 1 >= stateTaxBracketRemaining.Count)
                {
                    taxDue += qir * rate;
                    done = true;
                }
                else
                {
                    currentStateBracket++;
                }
            }
            else
            {
                done = true;
            }
        }
        return taxDue;
    }
}
