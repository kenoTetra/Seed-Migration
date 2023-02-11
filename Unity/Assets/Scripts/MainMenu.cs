using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject creditsPanel;
    public GameObject optionsPanel;
    public GameObject mainPanel;
    public GameObject deletePanel;
    [Space(5)]

    [Header("Buttons")]
    public GameObject startButton;
    public GameObject continueButton;
    [Space(5)]

    [Header("Audio")]
    public AudioClip clickClip;
    public AudioClip backClip,hoverClip,startClip,continueClip,deleteClip;

    [Header("References")]
    private OptionsScript os;
    public AudioSource aud;

    void Start()
    {
        os = FindObjectOfType<OptionsScript>();
        os.loadGraphics();
        aud = GetComponent<AudioSource>();

        if(PlayerPrefs.HasKey("Score"))
        {
            continueToggle();
        }
    }

    public void quit()
    {
        Application.Quit(0);
    }

    public void creditsToggle()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
        mainPanel.SetActive(!mainPanel.activeSelf);
    }

    public void optionsToggle()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
        mainPanel.SetActive(!mainPanel.activeSelf);
    }

    public void deleteToggle()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
        deletePanel.SetActive(!deletePanel.activeSelf);
    }

    public void continueToggle()
    {
        startButton.SetActive(!startButton.activeSelf);
        continueButton.SetActive(!continueButton.activeSelf);
    }

    public void playHover()
    {
        aud.PlayOneShot(hoverClip);
    }

    public void playBack()
    {
        aud.PlayOneShot(backClip);
    }

    public void playClick()
    {
        aud.PlayOneShot(clickClip);
    }

    public void playStart()
    {
        aud.PlayOneShot(startClip);
    }

    public void playContinue()
    {
        aud.PlayOneShot(continueClip);
    }

    public void playDelete()
    {
        aud.PlayOneShot(deleteClip);
    }
}
