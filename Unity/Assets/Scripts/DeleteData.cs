using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeleteData : MonoBehaviour
{
    public GameObject deletePanel;
    public GameObject optionsPanel;
    private MainMenu mm;

    void Start()
    {
        mm = FindObjectOfType<MainMenu>();
    }

    public void goBack()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
        deletePanel.SetActive(!deletePanel.activeSelf);
    }

    public void deleteData()
    {
        if(PlayerPrefs.HasKey("Score"))
        {
            mm.continueToggle();
        }
        
        PlayerPrefs.DeleteAll();
        goBack();
    }
}
