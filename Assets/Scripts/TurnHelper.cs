using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHelper : MonoBehaviour
{
    public Transform objectToTurn;
    public float aimSpeed = 12f;


    // Update is called once per frame
    void LateUpdate()
    {
        if (objectToTurn != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, objectToTurn.rotation, aimSpeed * Time.deltaTime);
        }
    }
}
