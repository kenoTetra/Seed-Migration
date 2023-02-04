using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed_Controller : MonoBehaviour
{
    public bool grounded;
    public RaycastHit2D downRay;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checks for colliders with the ground tag out .1 units from both sides of the player collider.
        downRay = Physics2D.Raycast(transform.position, -Vector2.up, 0.55f, groundLayer);

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
            transform.Rotate(new Vector3(0.0f, 0.0f, -Input.GetAxis("Horizontal")));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Wind")
        {
            GetComponent<Rigidbody2D>().AddForce(collision.transform.right * collision.GetComponent<Wind_Script>().strength);
        }
    }
}
