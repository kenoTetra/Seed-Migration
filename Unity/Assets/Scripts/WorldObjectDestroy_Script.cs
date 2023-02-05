using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectDestroy_Script : MonoBehaviour
{
    public GameObject cameraObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x + 25.0f <= cameraObject.transform.position.x || transform.position.y + 25f <= cameraObject.transform.position.y)
        {
            Destroy(this.gameObject);
        }
    }
}
