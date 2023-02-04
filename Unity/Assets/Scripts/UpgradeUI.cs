using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    // Panels
    public GameObject[] upgradePanels;
    public TMP_Text scoreText;
    public string scoreHeader = "Score :";
    public int score = 0;

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
}
