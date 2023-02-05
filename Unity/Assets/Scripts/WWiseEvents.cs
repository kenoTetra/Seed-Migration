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

    public void onDelete()
    {
        Debug.Log("This is so sad, can we hit 30 children?");
        AkSoundEngine.PostEvent("save_delete", gameObject);
    }
}
