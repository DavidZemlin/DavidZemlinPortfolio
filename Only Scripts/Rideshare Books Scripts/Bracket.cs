//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using System;
using UnityEngine;

public class Bracket : MonoBehaviour, IComparable<Bracket>
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private int limit;
    [SerializeField] private float percent;

    public int GetLimit() { return limit; }
    public float GetPercent() { return percent; }

    public void SetLimit(int pLimit) { limit = pLimit; }
    public void SetPercent(float pPercent) { percent = pPercent; }

    public void SetAll(int pLimit, float pPercent)
    {
        SetLimit(pLimit);
        SetPercent(pPercent);
    }

    public int CompareTo(Bracket other)
    {
        if (other == null)
        {
            return 1;
        }
        else
        {
            return limit.CompareTo(other.GetLimit());
        }
    }
}
