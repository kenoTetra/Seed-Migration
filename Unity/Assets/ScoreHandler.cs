using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    private GameObject seed;
    private float startX;
    private float distanceTraveled;
    private int roundedScore;
    public TMP_Text scoreText;
    public string scoreHeader = "Score: ";

    // Start is called before the first frame update
    void Start()
    {
        seed = GameObject.FindWithTag("Seed");
        startX = seed.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(seed.GetComponent<Rigidbody2D>().simulated)
        {
            if(seed.transform.position.x - startX > distanceTraveled)
            {
                distanceTraveled = seed.transform.position.x - startX;
                roundedScore = (int)Math.Ceiling(distanceTraveled);
                scoreText.text = scoreHeader + roundedScore.ToString();
            }
        }
    }
}
