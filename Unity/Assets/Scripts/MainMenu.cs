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

    [Header("References")]
    private OptionsScript os;

    void Start()
    {
        os = FindObjectOfType<OptionsScript>();
        os.loadGraphics();

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
}
