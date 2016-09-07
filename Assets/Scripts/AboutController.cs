using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class AboutController : MonoBehaviour
{
    public GameObject closeButton;
    //panels
    public GameObject controlsPanel;
    public GameObject countingPanel;
    public GameObject creditsPanel;

    public void ClosePanel()
    {
        controlsPanel.SetActive(false);
        countingPanel.SetActive(false);
        creditsPanel.SetActive(false);

        closeButton.SetActive(false);
    }

    public void OpenControlsPanel()
    {
        controlsPanel.SetActive(true);
        closeButton.SetActive(true);
    }

    public void OpenCountingPanel()
    {
        countingPanel.SetActive(true);
        closeButton.SetActive(true);
    }

    public void OpenCreditsPanel()
    {
        creditsPanel.SetActive(true);
        closeButton.SetActive(true);
    }
}
