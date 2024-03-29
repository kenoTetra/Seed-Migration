using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AK.Wwise;

public class Seed_Controller : MonoBehaviour
{
    [Header("Normal Data")]
    public bool grounded;
    public RaycastHit2D downRay;
    public LayerMask groundLayer;
    public float sailModifier;
    public GameObject mainWind;
    public float windModifier;
    public float fuelTank;
    public float germinationTimer;
    public float germinationCDTimer;
    public bool germinating;
    public bool germinatingBounce;
    public GameObject scoreHandler;
    public float leafPowerTimer;
    public float velYClamp = 35f;
    [Space(5)]

    [Header("Upgrades")]
    public float sail;
    public int gravity;
    public int weight;
    public int wind;
    public bool rocket;
    public float rocketStrength;
    public int rocketFuel;
    public bool germination;
    public int germinationDuration;
    public int germinationCooldown;
    public int fuelEfficency;
    public int jermaSpeed;
    public int clamp;
    public int turnStab;
    [Space(5)]

    // Balance Data
    [Header("Upgrade Balance")]
    public float rocketStrengthBalance = 6f;
    public float fuelEffBalance = .5f;
    public float jermaSpeedBalance = .5f;
    public float clampBalance = 1.25f;
    public float turnStabBalance = .5f;
    [Space(5)]

    // Powers
    [Header("Active Powerups")]
    public bool windPower;
    public bool leafPower;
    public bool pointPower;
    public bool fuelPower;
    [Space(5)]

    [Header("UI")]
    public GameObject hintText;
    public GameObject fuelUI;
    public GameObject leafUI;
    public Image fuelUIFill;
    public Image leafUIFill;
    public TMP_Text distance,height,velocity;
    public string distanceBlurb = "Distance:";
    public string heightBlurb = "Height: ";
    public string velocityBlurb = "Velocity:";
    public string unitBlurb = "m/s";
    [Space(5)]

    [Header("Reference Data")]
    public Vector2 sampleStartPos;
    public Vector2 posOffset = new Vector2(0f, -2.5f);

    // Sound trigger bools
    private bool soundWind,soundBounce,soundRocket,soundGerminate;
    private bool inWind;

    // References
    private Rigidbody2D rb;
    private Animator animator;
    private ScoreHandler sh;

    // Start is called before the first frame update
    void Start()
    {
        // references
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sh = FindObjectOfType<ScoreHandler>();

        // get upgrades
        sail = checkSetKeyInt("Sail") / 2f;
        weight = checkSetKeyInt("Mass");
        rocket = checkSetKeyBool("Rocket");
        rocketStrength = checkSetKeyInt("RocketSpeed") / rocketStrengthBalance;
        rocketFuel = checkSetKeyInt("Fuel");
        germination = checkSetKeyBool("Germination");
        germinationDuration = checkSetKeyInt("Sugar");
        germinationCooldown = checkSetKeyInt("JermaCooldown");
        fuelEfficency = checkSetKeyInt("FuelEfficency");
        jermaSpeed = checkSetKeyInt("JermaSpeed");
        clamp = checkSetKeyInt("Clamp");
        turnStab = checkSetKeyInt("TurnStab");

        // get powers
        windPower = checkSetKeyBool("AirCurrent");
        leafPower = checkSetKeyBool("Leaves");
        pointPower = checkSetKeyBool("GenomePickups");
        fuelPower = checkSetKeyBool("FuelPower");

        // set upgrades rigidbody
        rb.gravityScale = 0.5f * (1.0f - 0.125f * gravity);
        rb.mass = 2.0f * (1.0f - 0.0625f * weight);

        // set upgrades rocket
        fuelTank = 3.0f + (1.0f * rocketFuel);

        // set upgrade visuals
        animator.SetBool("Rocket", rocket);
        animator.SetBool("Leaf", germination);

        // set upgrade UI visuals
        fuelUI.SetActive(rocket);
        leafUI.SetActive(germination);

        // set samples start position
        sampleStartPos = new Vector2 (transform.position.x, transform.position.y);
        sampleStartPos += posOffset;
    }

