//This document and all its contents are copyrighted by David Zemlin and my not be used or reproduced without express written consent.
 
using UnityEngine;

public class BookViewerBridge : MonoBehaviour
{
    //All Fields are serialized for development purposes and debugging in Unity. Most of these need to be un-serialized in final build.
    [SerializeField] private BookViewer bookViewer;
    [SerializeField] private JobMenuHook job;

    private void Awake()
    {
        bookViewer = GameObject.FindGameObjectWithTag("BookViewer").GetComponent<BookViewer>();
    }

    public void SetCurrentJobInBookViewer()
    {
        bookViewer.SetActiveJob(job.GetJobData(), job, true);
    }
}
