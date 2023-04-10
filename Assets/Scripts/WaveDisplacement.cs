using UnityEngine;

public static class WaveDisplacement
{
    private static Vector3 SineWaves(Vector3 position,
        float waveFrequency, float waveFrequencyB, float waveFrequencyC, float waveFrequencyD,
        float waveAmplitude, float waveAmplitudeB, float waveAmplitudeC, float waveAmplitudeD,
        float waveHeight, float waveHeightB, float waveHeightC, float waveHeightD,
        float waveCDirectionX, float waveCDirectionZ, float waveDDirectionX, float waveDDirectionZ)
    {
        float a = (Mathf.Sin((position.x * waveAmplitude) + (Time.time * waveFrequency))) * waveHeight;
        float b = (Mathf.Sin((position.z * waveAmplitudeB) + (Time.time * waveFrequencyB))) * waveHeightB;
        float inter = (position.x * waveCDirectionX) + (position.z * waveCDirectionZ);
        float c = (Mathf.Sin((inter * waveAmplitudeC) + (Time.time * waveFrequencyC))) * waveHeightC;

        float interD = (position.x * waveDDirectionX) + (position.z * waveDDirectionZ);
        float d = (Mathf.Sin((interD * waveAmplitudeD) + (Time.time * waveFrequencyD))) * waveHeightD;
        Vector3 prev = new Vector3(position.x, a + b + c + d, position.z);


        return prev;
    }

    private static Vector3 Ripple(Vector3 position, Vector3 playerPos, float cameraSize, Texture2D rippleTexture, float rippleHeight, Transform waterObject)
    {

        Vector2 a = new Vector2(position.x, position.z);
        Vector2 b = new Vector2(playerPos.x, playerPos.z);
        Vector2 uv = ((a - b) / (cameraSize * 2)) + new Vector2(0.5f, 0.5f);
        Vector3 final = new Vector3(waterObject.position.x, waterObject.position.y + (rippleTexture.GetPixelBilinear(uv.x, uv.y).g * rippleHeight), waterObject.position.z);


        return final;
    }

    public static Vector3 GetWaveHeight(Vector3 position,
        float waveFrequency, float waveFrequencyB, float waveFrequencyC, float waveFrequencyD,
        float waveAmplitude, float waveAmplitudeB, float waveAmplitudeC, float waveAmplitudeD,
        float waveHeight, float waveHeightB, float waveHeightC, float waveHeightD,
        Vector3 playerPos, float cameraSize, Texture2D rippleTexture, float rippleHeight, Transform waterObject,
        float waveCDirectionX, float waveCDirectionZ, float waveDDirectionX, float waveDDirectionZ)
    {
        Vector3 offset = Vector3.zero;
        offset += SineWaves(position,
            waveFrequency, waveFrequencyB, waveFrequencyC, waveFrequencyD,
            waveAmplitude, waveAmplitudeB, waveAmplitudeC, waveAmplitudeD,
            waveHeight, waveHeightB, waveHeightC, waveHeightD,
            waveCDirectionX, waveCDirectionZ, waveDDirectionX, waveDDirectionZ); 

        offset += Ripple(position, playerPos, cameraSize, rippleTexture, rippleHeight, waterObject);

        //Vector3 sineWave = SineWaves(position, waveFrequency, waveFrequencyB, waveAmplitude, waveAmplitudeB, waveHeight, waveHeightB);
        //Vector3 ripple = Ripple(position, playerPos, cameraSize, rippleTexture, rippleHeight, waterObject);


        return offset;



    }
}
