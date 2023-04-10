using UnityEngine;

public static class GerstnerWaveDisplacement
{
    private static Vector3 GerstnerWave(Vector3 position, float steepness, float wavelength, float speed, float direction)
    {
        direction = (direction * 2 - 1);
        Vector2 d = new Vector2(Mathf.Cos(Mathf.PI * direction), Mathf.Sin(Mathf.PI * direction)).normalized;
        float k = 2 * Mathf.PI / wavelength;
        float a = steepness / k;
        float f = k * (Vector2.Dot(d, new Vector2(position.x, position.z)) - speed * Time.time);


        return new Vector3(d.x * a * Mathf.Cos(f), a * Mathf.Sin(f), d.y * a * Mathf.Cos(f));
    }

    private static Vector3 Ripple(Vector3 position, Vector3 playerPos, float cameraSize, Texture2D rippleTexture, float rippleHeight, Transform waterObject)
    {
       
        Vector2 a = new Vector2(position.x, position.z);
        Vector2 b = new Vector2(playerPos.x, playerPos.z);
        Vector2 uv = ((a - b) / (cameraSize * 2)) + new Vector2(0.5f, 0.5f);
        Vector3 final = new Vector3(waterObject.position.x, waterObject.position.y + (rippleTexture.GetPixelBilinear(uv.x, uv.y).g * rippleHeight), waterObject.position.z);
        

        return final;
    }

    public static Vector3 GetWaveDisplacement(Vector3 position, float steepness, float wavelength, float speed, Vector4 directions, Texture2D ripplesTexture,
        Vector3 playerPos, float cameraSize, float rippleHeight, Transform waterObject)
    {

        Vector3 offset = Vector3.zero;

        offset += GerstnerWave(position, steepness, wavelength, speed, directions.x);
        offset += GerstnerWave(position, steepness, wavelength, speed, directions.y);
        offset += GerstnerWave(position, steepness, wavelength, speed, directions.z);
        offset += GerstnerWave(position, steepness, wavelength, speed, directions.w);
        offset += Ripple(position, playerPos, cameraSize, ripplesTexture, rippleHeight, waterObject);

        

        return offset;



    }
}
