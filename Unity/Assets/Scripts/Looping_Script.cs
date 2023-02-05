using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looping_Script : MonoBehaviour
{
    public GameObject cameraObject;

    public GameObject groundPrefab;
    public GameObject[] ground;

    public GameObject windPrefab;
    public List<GameObject> windObjectsLeft;
    public List<GameObject> windObjectsCenter;
    public List<GameObject> windObjectsRight;

    // Start is called before the first frame update
    void Start()
    {
        windObjectsRight.AddRange(CreateWindPrefab(2, false));
        windObjectsRight.AddRange(CreateWindPrefab(2, true));
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

            windObjectsRight.Clear();
            windObjectsRight = windObjectsCenter;
            windObjectsCenter = windObjectsLeft;
            windObjectsLeft.Clear();
            windObjectsLeft.AddRange(CreateWindPrefab(0, true));
            windObjectsLeft.AddRange(CreateWindPrefab(0, false));
        }
        // Continues to the right
        else if (ground[1].transform.position.x < cameraObject.transform.position.x - 12.5f)
        {
            Destroy(ground[0]);
            ground[0] = ground[1];
            ground[1] = ground[2];
            ground[2] = Instantiate(groundPrefab, new Vector3(ground[1].transform.position.x + 25.0f, -5.0f, 0.0f), Quaternion.identity);

            windObjectsLeft.Clear();
            windObjectsLeft = windObjectsCenter;
            windObjectsCenter = windObjectsRight;
            windObjectsRight.Clear();
            windObjectsRight.AddRange(CreateWindPrefab(2, false));
            windObjectsRight.AddRange(CreateWindPrefab(2, true));
        }
    }

    List<GameObject> CreateWindPrefab(int groundIndex, bool right)
    {
        List<GameObject> prefabList = null;
        GameObject newPrefab;

        if (right)
        {
            float incHeight = 2.0f;

            newPrefab = Instantiate(windPrefab, new Vector3(ground[groundIndex].transform.position.x + Random.Range(0.5f, 12.0f), Random.Range(-3.0f, incHeight), 0.0f), Quaternion.Euler(0.0f, 0.0f, Random.Range(-10.0f, 35.0f)));
            newPrefab.GetComponent<Wind_Script>().strength = Random.Range(4.0f, 15.0f);
            newPrefab.transform.localScale = new Vector3(Random.Range(2.0f, 10.0f), Random.Range(0.75f, 4.0f), 1.0f);
            prefabList.Add(newPrefab);

            // Makes more wind currents upwards
            do
            {
                Instantiate(windPrefab, new Vector3(ground[groundIndex].transform.position.x + Random.Range(0.5f, 12.0f), Random.Range(incHeight, incHeight + 8.0f), 0.0f), Quaternion.Euler(0.0f, 0.0f, Random.Range(-15.0f, 50.0f)));
                newPrefab.GetComponent<Wind_Script>().strength = Random.Range(4.0f, 15.0f);
                newPrefab.transform.localScale = new Vector3(Random.Range(2.0f, 10.0f), Random.Range(0.75f, 4.0f), 1.0f);
                incHeight += 8.0f;
            } while (incHeight <= cameraObject.transform.position.y + 12.0f);
        }
        else
        {
            float incHeight = 2.0f;

            newPrefab = Instantiate(windPrefab, new Vector3(ground[groundIndex].transform.position.x - Random.Range(0.5f, 12.0f), Random.Range(-3.0f, incHeight), 0.0f), Quaternion.Euler(0.0f, 0.0f, Random.Range(-10.0f, 35.0f)));
            newPrefab.GetComponent<Wind_Script>().strength = Random.Range(2.0f, 15.0f);
            newPrefab.transform.localScale = new Vector3(Random.Range(2.0f, 10.0f), Random.Range(0.75f, 4.0f), 1.0f);

            do
            {
                Instantiate(windPrefab, new Vector3(ground[groundIndex].transform.position.x - Random.Range(0.5f, 12.0f), Random.Range(incHeight, incHeight + 8.0f), 0.0f), Quaternion.Euler(0.0f, 0.0f, Random.Range(-15.0f, 50.0f)));
                newPrefab.GetComponent<Wind_Script>().strength = Random.Range(4.0f, 15.0f);
                newPrefab.transform.localScale = new Vector3(Random.Range(2.0f, 10.0f), Random.Range(0.75f, 4.0f), 1.0f);
                incHeight += 8.0f;
            } while (incHeight <= cameraObject.transform.position.y);
        }

        return prefabList;
    }
}
