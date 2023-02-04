using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeoutHandler : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject clock;
    public Image clockFill;
    [Space(5)]

    [Header("Timers")]
    private float timer;
    private float noClockSpawn;
    public float timeoutTime = 4f;
    private bool timedOut;
    [Space(5)]

    [Header("References")]
    private ScoreHandler sh;
    private TransitionHandler th;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Reference data.
        sh = FindObjectOfType<ScoreHandler>();
        th = FindObjectOfType<TransitionHandler>();
        rb = GameObject.FindWithTag("Seed").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.simulated)
        {
            noClockSpawn += Time.deltaTime;
        }
        
        if(rb.velocity == Vector2.zero && !timedOut && rb.simulated && noClockSpawn > 1f)
        {
            StartCoroutine(timeoutPlayer());
        }
    }
    
    private IEnumerator timeoutPlayer()
    {
        timer += Time.deltaTime;

        // clock visuals
        clock.SetActive(true);
        clockFill.fillAmount = timer/timeoutTime;

        // if you hit timeout time, push score and go to upgrades
        if(timer >= timeoutTime)
        {
            sh.pushScore();
            th.changeScene("UpgradeMenu");
            timedOut = true;
            yield break;
        }

        // if the acorn moves, stop everything!
        if(rb.velocity != Vector2.zero)
        {
            clock.SetActive(false);
            timer = 0f;
            yield break;
        }
    }
}
