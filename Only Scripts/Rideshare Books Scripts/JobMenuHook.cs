//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.

using UnityEngine;
using TMPro;

public class JobMenuHook : MonoBehaviour
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private TMP_Text tabTtitleText;
    [SerializeField] private TabButton tab;
    [SerializeField] private Job JobData;
    [SerializeField] private string jobName;

    // getters
    public TabButton GetTab() { return tab; }
    public string GetJobName() { return jobName; }
    public Job GetJobData() { return JobData; }

    // setters
    public void SetTabTitleText(string pTitle) { tabTtitleText.text = pTitle; }
    public void SetName(string pName) { jobName = pName; }
    public void SetJobData(Job pData) { JobData = pData; }
}
