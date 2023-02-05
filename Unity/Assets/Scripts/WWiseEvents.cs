using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class WWiseEvents : MonoBehaviour
{
    public void onClick()
    {
        Debug.Log("lets go!!!!!!!!!");
        AkSoundEngine.PostEvent("menu_confirm", gameObject);
    }

    public void onHover()
    {
        Debug.Log("will they won't they");
        AkSoundEngine.PostEvent("menu_hover", gameObject);
    }

    public void onBack()
    {
        Debug.Log("return to hell");
        AkSoundEngine.PostEvent("menu_cancel", gameObject);
    }

    public void onStart()
    {
        Debug.Log("Start/Continue");
        AkSoundEngine.PostEvent("game_start", gameObject);
    }

    public void onPurchase()
    {
        Debug.Log("i bought dick for three ninty nine");
        AkSoundEngine.PostEvent("shop_buy", gameObject);
    }

    public void onSell()
    {
        Debug.Log("i sold my soul for 5 points");
        AkSoundEngine.PostEvent("shop_sell", gameObject);
    }

    public void onDelete()
    {
        Debug.Log("This is so sad, can we hit 30 children?");
        AkSoundEngine.PostEvent("save_delete", gameObject);
    }
}
