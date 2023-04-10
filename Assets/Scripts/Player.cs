using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject rippleCamera;
    
    // Update is called once per frame
    void Update()
    {
        rippleCamera.transform.position = transform.position + Vector3.up * 10;
        Shader.SetGlobalVector("_PlayerPos", rippleCamera.transform.position);
        //ripples.Emit(transform.position + transform.forward, transform.forward, 2, 3, Color.white);
    }
}
