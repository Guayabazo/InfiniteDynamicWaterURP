using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(WaterManager))]
public class Floater : MonoBehaviour
{
    public Transform[] floaters;
    private bool[] underWaterFloater;
    private bool[] wasUnderWaterFloater;
    public float floatingPower = 15f;
    WaterManager waterManager;
    Rigidbody rb;

    public bool underwater;
    public bool wasOnWater;

    public ParticleSystem[] inOut;
    public ParticleSystem[] moving;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        waterManager = GetComponent<WaterManager>();
        wasUnderWaterFloater = new bool[floaters.Length];
        underWaterFloater = new bool[floaters.Length];
        
    }

    void FixedUpdate()
    {
        if (waterManager.oceanMat != null)
        {
            Vector2 vel = new(rb.velocity.x, rb.velocity.z);

            for (int i = 0; i < floaters.Length; i++)
            {
                ParticleSystem.EmissionModule movingEmission;
                if (moving[i]!= null)                
                    movingEmission = moving[i].emission;
                
                float difference = floaters[i].position.y - waterManager.WaterHeightAtPosition(floaters[i].position);
                if (difference < 0)
                {
                    underwater = true;
                    underWaterFloater[i] = true;
                    
                    float submersion = Mathf.Clamp01(Mathf.Abs(difference));
                    rb.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(submersion), floaters[i].position, ForceMode.Force);

                    if (moving[i] != null && !movingEmission.enabled && vel.magnitude > 0.2f)
                        movingEmission.enabled = true;
                }
                else
                {
                    underWaterFloater[i] = false;
                    underwater = false;
                    for (int j = 0; j < floaters.Length; j++)
                    {
                        if (underWaterFloater[j])
                        {
                            underwater = true;
                            break;
                        }
                    }

                    

                    if (moving[i] != null && movingEmission.enabled)
                    {
                        movingEmission.enabled = false;
                        
                    }

                } 

                if (vel.magnitude <= 0.2f && moving[i] != null && movingEmission.enabled)
                {
                    movingEmission.enabled = false;

                }

                if (underWaterFloater[i] != wasUnderWaterFloater[i] && inOut[i]!= null)
                {
                    ParticleSystem.MainModule displacementPar = inOut[i].main;
                    float colorMultiplier = Mathf.Abs(rb.velocity.y * 0.1f);
                    colorMultiplier = Mathf.Clamp(colorMultiplier, 0f, 0.75f);
                    displacementPar.startColor = Color.white * colorMultiplier;
                    inOut[i].Play();
                }

                

                wasUnderWaterFloater[i] = underWaterFloater[i];


            }

        }



    }
}
