﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class rotateL2 : MonoBehaviour
{

    public float zangle = 0f;
    public float zanglePrev = 0f;

    float currentAngle = 0f;

    float prevAangle = 0f;
    public GameObject GameObject;
    private JohnArduinoManager Arduino;
    int detach = 0;
    int counter = 0;

    float Aread = 0f;
    float Aangle = 0f;

    public float[] potVal = new float[2];
    float diff;
    int limit = 20;

    public float position = 0f;

    public float writeAngle;


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
        Aread = Arduino.currentVals[5];

        Aangle = (Aread - 255) * (-0.53f);

        float AangleChange = Aangle - prevAangle;

        currentAngle += AangleChange;

        if (counter < 6)
        {
            potVal[0] = 20;
            potVal[1] = 20;
            counter++;

        }
        else if (counter == 6)
        {
            potVal[0] = Aangle;
            potVal[1] = Aangle;

            counter++;
        }
        else
        {

            potVal[1] = potVal[0];
            potVal[0] = Aangle;
        }

        diff = Math.Abs(potVal[1] - potVal[0]);

        if (diff > limit)
        {
            potVal[0] = potVal[1];
        }

        if (Input.GetKeyDown("d"))
        {
            detach++;
            detach = detach % 2;
        }

        if (detach == 1)
        {
            keyRotate();
        }
        else if (detach == 0)
        {
            //bendRotate();
            zangle = currentAngle;
            transform.Rotate(0, 0, AangleChange, Space.Self);

        }

        //print("zLeft: " + zangle);
        //print("aLeft: " + Aangle);
        //print("pot1: " + potVal[0]);

        //zanglePrev = potVal[0];
        writeAngle = zangle - 20;
        //zanglePrev = zangle;

        if (Input.GetKeyDown("z"))
        {
            Quaternion zero = Quaternion.Euler(0, 0, 0);

            transform.localRotation = zero;
            currentAngle = 0;



        }

        prevAangle = Aangle;

    }

    void keyRotate()
    {
        //zangle = potVal[1];

        if (Input.GetKey("left"))
        {
            if (zangle >= -95 && zangle <= 90)
            {
                zangle++;
            }
        }
        if (Input.GetKey("right"))
        {
            if (zangle >= -90 && zangle <= 95)
            {
                zangle--;
            }
        }

        float potChange = potVal[0] - potVal[1];
        transform.Rotate(0, 0, potChange, Space.Self);

    }

    void bendRotate()
    {

        zangle = currentAngle;
        //zangle = potVal[0];
        float zangleChange = zangle - zanglePrev;

        transform.Rotate(0, 0, zangleChange, Space.Self);
        print("L1 Aangle: " + Aangle);

        //float potChange = potVal[0] - potVal[1];
        //transform.Rotate(0, 0, potChange, Space.Self);



        //if (Input.GetKeyDown("z"))
        //{
        //    Quaternion zero = Quaternion.Euler(0, 0, 0);

        //    transform.localRotation = zero;

        //}
        zanglePrev = Aangle;
    }
}
