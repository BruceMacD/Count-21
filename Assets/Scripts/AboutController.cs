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
    //UI Sounds
    public AudioSource click;

    public void ClosePanel()
    {
        //hide any open panel
        controlsPanel.SetActive(false);
        countingPanel.SetActive(false);
        creditsPanel.SetActive(false);

        closeButton.SetActive(false);
    }

    public void OpenControlsPanel()
    {
        PlaySound();

        controlsPanel.SetActive(true);
        closeButton.SetActive(true);
    }

    public void OpenCountingPanel()
    {
        PlaySound();

        countingPanel.SetActive(true);
        closeButton.SetActive(true);
    }

    public void OpenCreditsPanel()
    {
        PlaySound();

        creditsPanel.SetActive(true);
        closeButton.SetActive(true);
    }

    public void PlaySound()
    {
        click.Play();
    }
}
