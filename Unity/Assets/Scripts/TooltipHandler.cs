using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipHandler : MonoBehaviour
{
    public static TooltipHandler instance;

    public TMP_Text title,body,cost,level;

    public string symbol = " pts";
    public string levelText = "Lvl. ";

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void setShowTooltip(string titleString, string bodyString, string price, int found_level)
    {
        gameObject.SetActive(true);
        title.text = titleString;
        body.text = bodyString;

        if(price != "MAX!")
        {
            cost.text = price + symbol;
        }

        else
        {
            cost.text = price;
        }
        
        level.text = levelText + found_level.ToString();
    }

    public void hideTooltip()
    {
        gameObject.SetActive(false);
        title.text = string.Empty;
        body.text = string.Empty;
        cost.text = string.Empty;
        level.text = string.Empty;
    }
}
