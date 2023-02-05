using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string title = "This is a title";
    public string body = "This is the body";
    public int price = 5;
    
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        TooltipHandler.instance.setShowTooltip(title, body, price);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        TooltipHandler.instance.hideTooltip();
    }
}
