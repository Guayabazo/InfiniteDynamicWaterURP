using UnityEngine;

public class WaterData : MonoBehaviour
{
    public static WaterData instance;
    [HideInInspector]public Texture2D ripples;
    public RenderTexture ripplesTexture;
    public Vector3 playerPos;
    private void Awake()
    {
        instance = this;
        ripples = ToTexture2D(ripplesTexture);
        playerPos = Shader.GetGlobalVector("_PlayerPos");
    }
    
    
    // Update is called once per frame
    void FixedUpdate()
    {
        ripples = ToTexture2D(ripplesTexture);
        playerPos = Shader.GetGlobalVector("_PlayerPos");
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
}
