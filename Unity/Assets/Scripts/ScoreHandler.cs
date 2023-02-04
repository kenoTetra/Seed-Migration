using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    [Header("Seed Data")]
    private GameObject seed;
    private float startX;
    private float distanceTraveled;
    [Space(5)]

    [Header("Score Data")]
    private int roundedScore;
    private int leftoverScore = 0;
    [Space(5)]

    [Header("Score Visuals")]
    public TMP_Text scoreText;
    public string scoreHeader = "Score: ";

    // Start is called before the first frame update
    void Start()
    {
        // Seed references
        seed = GameObject.FindWithTag("Seed");
        startX = seed.transform.position.x;

        // If the player has already played once, find their score and set it to leftover
        if(PlayerPrefs.HasKey("Score"))
        {
            leftoverScore = PlayerPrefs.GetInt("Score");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // When the seed is simulating
        if(seed.GetComponent<Rigidbody2D>().simulated)
        {
            // And if they're not moving backwards (when rocking that happens)
            if(seed.transform.position.x - startX > distanceTraveled)
            {
                // Push their position moved from their startpos rounded up (.01 -> 1) to the score.
                distanceTraveled = seed.transform.position.x - startX;
                roundedScore = (int)Math.Ceiling(distanceTraveled);
                scoreText.text = scoreHeader + roundedScore.ToString();
            }
        }
    }

    public void pushScore()
    {
        // add current score and any leftover to their total score
        PlayerPrefs.SetInt("Score", roundedScore + leftoverScore);        
    }
}
