using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour
{
    [Header("Scene Info")]
    public string sceneName;
    public Animator animator;

    public void changeScene(string scene)
    {
        sceneName = scene;
        animator.SetBool("Fade", true);
    }

    public void nextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
