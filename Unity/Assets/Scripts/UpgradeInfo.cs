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
    [Space(5)]

    [Header("Tooltip Information")]
    public string title = "This is a title";
    public string body = "This is the body";

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        updateInfo();
    }

    public void buyUpgrade()
    {
        if(level <= maxLevel)
        {
            // Sound!
            Debug.Log("i bought dick for three ninty nine");
            AkSoundEngine.PostEvent("shop_buy", gameObject);

            // Info update
            level++;
            PlayerPrefs.SetInt(prefKey, level);

            // Animator update
            updateAnimator();
        }

        else
        {
            Debug.Log("you're ALREADY rich dude, fuck off!!!");
            AkSoundEngine.PostEvent("shop_max", gameObject);
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
        TooltipHandler.instance.setShowTooltip(title, body, price, animator.GetInteger("Level"));
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        TooltipHandler.instance.hideTooltip();
    }
}
