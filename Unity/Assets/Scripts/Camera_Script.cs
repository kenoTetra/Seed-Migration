using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    public GameObject seed;
    public float xOffset;
    public float yOffset;
    public GameObject background;

    public float minX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        minX = Mathf.Max(seed.transform.position.x, minX);
        transform.position = new Vector3(minX + xOffset, Mathf.Max(seed.transform.position.y - yOffset, 0), -10.0f);

        background.transform.localPosition = new Vector3(0.0f, Mathf.Clamp(60.0f - transform.position.y / 2.0f, -60.0f, 60.0f), 10.0f);
    }
}
