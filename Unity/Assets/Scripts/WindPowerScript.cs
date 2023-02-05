using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPowerScript : MonoBehaviour
{
    public GameObject windCurrent;
    public GameObject cameraObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x + 25.0f <= cameraObject.transform.position.x)
        {
            Destroy(this);
        }
    }

    public void Activate()
    {
        GameObject newWind = Instantiate(windCurrent, new Vector2(transform.position.x + 4.0f, transform.position.y + 2.0f), Quaternion.Euler(0.0f, 0.0f, 30.0f));
        newWind.GetComponent<Wind_Script>().strength = 30.0f;
        newWind.transform.localScale = new Vector2(8.0f, 12.0f);

        Destroy(this);
    }
}
