using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public Transform motorPosition;
    public float speed;
    public AnimationCurve accelerationCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));

    public float turnSpeed;
    public float tiltForce;

    private float rotation;
    private Rigidbody rb;
    private bool underWater;

    public GameObject turnHelper;
    public Floater floater;

    public float elapsedTime, elapsedTimeBack;

    public float dragUnder, dragOver;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        //prevent upside down
        rotation = Vector3.Angle(Vector3.up, transform.TransformDirection(Vector3.up));
        if (rotation > 70f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, transform.eulerAngles.y, 0f), 5f * Time.deltaTime);

        // Check if in water
        if (floater.underwater)
        {
            underWater = true;
            rb.drag = dragUnder;
        }
        else
        {
            underWater = false;
            rb.drag = dragOver;
        }


        if (underWater && turnHelper != null)
        {
            rb.AddTorque(transform.up * horizontal * 100f * turnSpeed * Time.deltaTime); //turning

            if (vertical > 0.1f)
            {
                float evaluatedCurve = accelerationCurve.Evaluate(elapsedTime);
                rb.AddForce(turnHelper.transform.forward * speed * evaluatedCurve * 0.05f * vertical * Time.deltaTime * 300f, ForceMode.Force);  //moving
                rb.AddTorque(transform.right * tiltForce * -vertical * Time.deltaTime, ForceMode.Force); //optional tilt 
            }
            if (vertical < -0.1f)
            {
                float evaluatedCurve = accelerationCurve.Evaluate(elapsedTimeBack);
                rb.AddForce(turnHelper.transform.forward * speed * evaluatedCurve * 0.02f * vertical * Time.deltaTime * 300f, ForceMode.Force);  //moving  
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        if (vertical <= 0f && elapsedTime > 0f)
        {
            elapsedTime -= Time.deltaTime;

        }
        if (vertical >= 0f && elapsedTimeBack > 0f)
        {

            elapsedTimeBack -= Time.deltaTime;
        }
        if (vertical >= 0.1f && elapsedTime < 1f)
            elapsedTime += Time.deltaTime;
        if (vertical <= -0.1f && elapsedTimeBack < 1f)
            elapsedTimeBack += Time.deltaTime;
    }
}
