using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_Script : MonoBehaviour
{
    public float strength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var particleSpeed = GetComponent<ParticleSystem>().main;
        var particleEmission = GetComponent<ParticleSystem>().emission;
        particleSpeed.startSpeedMultiplier = strength / 10.0f;
        particleSpeed.startLifetimeMultiplier = 6 / strength;
        particleEmission.rateOverTimeMultiplier = strength / 2;
    }
}
