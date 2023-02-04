using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looping_Script : MonoBehaviour
{
    public GameObject cameraObject;

    public GameObject groundPrefab;
    public GameObject[] ground;

    public GameObject windPrefab;
    public GameObject[] windObjects;

    // Start is called before the first frame update
    void Start()
    {
        windObjects[4] = CreateWindPrefab(2, false);
        windObjects[5] = CreateWindPrefab(2, true);
    }

    // Update is called once per frame
    void Update()
    {
        // Continues to the left
        if (ground[1].transform.position.x > cameraObject.transform.position.x + 12.5f)
        {
            Destroy(ground[2]);
            ground[2] = ground[1];
            ground[1] = ground[0];
            ground[0] = Instantiate(groundPrefab, new Vector3(ground[1].transform.position.x - 25.0f, -5.0f, 0.0f), Quaternion.identity);

            Destroy(windObjects[4]);
            Destroy(windObjects[5]);
            windObjects[5] = windObjects[3];
            windObjects[4] = windObjects[2];
            windObjects[3] = windObjects[1];
            windObjects[2] = windObjects[0];
            windObjects[1] = CreateWindPrefab(0, true);
            windObjects[0] = CreateWindPrefab(0, false);
        }
        // Continues to the right
        else if (ground[1].transform.position.x < cameraObject.transform.position.x - 12.5f)
        {
            Destroy(ground[0]);
            ground[0] = ground[1];
            ground[1] = ground[2];
            ground[2] = Instantiate(groundPrefab, new Vector3(ground[1].transform.position.x + 25.0f, -5.0f, 0.0f), Quaternion.identity);

            Destroy(windObjects[0]);
            Destroy(windObjects[1]);
            windObjects[0] = windObjects[2];
            windObjects[1] = windObjects[3];
            windObjects[2] = windObjects[4];
            windObjects[3] = windObjects[5];
            windObjects[4] = CreateWindPrefab(2, false);
            windObjects[5] = CreateWindPrefab(2, true);
        }
    }

    GameObject CreateWindPrefab(int groundIndex, bool right)
    {
        GameObject newPrefab;

        if (right)
        {
            newPrefab = Instantiate(windPrefab, new Vector3(ground[groundIndex].transform.position.x + Random.Range(0.5f, 12.0f), Random.Range(-3.0f, 4.0f), 0.0f), Quaternion.Euler(0.0f, 0.0f, Random.Range(-10.0f, 35.0f)));
            newPrefab.GetComponent<Wind_Script>().strength = Random.Range(8.0f, 40.0f);
            newPrefab.transform.localScale = new Vector3(Random.Range(2.0f, 10.0f), Random.Range(0.75f, 4.0f), 1.0f);
        }
        else
        {
            newPrefab = Instantiate(windPrefab, new Vector3(ground[groundIndex].transform.position.x - Random.Range(0.5f, 12.0f), Random.Range(-3.0f, 4.0f), 0.0f), Quaternion.Euler(0.0f, 0.0f, Random.Range(-10.0f, 35.0f)));
            newPrefab.GetComponent<Wind_Script>().strength = Random.Range(8.0f, 40.0f);
            newPrefab.transform.localScale = new Vector3(Random.Range(2.0f, 10.0f), Random.Range(0.75f, 4.0f), 1.0f);
        }

        return newPrefab;
    }
}
