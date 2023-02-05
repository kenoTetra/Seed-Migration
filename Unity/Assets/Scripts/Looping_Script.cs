using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looping_Script : MonoBehaviour
{
    public GameObject cameraObject;
    public GameObject seed;

    public GameObject groundPrefab;
    public GameObject[] ground;

    public GameObject windPrefab;

    public GameObject windPower;
    public GameObject leafPower;
    public GameObject pointPower;
    public GameObject fuelPower;

    // Start is called before the first frame update
    void Start()
    {
        CreateWindPrefab(2, false);
        CreateWindPrefab(2, true);
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

            CreateWindPrefab(0, true);
            CreateWindPrefab(0, false);
        }
        // Continues to the right
        else if (ground[1].transform.position.x < cameraObject.transform.position.x - 12.5f)
        {
            Destroy(ground[0]);
            ground[0] = ground[1];
            ground[1] = ground[2];
            ground[2] = Instantiate(groundPrefab, new Vector3(ground[1].transform.position.x + 25.0f, -5.0f, 0.0f), Quaternion.identity);

            CreateWindPrefab(2, false);
            CreateWindPrefab(2, true);
        }
    }

    void CreateWindPrefab(int groundIndex, bool right)
    {
        GameObject newPrefab;

        if (right)
        {
            float incHeight = 2.0f;

            newPrefab = Instantiate(windPrefab, new Vector3(ground[groundIndex].transform.position.x + Random.Range(0.5f, 12.0f), Random.Range(-3.0f, incHeight), 0.0f), Quaternion.Euler(0.0f, 0.0f, Random.Range(-10.0f, 35.0f)));
            newPrefab.GetComponent<Wind_Script>().strength = Random.Range(4.0f, 15.0f);
            newPrefab.GetComponent<Wind_Script>().cameraObject = cameraObject;
            newPrefab.transform.localScale = new Vector3(Random.Range(2.0f, 10.0f), Random.Range(0.75f, 4.0f), 1.0f);

            // Makes more wind currents upwards
            do
            {
                newPrefab = Instantiate(windPrefab, new Vector3(ground[groundIndex].transform.position.x + Random.Range(0.5f, 12.0f), Random.Range(incHeight, incHeight + 8.0f), 0.0f), Quaternion.Euler(0.0f, 0.0f, Random.Range(-15.0f, 50.0f)));
                newPrefab.GetComponent<Wind_Script>().strength = Random.Range(4.0f, 15.0f);
                newPrefab.GetComponent<Wind_Script>().cameraObject = cameraObject;
                newPrefab.transform.localScale = new Vector3(Random.Range(2.0f, 10.0f), Random.Range(0.75f, 4.0f), 1.0f);
                incHeight += 8.0f;
            } while (incHeight <= cameraObject.transform.position.y + 12.0f);

            if (seed.GetComponent<Seed_Controller>().windPower && Random.Range(0.0f, 1.0f) >= 0.2f)
            {
                newPrefab = Instantiate(windPower, new Vector2(ground[groundIndex].transform.position.x + Random.Range(-12.5f, 12.5f), Mathf.Max(cameraObject.transform.position.y + Random.Range(-10.0f, 10.0f), -2.0f)), Quaternion.identity);
                newPrefab.GetComponent<WindPowerScript>().cameraObject = cameraObject;
            }
            if (seed.GetComponent<Seed_Controller>().leafPower && Random.Range(0.0f, 1.0f) >= 0.2f)
            {
                newPrefab = Instantiate(leafPower, new Vector2(ground[groundIndex].transform.position.x + Random.Range(-12.5f, 12.5f), Mathf.Max(cameraObject.transform.position.y + Random.Range(-10.0f, 10.0f), -2.0f)), Quaternion.identity);
                newPrefab.GetComponent<WorldObjectDestroy_Script>().cameraObject = cameraObject;
            }
            if (seed.GetComponent<Seed_Controller>().pointPower && Random.Range(0.0f, 1.0f) >= 0.2f)
            {
                newPrefab = Instantiate(pointPower, new Vector2(ground[groundIndex].transform.position.x + Random.Range(-12.5f, 12.5f), Mathf.Max(cameraObject.transform.position.y + Random.Range(-10.0f, 10.0f), -2.0f)), Quaternion.identity);
                newPrefab.GetComponent<WorldObjectDestroy_Script>().cameraObject = cameraObject;
            }
            if (seed.GetComponent<Seed_Controller>().pointPower && Random.Range(0.0f, 1.0f) >= 0.2f)
            {
                newPrefab = Instantiate(fuelPower, new Vector2(ground[groundIndex].transform.position.x + Random.Range(-12.5f, 12.5f), Mathf.Max(cameraObject.transform.position.y + Random.Range(-10.0f, 10.0f), -2.0f)), Quaternion.identity);
                newPrefab.GetComponent<WorldObjectDestroy_Script>().cameraObject = cameraObject;
            }
        }
        else
        {
            float incHeight = 2.0f;

            newPrefab = Instantiate(windPrefab, new Vector3(ground[groundIndex].transform.position.x - Random.Range(0.5f, 12.0f), Random.Range(-3.0f, incHeight), 0.0f), Quaternion.Euler(0.0f, 0.0f, Random.Range(-10.0f, 35.0f)));
            newPrefab.GetComponent<Wind_Script>().strength = Random.Range(2.0f, 15.0f);
            newPrefab.GetComponent<Wind_Script>().cameraObject = cameraObject;
            newPrefab.transform.localScale = new Vector3(Random.Range(2.0f, 10.0f), Random.Range(0.75f, 4.0f), 1.0f);

            do
            {
                newPrefab = Instantiate(windPrefab, new Vector3(ground[groundIndex].transform.position.x - Random.Range(0.5f, 12.0f), Random.Range(incHeight, incHeight + 8.0f), 0.0f), Quaternion.Euler(0.0f, 0.0f, Random.Range(-15.0f, 50.0f)));
                newPrefab.GetComponent<Wind_Script>().strength = Random.Range(4.0f, 15.0f);
                newPrefab.GetComponent<Wind_Script>().cameraObject = cameraObject;
                newPrefab.transform.localScale = new Vector3(Random.Range(2.0f, 10.0f), Random.Range(0.75f, 4.0f), 1.0f);
                incHeight += 8.0f;
            } while (incHeight <= cameraObject.transform.position.y);
        }
    }
}
