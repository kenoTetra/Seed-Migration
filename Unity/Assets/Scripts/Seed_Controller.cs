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

    // Upgrades
    public int sail;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            mainWind.SetActive(true);
            GetComponent<Rigidbody2D>().simulated = true;
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

            if (GetComponent<Rigidbody2D>().angularVelocity < 100.0f && horizInput <= 0.0f)
            {
                GetComponent<Rigidbody2D>().AddTorque(-horizInput);
            }
            else if (GetComponent<Rigidbody2D>().angularVelocity > -100.0f && horizInput >= 0.0f)
            {
                GetComponent<Rigidbody2D>().AddTorque(-horizInput);
            }    
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Catches the wind
        if (collision.name == "Wind")
        {
            GetComponent<Rigidbody2D>().AddForce(collision.transform.right * collision.GetComponent<Wind_Script>().strength);

            // Calculates the sail effect
            if (grounded == false)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * (collision.GetComponent<Wind_Script>().strength * Vector3.Dot(transform.up, collision.transform.right)) * ((float)sail / sailModifier));
            }
        }
        else if (collision.name == "MainWind")
        {
            GetComponent<Rigidbody2D>().AddForce(collision.transform.right * collision.GetComponent<MainWind_Script>().strength);

            // Calculates the sail effect
            if (grounded == false)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * (collision.GetComponent<MainWind_Script>().strength * Vector3.Dot(transform.up, collision.transform.right)) * ((float)sail / sailModifier));
            }
        }
    }
}
