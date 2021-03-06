﻿using UnityEngine;
using System.Collections;
using System.IO.Ports;
using UnityEngine.UI;
using System.Collections.Generic;
//using UnityEditor;
using System;
using System.Globalization;
using System.Threading;

public class JohnArduinoManager : MonoBehaviour
{
    int trigger;
    public static string serialName = "COM3";
    public SerialPort mySPort = new SerialPort(serialName, 115200);

    public float[] currentVals = new float[21];

    public bool output;

    //float[] detach = new float[6];

    float detach = 0f;


    private float rotateLeftVal2;
    private GeneralRotate RotateL2;
    private float rotateLeftVal3;
    private GeneralRotate RotateL3;
    private float rotateLeftVal4;
    private GeneralRotate RotateL4;

    private float rotateRightVal2;
    private GeneralRotate RotateR2;
    private float rotateRightVal3;
    private GeneralRotate RotateR3;
    private float rotateRightVal4;
    private GeneralRotate RotateR4;

    private GeneralRotate GeneralRotate;

    public GameObject HingeL2;
    public GameObject HingeL3;
    public GameObject HingeL4;

    public GameObject HingeR2;
    public GameObject HingeR3;
    public GameObject HingeR4;

    char SOP = '&';
    char EOP = '$';

    bool started = false;
    bool ended = false;

    void Awake()
    {

        RotateL2 = HingeL2.GetComponent<GeneralRotate>();
        RotateL3 = HingeL3.GetComponent<GeneralRotate>();
        RotateL4 = HingeL4.GetComponent<GeneralRotate>();

        RotateR2 = HingeR2.GetComponent<GeneralRotate>();
        RotateR3 = HingeR3.GetComponent<GeneralRotate>();
        RotateR4 = HingeR4.GetComponent<GeneralRotate>();
    }

    // Use this for initialization
    void Start()
    {
        mySPort.Open();
        trigger = 1;
        mySPort.NewLine = "$";
        //for(int i=0; i<6; i++)
        //{
        //    detach[i] = 0;
        //}
    }

    // Update is called once per frame
    void Update()
    {           

        if (Input.GetKeyDown("d"))
        {
            detach++;
            detach = detach % 2;

        }

       

       
        //if (trigger == 0)
        //{
        //    rotateLeftVal1 = RotateL1.zangle + 90;
        //    //print("rotateRLeftVal1: " + rotateLeftVal1);
        //    rotateLeftVal2 = RotateL2.zangle + 90;
        //    rotateLeftVal3 = RotateL3.zangle + 90;
        //    rotateLeftVal4 = RotateL4.zangle + 90;

        //    rotateRightVal1 = RotateR1.zangle + 90;
        //    rotateRightVal2 = RotateR2.zangle + 90;
        //    rotateRightVal3 = RotateR3.zangle + 90;
        //    rotateRightVal4 = RotateR4.zangle + 90;
        //    trigger = 1;


        //    writeValues();
        //    print("writeValues");
        //}
        //else if (trigger == 1)
        //{


        char serialChar = '\0';
        string serialValue = string.Empty;

        //serialValue = (char)mySPort.ReadChar();
        //print("serialValue: " + serialValue);
        //int counter = 0;

        //for(int i = 0; i<70; i++)
        //{
        //    serialValue = (char)mySPort.ReadChar();
        //    if (serialValue == SOP)
        //    {
        //        //index = 0;
        //        //serialValues[index] = '\0';
        //        started = true;
        //        //ended = false;
        //        break;
        //    }
        //    else if (i == 69)
        //    {
        //        started = true;
        //        ended = true;
        //        break;
        //    }
        //}

        //string[] serialValues;
        while (started == false)
            {
                serialChar = (char)mySPort.ReadChar();
                //print("serialValue: " + serialChar);
                if (serialChar == SOP)
                {
                serialValue = mySPort.ReadLine();


                //string[] serialValues = serialValue.Split('&');
                //print("serialValue " + serialValue);
                if (serialValue.Length > 1)
                {
                    string[] bendValues = serialValue.Split(',');

                    // for (int t = 0; t < 5; t++)
                    //print("bendValues " + bendValues[t]);
                    //float[] floatBendValues = new float[bendValues.Length];
                    for (int j = 0; j < (bendValues.Length); j++)
                    {
                        currentVals[j] = float.Parse(bendValues[j]);



                    }
                }
                //index = 0;
                //serialValues[index] = '\0';
                started = true;
                ended = true;
                    break;
                }
            }


        if (started == true && ended == true)
            {
                // The end of packet marker arrived. Process the packet

                // Reset for the next packet
                started = false;
                ended = false;
                //index = 0;
                //serialValues[index] = '\0';
            }

        mySPort.DiscardInBuffer();

        //print("rotateRLeftVal1: " + rotateLeftVal1);
        rotateLeftVal2 = -RotateL2.zangle + 90;
        rotateLeftVal3 = -RotateL3.zangle + 90;
        rotateLeftVal4 = -RotateL4.zangle + 90;

        rotateRightVal2 = -RotateR2.zangle + 90;
        rotateRightVal3 = -RotateR3.zangle + 90;
        rotateRightVal4 = -RotateR4.zangle + 90;
        trigger = 1;

        //print("rotateRightVal4: " + RotateR4.zangle);

        writeValues();


        mySPort.DiscardOutBuffer();

    }
    void writeValues()
    {
        string writeVal;

        //if (detach == 1)
        //{
            writeVal = "<" + string.Format("{0:N02}", detach) + "," + string.Format("{0:N02}", rotateLeftVal4) + ", " + string.Format("{0:N02}", rotateLeftVal2) + ", " + string.Format("{0:N02}", rotateRightVal3) + ", " + string.Format("{0:N02}", rotateRightVal4) + ", " + string.Format("{0:N02}", rotateRightVal2) + ", " + string.Format("{0:N02}", rotateLeftVal3) + ">";
        //}
        //else
        //{
            //writeVal = "<" + string.Format("{0:N02}", detach) + ">";
        //}
        mySPort.WriteLine(writeVal);

        //string writeVal = string.Format("{0:N03}", rotateLeftVal) + "&" + string.Format("{0:N03}", rotateRightVal) + "&";
        //mySPort.WriteLine(writeVal);

        //print("writeval: " + writeVal);

        //print("detach: " + detach);

    }
    void individualServo()
    {
        //if (Input.GetKeyDown("2"))
        //{
        //    detach[1]++;
        //    detach[1] = detach[1] % 2;
        //}

        //if (Input.GetKeyDown("3"))
        //{
        //    detach[2]++;
        //    detach[2] = detach[2] % 2;
        //}

        //if (Input.GetKeyDown("4"))
        //{
        //    detach[3]++;
        //    detach[3] = detach[3] % 2;
        //}

        //if (Input.GetKeyDown("5"))
        //{
        //    detach[4]++;
        //    detach[4] = detach[4] % 2;
        //}

        //if (Input.GetKeyDown("6"))
        //{
        //    detach[5]++;
        //    detach[5] = detach[5] % 2;
        //}
    }
}