    // Update is called once per frame
    void Update()
    {
        // Sound data (really scuffed)
        playSoundOnce();

        // If the player hits jump, start the game.
        if (Input.GetButtonDown("Jump") && !rb.simulated)
        {
            hintText.SetActive(false);
            mainWind.SetActive(true);
            AkSoundEngine.PostEvent("acorn_detach", gameObject);
            rb.simulated = true;
        }

        groundedUpdate();

        leafPowerUpdate();

        if (rocket)
            rocketUpdate();

        // Jerma Nation
        if (germination)
            germinationUpdate();

        clampUpdate();

        uiUpdate();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Null check.
        if(collision != null)
        {
            // Collects the Wind Power
            if (collision.name == "WindPower(Clone)")
            {
                collision.GetComponent<WindPowerScript>().Activate();
            }
            // Collects the Leaf Power
            if (collision.name == "LeafPower(Clone)")
            {
                leafPowerTimer += 5.0f;
                Destroy(collision.gameObject);
            }
            // Collects the Point Power
            if (collision.name == "PointPower(Clone)")
            {
                sh.scoreBonus += (int)Math.Ceiling(50 * (1 + (sh.Markiplier * sh.MarkpilierEff)));
                Destroy(collision.gameObject);
            }
            // Collects the Fuel Power
            if (collision.name == "FuelPower(Clone)")
            {
                fuelTank += .5f;
                Destroy(collision.gameObject);
            }

            // Catches the wind
            if (collision.name == "Wind(Clone)")
            {
                rb.AddForce(collision.transform.right * collision.GetComponent<Wind_Script>().strength * (1.0f + windModifier * (float)wind));

                // Calculates the sail effect
                if (grounded == false)
                {
                    rb.AddForce(transform.up * (collision.GetComponent<Wind_Script>().strength * Vector3.Dot(transform.up, collision.transform.right)) * ((float)sail * sailModifier));
                }

                inWind = true;

            }
            else if (collision.name == "MainWind")
            {
                rb.AddForce(collision.transform.right * collision.GetComponent<MainWind_Script>().strength * (1.0f + windModifier * (float)wind));

                // Calculates the sail effect
                if (grounded == false)
                {
                    rb.AddForce(transform.up * (collision.GetComponent<MainWind_Script>().strength * Vector3.Dot(transform.up, collision.transform.right)) * ((float)sail * sailModifier));
                }

                inWind = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.name == "Wind(Clone)")
        {
            inWind = false;
        }

        if(col.name == "MainWind")
        {
            inWind = false;
        }
    }

    int checkSetKeyInt(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }

        return 0;
    }

    bool checkSetKeyBool(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            Debug.Log(key);
            return PlayerPrefs.GetInt(key) != 0;
        }

