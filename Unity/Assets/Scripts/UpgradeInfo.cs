using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Upgrade Data")]
    public int level;
    public int maxLevel = 4;
    public string prefKey;
    public int price = 10;
    public int multpVal = 2;
    public bool expPriceInc;
    public GameObject[] unlockables;
    public Animator acornAnimator;
    public bool acornAnimBool;
    public string acornAnimString;
    private int priceSaved;
    [Space(5)]

    [Header("Audio")]
    public AudioSource aud;
    public AudioClip hoverClip,maxClip,buyClip,poorClip;
    
    [Space(5)]

    [Header("Tooltip Information")]
    public string title = "This is a title";
    public string body = "This is the body";
    public bool isMaxLevel;

    private Animator animator;
    private UpgradeUI uui;

    // Start is called before the first frame update
    void Start()
    {
        isMaxLevel = false;
        animator = GetComponent<Animator>();
        uui = FindObjectOfType<UpgradeUI>();

        foreach(GameObject unlock in unlockables)
        {
            unlock.SetActive(false);
        }

        updateInfo();
    }

    public void buyUpgrade()
    {
        if(uui.score >= price)
        {
            if(level < maxLevel)
            {
                // Sound!
                Debug.Log("i bought dick for three ninty nine");
                playBuy();
                // buy sound here

                // Info update
                level++;
                PlayerPrefs.SetInt(prefKey, level);

                // Animator update
                updateAnimator();

                // Acorn animation (if active)
                animateAcorn();
            
                // Take the man's money
                uui.useScore(-price);

                // Price increase

                if(expPriceInc)
                {
                    price = price * price;
                }

                else
                {
                    price = price * multpVal;
                }

                priceSaved = price;

                // Unlocks new things!
                foreach(GameObject unlock in unlockables)
                {
                    unlock.SetActive(true);
                }

                // Tooltip update
                resetTooltip();
            }

            // edgecase bug fix
            else if(level > maxLevel)
            {
                level = maxLevel;
                updateAnimator();
            }

            else
            {
                Debug.Log("you're ALREADY rich dude, fuck off!!!");
                // shop max sound
                playMax();
            }  
        }

        else
        {
            Debug.Log("YOU''RE FUCKING POOR");
            // menu cancel sound
            playPoor();
        }

    }

    void updateInfo()
    {
        // If you have the key
        if(PlayerPrefs.HasKey(prefKey))
        {
            level = PlayerPrefs.GetInt(prefKey);

            if(level > maxLevel)
            {
                level = maxLevel;
            }

            // Increase cost based on level, loops.
            for(int i = 0; i < level; i++)
            {
                if(expPriceInc)
                {
                    price = price * price;
                }

                else
                {
                    price = price * multpVal;
                }
            }

            // If you're at max level, sets price to 0.
            if(level == maxLevel)
            {
                isMaxLevel = true;
            }

            priceSaved = price;

            // Enable all unlockables if you have the key.
            foreach(GameObject unlock in unlockables)
            {
                unlock.SetActive(true);
            }

            updateAnimator();

            // If acorn real
            animateAcorn();
        }
    }

    void updateAnimator()
    {
        animator.SetInteger("Level", level);
    }

    // ACORN ANIMATIONS \\
    public void animateAcorn()
    {
        if(acornAnimator != null)
        {
            acornAnimator.SetBool(acornAnimString, acornAnimBool);
        }
    }


    // TOOLTIP HANDLERS \\
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(!isMaxLevel)
        {
            TooltipHandler.instance.setShowTooltip(title, body, price.ToString(), animator.GetInteger("Level"));
        }

        else
        {
            TooltipHandler.instance.setShowTooltip(title, body, "MAX!", animator.GetInteger("Level"));
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        TooltipHandler.instance.hideTooltip();
    }

    void resetTooltip()
    {
        TooltipHandler.instance.hideTooltip();
        if(!isMaxLevel)
        {
            TooltipHandler.instance.setShowTooltip(title, body, price.ToString(), animator.GetInteger("Level"));
        }
        
        else
        {
            TooltipHandler.instance.setShowTooltip(title, body, "MAX!", animator.GetInteger("Level"));
        }
    }

    public void playHover()
    {
        aud.PlayOneShot(hoverClip);
    }

    public void playBuy()
    {
        aud.PlayOneShot(buyClip);
    }

    public void playMax()
    {
        aud.PlayOneShot(maxClip);
    }

    public void playPoor()
    {
        aud.PlayOneShot(poorClip);
    }
}
