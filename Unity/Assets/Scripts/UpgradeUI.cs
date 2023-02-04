using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    // Panels
    public GameObject[] upgradePanels;

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
}
