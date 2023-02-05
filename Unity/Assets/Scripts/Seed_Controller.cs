using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AK.Wwise;

public class Seed_Controller : MonoBehaviour
{
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

    // Upgrades
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

    // Powers
    public bool windPower;
    public bool leafPower;
    public bool pointPower;
    public bool fuelPower;

    // Hint Text
    public GameObject hintText;

    // UI elements
    public GameObject fuelUI;
    public GameObject leafUI;
    public Image fuelUIFill;
    public Image leafUIFill;

    // Sound trigger bools
    private bool soundWind,soundBounce,soundRocket,soundGerminate;
    private bool inWind;

    // References
    private Rigidbody2D rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // references
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // get upgrades
        sail = checkSetKeyInt("Sail") / 3f;
        weight = checkSetKeyInt("Mass");
        rocket = checkSetKeyBool("Rocket");
        rocketStrength = checkSetKeyInt("RocketSpeed") / 6f;
        rocketFuel = checkSetKeyInt("Fuel");
        germination = checkSetKeyBool("Germination");
        germinationDuration = checkSetKeyInt("Sugar");
        germinationCooldown = checkSetKeyInt("JermaCooldown");
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
    }

    // Update is called once per frame
    void Update()
    {
        playSoundOnce();

        if (Input.GetButtonDown("Jump"))
        {
            hintText.SetActive(false);
            mainWind.SetActive(true);
            AkSoundEngine.PostEvent("acorn_detach", gameObject);
            rb.simulated = true;
        }

        // Checks for colliders with the ground tag out .1 units from both sides of the player collider.
        downRay = Physics2D.Raycast(transform.position, -Vector2.up, 0.6f, groundLayer);

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

        // Rocket Time
        if (rocket && fuelTank > 0.0f && Input.GetButton("Fire1") && rb.simulated)
        {
            rb.AddForce(transform.up * (2.0f + rocketStrength * 1.0f));
            fuelTank -= Time.deltaTime;
        }

        // rocket ui visuals
        if (rocket)
        {
            fuelUIFill.fillAmount = fuelTank / (3.0f + (1.0f * rocketFuel));
        }

        // Jerma Nation
        if (germination)
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


                rb.AddForce(Vector2.up * 3.0f);

                if (germinationTimer <= 0.0f)
                {
                    germinating = false;
                    leafUIFill.fillAmount = 0f;
                    germinationCDTimer = 15.0f - (2.0f * germinationCooldown);
                }
            }

            leafUIFill.fillAmount = ((15f - (2.0f * germinationCooldown)) - germinationCDTimer) / (15f - (2.0f * germinationCooldown));
        }

        if(rb.velocity.y < -velYClamp)
        {
            rb.velocity = new Vector2(rb.velocity.x, -velYClamp);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
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
            scoreHandler.GetComponent<ScoreHandler>().scoreBonus += 50;
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
}
