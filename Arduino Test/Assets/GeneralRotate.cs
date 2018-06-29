using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class GeneralRotate : MonoBehaviour
{
    public bool print;
    public bool negative;
    public bool servo1n2;
    public bool servo3n4;
    public bool servo5n6;
    public bool servo7n8;
    public bool servo9n10;
    public bool servo11n12;

    int negPos;
    public int inputValue;
    public float convOffset = 255;
    public float convMultiplyer = -0.6f;
    public float zangle = 0f;
    public float zanglePrev = 0f;
    public GameObject GameObject;
    private JohnArduinoManager Arduino;
    int detach = 0;
    int counter = 0;
    float Aread = 0f;
    float Aangle = 0f;
    float AanglePrev = 0f;
    public float toIMU = 0f;
    bool shapeActive = false;

    float offset = 0;

    Vector3 offsetVec = new Vector3(0f, 0f, 0f);


    public float[] potVal = new float[2];


    public float position = 0f;

    public float writeAngle;

    public float microzangle;

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
        if (negative == true)
        {
            negPos = -1;
        }
        else if (negative == false)
        {
            negPos = 1;
        }
        Aread = Arduino.currentVals[inputValue];

        Aangle = (Aread - convOffset) * (convMultiplyer);



        //Aangle = median_of_3(filterVal[0], filterVal[1], filterVal[2]);
        //Aangle = median_of_5(filterVal);




        if (Input.GetKeyDown("d"))
        {
            detach++;
            detach = detach % 2;
        }

        if (detach == 1)
        {
            keyRotate();
            toIMU = zangle;
        }
        else if (detach == 0)
        {
            bendRotate();
            toIMU = potVal[0];
        }


    }

    //void keyRotate()
    //{
    //    //zangle = potVal[1];

    //    if (Input.GetKey("left"))
    //    {
    //        if (zangle >= -95 && zangle <= 90)
    //        {
    //            zangle++;
    //        }
    //    }
    //    else if (Input.GetKey("right"))
    //    {
    //        if (zangle >= -90 && zangle <= 95)
    //        {
    //            zangle--;
    //        }
    //    }
    //    else if (Input.GetKey("1"))
    //    {
    //        shape1();
    //    }
    //    else if (Input.GetKey("2"))
    //    {
    //        shape2();
    //    }
    //    else if (Input.GetKey("3"))
    //    {
    //        shape3();
    //    }
    //    //float potChange = potVal[0] - potVal[1];
    //    //transform.Rotate(0, 0, negPos * potChange, Space.Self);
    //    float zangleChange = zangle - zanglePrev;

    //    transform.Rotate(0, 0, negPos * zangleChange, Space.Self);
    //    zanglePrev = zangle;

    //}


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
        else if (Input.GetKey("right"))
        {
            if (zangle >= -90 && zangle <= 95)
            {
                zangle--;
            }
        }
        else if (Input.GetKey("1"))
        {
            shape1();
        }
        else if (Input.GetKey("2"))
        {
            shape2();
        }
        else if (Input.GetKey("3"))
        {
            shape3();
        }

        if (print == true)
        {
            print("zangle: " + zangle);
        }
        //float potChange = potVal[0] - potVal[1];
        //transform.Rotate(0, 0, negPos * potChange, Space.Self);

        //float zangleChange = zangle - zanglePrev;
        //transform.Rotate(0, 0, negPos * zangleChange, Space.Self);
        //zanglePrev = zangle;

        transform.localEulerAngles = new Vector3(0f, 0f, negPos * (zangle));

    }

    void bendRotate()
    {
        //zangle = zangle + Aangle - AanglePrev;
        //AanglePrev = Aangle;

        ////zangle = potVal[0];
        //float zangleChange = zangle - zanglePrev;

        //transform.Rotate(0, 0, negPos * zangleChange, Space.Self);


        //float potChange = potVal[0] - potVal[1];
        //transform.Rotate(0, 0, negPos * potChange, Space.Self);

        zangle = Aangle;

        if (Input.GetKeyDown("z"))
        {
            offset = zangle;
            //Quaternion zero = Quaternion.Euler(0, 0, 0);
            //transform.localRotation = zero;
            //zangle = 0;
        }
        zangle -= offset;
        transform.localEulerAngles = new Vector3(0f, 0f, negPos*(zangle));


        //zanglePrev = zangle;
    }

    float median_of_3(float a, float b, float c)
    {
        float the_max = Math.Max(Math.Max(a, b), c);
        float the_min = Math.Min(Math.Min(a, b), c);
        // unnecessarily clever code
        float the_median = a + b + c - the_max - the_min;
        return the_median;
    }


    float median_of_5(float[] arr)
    {
        float[] temp = new float[5];
        for (int i = 0; i < 5; i++)
        {
            temp[i] = arr[i];
        }
        Array.Sort(arr);
        float the_median;
        if ((arr[2] - arr[1]) > 30)
        {
             the_median = arr[1];
        }
        else
        {
             the_median = arr[2];
        }
        for (int i = 0; i < 5; i++)
        {
            arr[i] = temp[i];
        }
        return the_median;
    }

    void shape1()
    {
        zangle = 0;
    }

    void shape2()
    {

        if (shapeActive == true)
        {
            zangle = 0;
            shapeActive = false;
        }
        else
        {
            if (servo5n6 || servo7n8)
            {
                zangle = 76;
            }
            else
            {
                zangle = 0;
            }
            shapeActive = true;
        }
    }

    void shape3()
    {
        if (shapeActive == true)
        {
            zangle = 0;
            shapeActive = false;
        }
        else
        {
            zangle = zangle + 43;
            shapeActive = true;
        }
    }

}
