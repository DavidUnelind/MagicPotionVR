using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    Renderer rend;
    Vector3 lastPos;
    Vector3 velocity;
    Vector3 lastRot;  
    Vector3 angularVelocity;
    public float MaxWobble;
    public float WobbleSpeed;
    public float Recovery;
    
    float wobbleAmountX;
    float wobbleAmountZ;
    float wobbleAmountToAddX;
    float wobbleAmountToAddZ;
    float pulse;
    float time = 0.5f;

    float scaleFactor;
    float scaledWobble;


    // fill settings

    public bool isDraining = false;
    private float startDrainRate = 0.2f;
    public float drainRate = 0.2f;
    public float fillLevel = 0.2f;
    
    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();

        scaleFactor = transform.lossyScale.x;
        scaledWobble = MaxWobble * scaleFactor;

        rend.material.SetFloat("_Fill", fillLevel);
    }
    private void Update()
    {
        time += Time.deltaTime;
        // decrease wobble over time
        wobbleAmountToAddX = Mathf.Lerp(wobbleAmountToAddX, 0, Time.deltaTime * (Recovery));
        wobbleAmountToAddZ = Mathf.Lerp(wobbleAmountToAddZ, 0, Time.deltaTime * (Recovery));

        // make a sine wave of the decreasing wobble
        pulse = 2 * Mathf.PI * WobbleSpeed;
        wobbleAmountX = wobbleAmountToAddX * Mathf.Sin(pulse * time);
        wobbleAmountZ = wobbleAmountToAddZ * Mathf.Sin(pulse * time);

        // send it to the shader
        rend.material.SetFloat("_WobbleX", wobbleAmountX);
        rend.material.SetFloat("_WobbleZ", wobbleAmountZ);

        // velocity
        velocity = (lastPos - transform.position) / Time.deltaTime;
        angularVelocity = transform.rotation.eulerAngles - lastRot;


        // add clamped velocity to wobble
        wobbleAmountToAddX += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * scaledWobble, -scaledWobble, scaledWobble);
        wobbleAmountToAddZ += Mathf.Clamp((velocity.z + (angularVelocity.x * 0.2f)) * scaledWobble, -scaledWobble, scaledWobble);

        // keep last position
        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;


        // empty bottle
        if (isDraining && fillLevel > -0.5f)
        {
            //drainRate += 0.1f;
            fillLevel = Mathf.Clamp(fillLevel - drainRate * Time.deltaTime, -0.5f, 1f);
            rend.material.SetFloat("_Fill", fillLevel);
        } 
    }

    public void SetFillLevel(float level)
    {
        fillLevel = level; 
        rend.material.SetFloat("_Fill", level);
    }
}