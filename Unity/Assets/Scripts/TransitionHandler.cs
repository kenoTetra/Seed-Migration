using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour
{
    [Header("Scene Info")]
    public string sceneName;
    public Animator animator;

    // USE THIS ONE!!!!!!!!!! USE THIS ONE!!!!!!!!!!!!!! USE THIS ONE!!!!!!!!!!!!!!!1
    public void changeScene(string scene)
    {
        // fades out the screen, animation will call next scene!
        sceneName = scene;
        animator.SetBool("Fade", true);
    }

    public void nextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
