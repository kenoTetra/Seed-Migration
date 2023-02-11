using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    // Panels
    [Header("Panels and Data")]
    public GameObject[] upgradePanels;
    public TMP_Text scoreText;
    public string scoreHeader = "Score :";
    public int score = 0;
    [Space(5)]

    [Header("Audio")]
    public AudioSource aud;
    public AudioClip hoverClip,clickClip,startClip,menuClip;

    void Start()
    {
        if(PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }

        updateScoreText();
    }

    public void openMenu(int menuNumber)
    {
        for (int i = 0; i < upgradePanels.Length; i++)
        {
            if(i == menuNumber)
            {
                upgradePanels[i].SetActive(true);
            }

            else
            {
                upgradePanels[i].SetActive(false);
            }
        }
    }

    public void updateScoreText()
    {
        scoreText.text = scoreHeader + score.ToString();
    }

    public void useScore(int amount)
    {
        score += amount;
        updateScoreText();
        PlayerPrefs.SetInt("Score", score);
    }

    public void playStart()
    {
        aud.PlayOneShot(startClip);
    }

    public void playHover()
    {
        aud.PlayOneShot(hoverClip);
    }

    public void playClick()
    {
        aud.PlayOneShot(clickClip);
    }

    public void playMenu()
    {
        aud.PlayOneShot(menuClip);
    }
}
