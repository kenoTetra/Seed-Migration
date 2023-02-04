using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWind_Script : MonoBehaviour
{
    public float strength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Deprecates the strength of wind over time
        strength = strength / (1.0f + Time.deltaTime * 0.375f);

        // Gets the particle emitter variables
        var particleSpeed = GetComponent<ParticleSystem>().main;
        var particleEmission = GetComponent<ParticleSystem>().emission;

        if (strength >= 0.25f)
        {
            particleSpeed.startSpeedMultiplier = strength / 10.0f;
            particleSpeed.startLifetimeMultiplier = 6 / strength;
            particleEmission.rateOverTimeMultiplier = strength / 2;
        }
        else
        {
            strength = 0.0f;
            particleEmission.rateOverTimeMultiplier = 0;
        }
    }
}
