//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System.Collections.Generic;
using UnityEngine;

//Collection of all tax rate information
public class TaxRates : MonoBehaviour
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private float mileageRate;
    [SerializeField] private float soSecTaxRate;
    [SerializeField] private float medicareTaxRate;
    [SerializeField] private int taxYear;
    [SerializeField] private List<Bracket> fedTaxBrackets;
    [SerializeField] private List<Bracket> stateTaxBrackets;
    [SerializeField] private GameObject fedTaxBracketsHolder;
    [SerializeField] private GameObject stateTaxBracketsHolder;
    [SerializeField] private GameObject bracketPrefab;

    public float GetMileageRate() { return mileageRate; }
    public float GetSoSecTaxeRate() { return soSecTaxRate; }
    public float GetMedicareTaxRate() { return medicareTaxRate; }
    public int GetTaxYear() { return taxYear;}
    public List<Bracket> GetFedTaxBrackets() { return fedTaxBrackets; }
    public List<Bracket> GetStateTaxBrackets() { return stateTaxBrackets; }

    public void SetMileageRate(float pMileageRate) { mileageRate = pMileageRate; }
    public void SetSoSecTaxeRate(float pSoSecTaxRate) { soSecTaxRate =pSoSecTaxRate; }
    public void SetMedicareTaxRate(float pMedicareTaxRate) { medicareTaxRate = pMedicareTaxRate; }
    public void SetTaxYear(int pTaxYear) { taxYear = pTaxYear; }

    //Calculates and Returns the Self Employment Tax Rate
    public float SelfEmploymentTax()
    {
        return GetSoSecTaxeRate() + GetMedicareTaxRate();
    }

    //Adds the next federal tax bracket
    public Bracket AddFedBracket()
    {
        GameObject go = Instantiate(bracketPrefab, fedTaxBracketsHolder.transform);
        Bracket newBracket = go.GetComponent<Bracket>();
        fedTaxBrackets.Add(newBracket);
        go.name = go.name + fedTaxBrackets.IndexOf(newBracket);
        return newBracket;
    }

    //Adds the next state tax bracket
    public Bracket AddStateBracket()
    {
        GameObject go = Instantiate(bracketPrefab, stateTaxBracketsHolder.transform);
        Bracket newBracket = go.GetComponent<Bracket>();
        stateTaxBrackets.Add(newBracket);
        go.name = go.name + stateTaxBrackets.IndexOf(newBracket);
        return newBracket;
    }

    public void ClearFedBracket()
    {
        foreach (Bracket b in fedTaxBrackets)
        {
            Destroy(b.gameObject);
        }
        fedTaxBrackets.Clear();
        fedTaxBrackets = new List<Bracket>();
    }

    public void ClearStateBracket()
    {
        foreach (Bracket b in stateTaxBrackets)
        {
            Destroy(b.gameObject);
        }
        stateTaxBrackets.Clear();
        stateTaxBrackets = new List<Bracket>();
    }

    public void ClearAllTaxRates()
    {
        ClearFedBracket();
        ClearStateBracket();
        soSecTaxRate = 0;
        medicareTaxRate = 0;
        mileageRate = 0;
        taxYear = 0;
    }

    public void RemoveFedBracket(Bracket pBracket)
    {
        fedTaxBrackets.Remove(pBracket);
        Destroy(pBracket.gameObject);
    }

    public void RemoveStateBracket(Bracket pBracket)
    {
        stateTaxBrackets.Remove(pBracket);
        Destroy(pBracket.gameObject);
    }

    public void SortFedBrackets()
    {
        fedTaxBrackets.Sort();
    }

    public void SortStateBrackets()
    {
        stateTaxBrackets.Sort();
    }
}
