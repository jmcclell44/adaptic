using UnityEngine;
using System.Collections;
using System.IO.Ports;
using UnityEngine.UI;
using System.Collections.Generic;
//using UnityEditor;
using System;
using System.Globalization;
using System.Threading;

public class JohnArduinoManager_3 : MonoBehaviour
{

    //public static string serialName = "COM4";
    //public SerialPort mySPort = new SerialPort(serialName, 9600);

    public float[] currentVals = new float[25];

    public bool output;

    float detach = 0f;

    private float rotateLeftVal1;
    private rotateL1 RotateL1;
    private float rotateLeftVal2;
    private rotateL2 RotateL2;
    private float rotateLeftVal3;
    private rotateL3 RotateL3;
    private float rotateLeftVal4;
    private rotateL4 RotateL4;

    private float rotateRightVal1;
    private rotateR1 RotateR1;
    private float rotateRightVal2;
    private rotateR2 RotateR2;
    private float rotateRightVal3;
    private rotateR3 RotateR3;
    private float rotateRightVal4;
    private rotateR4 RotateR4;


    public GameObject HingeL1;
    public GameObject HingeL2;
    public GameObject HingeL3;
    public GameObject HingeL4;

    public GameObject HingeR1;
    public GameObject HingeR2;
    public GameObject HingeR3;
    public GameObject HingeR4;




    public SerialController serialController;




    void Awake()
    {
        //if (output == true)
        //{
        RotateL1 = HingeL1.GetComponent<rotateL1>();
        RotateL2 = HingeL2.GetComponent<rotateL2>();
        RotateL3 = HingeL3.GetComponent<rotateL3>();
        RotateL4 = HingeL4.GetComponent<rotateL4>();

        RotateR1 = HingeR1.GetComponent<rotateR1>();
        RotateR2 = HingeR2.GetComponent<rotateR2>();
        RotateR3 = HingeR3.GetComponent<rotateR3>();
        RotateR4 = HingeR4.GetComponent<rotateR4>();
        //}
    }



    // Initialization
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();

        Debug.Log("Press A or Z to execute some actions");
    }

    // Use this for initialization
    //    void Start()
    //{
    //    mySPort.Open();
    //}

    // Update is called once per frame
    void Update()
    {


        rotateLeftVal1 = RotateL1.zangle + 90;
        //print("rotateRLeftVal1: " + rotateLeftVal1);
        rotateLeftVal2 = RotateL2.zangle + 90;
        rotateLeftVal3 = RotateL3.zangle + 90;
        rotateLeftVal4 = RotateL4.zangle + 90;

        rotateRightVal1 = RotateR1.zangle + 90;
        rotateRightVal2 = RotateR2.zangle + 90;
        rotateRightVal3 = RotateR3.zangle + 90;
        rotateRightVal4 = RotateR4.zangle + 90;
        if (output == true)
        {
            writeValues();
        }
        //string serialValue = mySPort.ReadLine();
        string serialValue = serialController.ReadSerialMessage();


        string[] serialValues = serialValue.Split('&');
        //print("serialValue " + serialValue);

        if (serialValues.Length > 1)
        {
            string[] bendValues = serialValues[1].Split(',');

            // for (int t = 0; t < 5; t++)
            //print("bendValues " + bendValues[t]);
            //float[] floatBendValues = new float[bendValues.Length];
            for (int j = 0; j < (bendValues.Length); j++)
            {
                currentVals[j] = float.Parse(bendValues[j]);



            }
        }
        if (Input.GetKeyDown("d"))
        {
            detach++;
            detach = detach % 2;
        }

    }
    void writeValues()
    {


        string writeVal = "<" + string.Format("{0:N02}", detach) + ", " + string.Format("{0:N02}", rotateLeftVal1) + ", " + string.Format("{0:N02}", rotateLeftVal2) + ", " + string.Format("{0:N02}", rotateLeftVal3) + ", " + string.Format("{0:N02}", rotateLeftVal4) + ", " + string.Format("{0:N02}", rotateRightVal1) + ", " + string.Format("{0:N02}", rotateRightVal2) + ", " + string.Format("{0:N02}", rotateRightVal3) + ", " + string.Format("{0:N02}", rotateRightVal4) + ">";
        //string writeVal = "<" + string.Format("{0:N02}", detach) + ", " + string.Format("{0:N02}", rotateRightVal1) + ", " + string.Format("{0:N02}", rotateRightVal2) + ", " + string.Format("{0:N02}", rotateRightVal3) + ", " + string.Format("{0:N02}", rotateRightVal4) + ", " + string.Format("{0:N02}", rotateLeftVal1) + ", " + string.Format("{0:N02}", rotateLeftVal2) + ", " + string.Format("{0:N02}", rotateLeftVal3) + ", " + string.Format("{0:N02}", rotateLeftVal4) + ">";

        //mySPort.WriteLine(writeVal);
        serialController.SendSerialMessage(writeVal);

        //string writeVal = string.Format("{0:N03}", rotateLeftVal) + "&" + string.Format("{0:N03}", rotateRightVal) + "&";
        //mySPort.WriteLine(writeVal);

        print("RotateLeftVal: " + writeVal);

        //print("detach: " + detach);

    }
    //void RotateObject(float Value)
    //{
    //    zAngle = (Value - 511.5f) * 0.352f;
    //    float zAngleChange = zAngle - zAnglePrev;

    //    print("zAngle= " + zAngle);
    //    print("zAngleChange= " + zAngleChange);

    //    Vector3 Rotate = new Vector3(zAngleChange, 0, 0);

    //    transform.Rotate(Rotate, Space.Self);

    //    zAnglePrev = zAngle;
    //}

}
