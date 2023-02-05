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
    public bool expPriceInc;
    [Space(5)]

    [Header("Tooltip Information")]
    public string title = "This is a title";
    public string body = "This is the body";

    private Animator animator;
    private UpgradeUI uui;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        uui = FindObjectOfType<UpgradeUI>();
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
                AkSoundEngine.PostEvent("shop_buy", gameObject);

                // Info update
                level++;
                PlayerPrefs.SetInt(prefKey, level);

                // Animator update
                updateAnimator();
            
                // Take the man's money
                uui.useScore(-price);

                // Price increase

                if(expPriceInc)
                {
                    price = price * price;
                }

                else
                {
                    price += price;
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
                AkSoundEngine.PostEvent("shop_max", gameObject);
            }  
        }

        else
        {
            Debug.Log("YOU''RE FUCKING POOR");
            AkSoundEngine.PostEvent("menu_cancel", gameObject);
        }

    }

    public void sellUpgrade()
    {
        Debug.Log("i sold my soul for 5 points");
        AkSoundEngine.PostEvent("shop_sell", gameObject);
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
                    price += price;
                }
            }

            if(level == maxLevel)
            {
                price = 0;
            }

            updateAnimator();
        }
    }

    void updateAnimator()
    {
        animator.SetInteger("Level", level);
    }

    // TOOLTIP HANDLERS \\
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(price > 0)
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
        if(price > 0)
        {
            TooltipHandler.instance.setShowTooltip(title, body, price.ToString(), animator.GetInteger("Level"));
        }
        
        else
        {
            TooltipHandler.instance.setShowTooltip(title, body, "MAX!", animator.GetInteger("Level"));
        }
    }
}
