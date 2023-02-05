using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipHandler : MonoBehaviour
{
    public static TooltipHandler instance;

    public TMP_Text title,body,cost;

    public string symbol = " pts";

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

    public void setShowTooltip(string titleString, string bodyString, int price)
    {
        gameObject.SetActive(true);
        title.text = titleString;
        body.text = bodyString;
        cost.text = price.ToString() + symbol;
    }

    public void hideTooltip()
    {
        gameObject.SetActive(false);
        title.text = string.Empty;
        body.text = string.Empty;
        cost.text = string.Empty;
    }
}
