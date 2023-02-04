using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeUI : MonoBehaviour
{
    // Panels
    public GameObject[] upgradePanels;
    [Space(5)]

    [Header("Start Stuff")]
    public Animator animator;
    private string sceneName;

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

    public void startGame(string scene)
    {
        sceneName = scene;
        animator.SetBool("Fade", true);
    }

    public void nextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
