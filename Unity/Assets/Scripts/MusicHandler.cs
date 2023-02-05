using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class MusicHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("ambience", gameObject);
        AkSoundEngine.PostEvent("music_game", gameObject);
    }

    public void stopSounds()
    {
        AkSoundEngine.StopAll(gameObject);
    }
}
