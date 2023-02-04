using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Wind")
        {
            GetComponent<Rigidbody2D>().AddForce(collision.transform.right * 20.0f);
        }
    }
}
