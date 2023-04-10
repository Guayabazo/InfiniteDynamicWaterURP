using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    
    public Material oceanMat;
    public float waterHeight;
    public Texture2D ripples;
    public RenderTexture ripplesTexture;
    public Vector3 playerPos;
    float cameraSize;
    float rippleHeight;
    public Transform waterObject;

    //SineWaves
    private float waveFrequency, waveFrequencyB, waveFrequencyC, waveFrequencyD,
        waveAmplitude, waveAmplitudeB, waveAmplitudeC, waveAmplitudeD,
        waveHeight, waveHeightB, waveHeightC, waveHeightD,
        waveCDirectionX, waveCDirectionZ, waveDDirectionX, waveDDirectionZ;

    void Start()
    {
        //ocean = null;
        SetVariables();
    }

    void SetVariables()
    {

        
        if (oceanMat != null)
        {

            
            //Ripples            
            playerPos =Shader.GetGlobalVector("_PlayerPos");
            cameraSize = oceanMat.GetFloat("_CameraSize");
            rippleHeight = oceanMat.GetFloat("_RippleHeight");


            //SineWaves
            waveFrequency = oceanMat.GetFloat("_WaveFrequency");
            waveFrequencyB = oceanMat.GetFloat("_WaveFrequencyB");
            waveFrequencyC = oceanMat.GetFloat("_WaveFrequencyC");
            waveFrequencyD = oceanMat.GetFloat("_WaveFrequencyD");
            waveAmplitude = oceanMat.GetFloat("_WaveAmplitude");
            waveAmplitudeB = oceanMat.GetFloat("_WaveAmplitudeB");
            waveAmplitudeC = oceanMat.GetFloat("_WaveAmplitudeC");
            waveAmplitudeD = oceanMat.GetFloat("_WaveAmplitudeD");
            waveHeight = oceanMat.GetFloat("_WaveHeight");
            waveHeightB = oceanMat.GetFloat("_WaveHeightB");
            waveHeightC = oceanMat.GetFloat("_WaveHeightC");
            waveHeightD = oceanMat.GetFloat("_WaveHeightD");
            waveCDirectionX = oceanMat.GetFloat("_WaveCDirectionX");
            waveCDirectionZ = oceanMat.GetFloat("_WaveCDirectionZ");
            waveDDirectionX = oceanMat.GetFloat("_WaveDDirectionX");
            waveDDirectionZ = oceanMat.GetFloat("_WaveDDirectionZ");



        }
        
        

    }

    Texture2D ToTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        //tex.Apply();
        return tex;
    }


    public void ChangeOcean(Material thisWater)
    {
        oceanMat = thisWater;
        SetVariables();
    }

    public float WaterHeightAtPosition(Vector3 position)
    {        
        ripples = WaterData.instance.ripples;
        playerPos = WaterData.instance.playerPos;

        //Gerstner waves, waterMat1
        //return waterHeight + (GerstnerWaveDisplacement.GetWaveDisplacement(position, steepness, wavelength, speed, directions, ripples, 
        //    playerPos, cameraSize, rippleHeight, waterObject).y);

        return waterHeight + WaveDisplacement.GetWaveHeight(position,
            waveFrequency, waveFrequencyB, waveFrequencyC, waveFrequencyD,
            waveAmplitude, waveAmplitudeB, waveAmplitudeC, waveAmplitudeD,
            waveHeight, waveHeightB, waveHeightC, waveHeightD, 
            playerPos, cameraSize, ripples, rippleHeight, waterObject, 
            waveCDirectionX, waveCDirectionZ, waveDDirectionX, waveDDirectionZ).y;
    }
}