        return false;
    }

    void groundedUpdate()
    {
        // Checks for collisions with a raycast.
        downRay = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);

        // Sets grounded to true or false based on if any colliders are found.
        if (downRay.collider != null)
        {
            grounded = true;
        }

        else
        {
            grounded = false;
        }

        // Allows rotation when not grounded
        if (grounded == false)
        {
            float horizInput = Input.GetAxis("Horizontal");

            // Add the powerups allowing multiplicitive rotations.
            horizInput = horizInput * (1 + (turnStab * turnStabBalance));

            // Rotates the player based on input.
            if (rb.angularVelocity < 100.0f && horizInput <= 0.0f)
            {
                rb.AddTorque(-horizInput);
            }
            
            else if (rb.angularVelocity > -100.0f && horizInput >= 0.0f)
            {
                rb.AddTorque(-horizInput);
            }

            germinatingBounce = false;
        }

        else if (germinatingBounce == false)
        {
            germinationCDTimer = Mathf.Min(20.0f - (2.0f * germinationCooldown), germinationCDTimer + 2.0f);
            germinatingBounce = true;
        }
    }

    void leafPowerUpdate()
    {
        // Leaf Power effect
        if (leafPowerTimer > 0.0f)
        {
            leafPowerTimer -= Time.deltaTime;
            sailModifier = 5.0f;
        }

        else
        {
            sailModifier = 3.5f;
            leafPowerTimer = 0.0f;
        }
    }

    void germinationUpdate()
    {
        if (germinationCDTimer > 0.0f)
        {
            germinationCDTimer -= Time.deltaTime;
        }

        else if (germinationTimer > 0.0f && Input.GetButtonDown("Fire2") && rb.simulated)
        {
            germinating = true;
        }

        else if (germinating == false)
        {
            germinationTimer = 1.0f + (0.5f * germinationDuration);
            germinationCDTimer = 0.0f;
        }

        if (germinating && germinationTimer > 0.0f)
        {
            germinationTimer -= Time.deltaTime;

            rb.AddForce(Vector2.up * (3.0f + (jermaSpeed * jermaSpeedBalance)));

            if (germinationTimer <= 0.0f)
            {
                germinating = false;
                leafUIFill.fillAmount = 0f;
                germinationCDTimer = 15.0f - (2.0f * germinationCooldown);
            }
        }
    }

    void rocketUpdate()
    {
        // Rocket Time
        if (rocket && fuelTank > 0.0f && Input.GetButton("Fire1") && rb.simulated)
        {
            rb.AddForce(transform.up * (2.0f + rocketStrength * 1.0f));
            fuelTank -= Time.deltaTime / (1 + (fuelEfficency * fuelEffBalance));
        }
    }

    void playSoundOnce()
    {
        //soundWind,soundBounce,soundRocket,soundGerminate;

        // Bouncing sound
        if(grounded && !soundBounce)
        {
            AkSoundEngine.PostEvent("acorn_bounces", gameObject);
            soundBounce = true;
        }

        else if(!grounded && soundBounce)
        {
            soundBounce = false;
        }

        // Rocket sound
        if(rocket && fuelTank > 0f && Input.GetButton("Fire1") && !soundRocket && rb.simulated)
        {
            AkSoundEngine.StopAll(gameObject);
            AkSoundEngine.PostEvent("rocket_thrusters", gameObject);
            soundRocket = true;
            animator.SetBool("RocketFire", true);
        }

        else if(rocket && !Input.GetButton("Fire1") && soundRocket)
        {
            AkSoundEngine.StopAll(gameObject);
            soundRocket = false;
            animator.SetBool("RocketFire", false);
        }

        if(fuelTank <= 0f && soundRocket)
        {
            AkSoundEngine.StopAll(gameObject);
            soundRocket = false;
            animator.SetBool("RocketFire", false);
        }

        // Propeller sound
        if(germinationTimer > 0.0f && Input.GetButtonDown("Fire2") && !soundGerminate && rb.simulated)
        {
            AkSoundEngine.StopAll(gameObject);
            AkSoundEngine.PostEvent("sail_open", gameObject);
            soundGerminate = true;
            animator.SetBool("LeafSpin", true);
        }

        else if(germinationTimer < 0.0f && !Input.GetButtonDown("Fire2") && soundGerminate)
        {
            AkSoundEngine.StopAll(gameObject);
            soundGerminate = false;
            animator.SetBool("LeafSpin", false);
        }

        // Wind sound

        if(inWind && !soundWind)
        {
           AkSoundEngine.PostEvent("wind_gust_light", gameObject); 
           soundWind = true;
        }

        else if(!inWind && soundWind)
        {
            soundWind = false;
        }
    }

    void uiUpdate()
    {   
        // Update the leaf cooldown visual.
        if(germination)
            leafUIFill.fillAmount = ((15f - (2.0f * germinationCooldown)) - germinationCDTimer) / (15f - (2.0f * germinationCooldown));

        // Update the rocket cooldown visual.
        if(rocket)
            fuelUIFill.fillAmount = fuelTank / (3.0f + (1.0f * rocketFuel));

        // Update the distance text.
        distance.text = distanceBlurb + " " + toNearestTenth((transform.position.x - sampleStartPos.x)).ToString();

        // Update the height text.
        height.text = heightBlurb + " " + toNearestTenth((transform.position.y - sampleStartPos.y)).ToString();

        // Update the velocity text.
        velocity.text = velocityBlurb + " " + toNearestTenth(rb.velocity.x).ToString() + unitBlurb;


    }

    void clampUpdate()
    {
        // Clamp data!
        if(rb.velocity.y < (-velYClamp + (clamp * clampBalance)))
        {
            rb.velocity = new Vector2(rb.velocity.x, (-velYClamp + (clamp * clampBalance)));
        }
    }

    float toNearestTenth(float toRound)
    {
        return (float)(Mathf.RoundToInt((toRound * 10f)) / 10f);
    }
}
