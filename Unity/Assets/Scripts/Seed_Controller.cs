using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Upgrades
    public int sail;
    public int gravity;
    public int weight;
    public int wind;
    public bool rocket;
    public int rocketStrength;
    public int rocketFuel;
    public bool germination;
    public int germinationDuration;
    public int germinationCooldown;

    // Hint Text
    public GameObject hintText;

    // References
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.5f * (1.0f - 0.125f * gravity);
        rb.mass = 2.0f * (1.0f - 0.0625f * weight);

        fuelTank = 5.0f + (5.0f * rocketFuel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            hintText.SetActive(false);
            mainWind.SetActive(true);
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

        // Rocket Time
        if (rocket && fuelTank > 0.0f && Input.GetButton("Fire1"))
        {
            rb.AddForce(transform.up * (2.0f + rocketStrength * 1.0f));
            fuelTank -= Time.deltaTime;
        }

        // Jerma Nation
        if (germination)
        {
            if (germinationCDTimer > 0.0f)
            {
                germinationCDTimer -= Time.deltaTime;
            }
            else if (germinationTimer > 0.0f && Input.GetButtonDown("Fire2"))
            {
                germinating = true;
            }
            else if (germinating == false)
            {
                germinationTimer = 1.0f + (1.0f * germinationCooldown);
                germinationCDTimer = 0.0f;
            }

            if (germinating && germinationTimer > 0.0f)
            {
                germinationTimer -= Time.deltaTime;

                rb.AddForce(Vector2.up * 2.0f);

                if (germinationTimer <= 0.0f)
                {
                    germinating = false;
                    germinationCDTimer = 15.0f - (2.0f * germinationCooldown);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Catches the wind
        if (collision.name == "Wind(Clone)")
        {
            rb.AddForce(collision.transform.right * collision.GetComponent<Wind_Script>().strength * (1.0f + windModifier * (float)wind));

            // Calculates the sail effect
            if (grounded == false)
            {
                rb.AddForce(transform.up * (collision.GetComponent<Wind_Script>().strength * Vector3.Dot(transform.up, collision.transform.right)) * ((float)sail * sailModifier));
            }
        }
        else if (collision.name == "MainWind")
        {
            rb.AddForce(collision.transform.right * collision.GetComponent<MainWind_Script>().strength * (1.0f + windModifier * (float)wind));

            // Calculates the sail effect
            if (grounded == false)
            {
                rb.AddForce(transform.up * (collision.GetComponent<MainWind_Script>().strength * Vector3.Dot(transform.up, collision.transform.right)) * ((float)sail * sailModifier));
            }
        }
    }
}
