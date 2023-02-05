using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AK.Wwise;

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
    public float preventTime = .33f;
    private bool timedOut;
    private bool canTimeout;
    [Space(5)]

    // private playonce
    private bool soundTimer;

    [Header("References")]
    private ScoreHandler sh;
    private TransitionHandler th;
    private Rigidbody2D rb;
    private MusicHandler mh;

    // Start is called before the first frame update
    void Start()
    {
        // Reference data.
        sh = FindObjectOfType<ScoreHandler>();
        th = FindObjectOfType<TransitionHandler>();
        mh = FindObjectOfType<MusicHandler>();
        rb = GameObject.FindWithTag("Seed").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.simulated)
        {
            noClockSpawn += Time.deltaTime;
        }
        
        if((rb.velocity.magnitude <= 0.2f || Vector2.Dot(rb.velocity, Vector2.right) < 0.0f) && !timedOut && rb.simulated && noClockSpawn > 1f && canTimeout)
        {
            StartCoroutine(timeoutPlayer());
        }

        if(rb.velocity.magnitude > 0.35f)
        {
            soundTimer = false;
            timer = 0f;
            stopClock();
            StopCoroutine(timeoutPlayer());
            StartCoroutine(preventTimeout());
        }
    }
    
    private IEnumerator timeoutPlayer()
    {
        timer += Time.deltaTime;

        // clock visuals
        clock.SetActive(true);
        clockFill.fillAmount = timer/timeoutTime;
        
        if(!soundTimer)
        {
            AkSoundEngine.PostEvent("clock_ticking", gameObject);
            soundTimer = true;
        }

        // if you hit timeout time, push score and go to upgrades
        if(timer >= timeoutTime)
        {
            sh.pushScore();
            th.changeScene("UpgradeMenu");
            mh.stopSounds();
            AkSoundEngine.PostEvent("game_start", gameObject);
            timedOut = true;
            yield break;
        }
    }

    private IEnumerator preventTimeout()
    {
        canTimeout = false;

        yield return new WaitForSeconds(preventTime);

        canTimeout = true;

        yield break;
    }

    void stopClock()
    {
        AkSoundEngine.StopAll(gameObject);
    }
}
