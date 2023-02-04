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
    [Space(5)]

    [Header("References")]
    private OptionsScript os;

    void Start()
    {
        os = FindObjectOfType<OptionsScript>();
        os.loadGraphics();
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
}
