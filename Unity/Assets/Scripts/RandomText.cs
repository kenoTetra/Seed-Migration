using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomText : MonoBehaviour
{
    public string[] hints;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<TMP_Text>().text = hints[Random.Range(0, hints.Length)];
    }
}
