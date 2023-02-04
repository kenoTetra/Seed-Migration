using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    public Toggle isFullscreen, isvSync;
    public List<resItem> resolutions = new List<resItem>();
    private int resIndex;
    public TMP_Text resLabel;
    private bool foundRes;

    void Start()
    {
        loadGraphics();

        // find the player's current resolution and select it
        for(int i = 0; i < resolutions.Count; i++)
        {
            if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                resIndex = i;
                updateResLabel();
            }
        }
    }

    public void resRight()
    {
        if(resIndex != resolutions.Count - 1)
            resIndex++;

        updateResLabel();
    }

    public void resLeft()
    {
        if(resIndex != 0)
            resIndex--;
        
        updateResLabel();
    }

    public void applyGraphics()
    {
        // set fullscreen
        Screen.fullScreen = isFullscreen.isOn;

        // set vsync
        if(isvSync.isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;

        // set resolution
        Screen.SetResolution(resolutions[resIndex].horizontal, resolutions[resIndex].vertical, isFullscreen.isOn);

        // Save preferences
        PlayerPrefs.SetInt("Fullscreen", Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetInt("vSync", QualitySettings.vSyncCount);
        PlayerPrefs.SetInt("ResIndex", resIndex);

    }

    public void loadGraphics()
    {
        // If there's a fullscreen key, load it.
        if(PlayerPrefs.HasKey("Fullscreen"))
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen"));

        else
            isFullscreen.isOn = Screen.fullScreen;

        // If there's a vSync key, load it.
        if(PlayerPrefs.HasKey("vSync"))
            QualitySettings.vSyncCount = PlayerPrefs.GetInt("vSync");

        else
        {
            if(QualitySettings.vSyncCount == 0)
                isvSync.isOn = false;
            else
                isvSync.isOn = true;
        }

        if(PlayerPrefs.HasKey("ResIndex"))
        {
            resIndex = PlayerPrefs.GetInt("ResIndex");
            Screen.SetResolution(resolutions[resIndex].horizontal, resolutions[resIndex].vertical, isFullscreen.isOn);
        }

    }

    void updateResLabel()
    {
        resLabel.text = resolutions[resIndex].horizontal.ToString() + " x " + resolutions[resIndex].vertical.ToString();
    }
}

[System.Serializable]
public class resItem
{
    public int horizontal, vertical;
}
