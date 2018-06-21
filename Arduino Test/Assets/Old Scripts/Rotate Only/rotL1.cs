using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotL1 : MonoBehaviour
{

    private float zAngle = 0f;
    private float zAnglePrev = 0f;
    private float bendValue = 0f;
    private JohnArduinoManager Arduino;

    public GameObject GameObject;


    void Awake()
    {
        Arduino = GameObject.GetComponent<JohnArduinoManager>();

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bendValue = Arduino.currentVals[3];
        RotateObject(bendValue);
    }


    void RotateObject(float Value)
    {
        zAngle = (Value - 255) * (-0.53f);
        float zAngleChange = zAngle - zAnglePrev;


        transform.Rotate(0, 0, zAngleChange, Space.Self);
        if (Input.GetKeyDown("z"))
        {
            Quaternion zero = Quaternion.Euler(0, 0, 0);

            transform.localRotation = zero;

        }

        zAnglePrev = zAngle;
    }
}