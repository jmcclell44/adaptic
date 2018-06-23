﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System;
using UnityEngine;
using UnityEngine.UI;

public class ModelMap : MonoBehaviour {
    public Text approachText;

    private Quaternion imucap;
    private float zAngle = 0f;
    private float zAnglePrev = 0f;
    private float bendValue = 0f;

    private JohnArduinoManager Arduino;
    public GameObject GameObject;

    public GameObject[] Device = new GameObject[4];

    public GameObject[] Models = new GameObject[6];
    public GameObject[] Targets = new GameObject[6];
    private int[] random = new int[6] { 0, 1, 2, 3, 4, 5};
    private int[] randomTarget = new int[6] { 0, 1, 2, 3, 4, 5 };
    private int[] randomApproach = new int[3] { 0, 1, 2};

    private float[] difference = new float[4];

    private int counter = 1;

    private float qW = 0;
    private float qY = 0;
    private float qX = 0;
    private float qZ = 0;

    bool Mapped;

    int mapToggle = 1;
    //int posToggle = 1;
    bool modelOn = false;

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

    public GameObject HingeL2;
    public GameObject HingeL3;
    public GameObject HingeL4;

    public GameObject HingeR2;
    public GameObject HingeR3;
    public GameObject HingeR4;

    Vector3[] targetLocations = new[] {
        new Vector3(5f, 5f, -5f),
        new Vector3(5f, -5f, -5f),
        new Vector3(-5f, 5f, -5f),
        new Vector3(-5f, -5f, -5f),
        new Vector3(7.07f, 0f, -5f),
        new Vector3(-7.07f, 0f, -5f)
    };

    Vector3[] targetRotations = new[] {
        new Vector3(60f, 30f, 0f),
        new Vector3(0f, 60f, 30f),
        new Vector3(30f, 0f, 60f),
        new Vector3(60f, 0f, 30f),
        new Vector3(30f, 60f, 0f),
        new Vector3(0f, 30f, 60f)
    };

    string[] stringModels = new string[6] { "book", "phone", "tablet", "hammer", "pen", "flashlight" };

    string[] stringApproaches = new string[3] { "Approach 1", "Approach 2", "Approach 3"};
    string stringApproach;

    float timer = 0f;
    bool timerOn = false;

    Vector3 targetLocation;
    Vector3 targetRotation;

    Vector3 locationStart;
    Vector3 locationEnd;
    Vector3 rotationEnd;
    Vector3 locationDiff;
    Vector3 rotationDiff;

    int participantNumber = 1;
    string fileName;
    int currentModel;
    private int approachCounter = 0;
    bool textOn = true;

    void Awake()
    {
        Arduino = GameObject.GetComponent<JohnArduinoManager>();

        RotateL2 = HingeL2.GetComponent<GeneralRotate>();
        RotateL3 = HingeL3.GetComponent<GeneralRotate>();
        RotateL4 = HingeL4.GetComponent<GeneralRotate>();

        RotateR2 = HingeR2.GetComponent<GeneralRotate>();
        RotateR3 = HingeR3.GetComponent<GeneralRotate>();
        RotateR4 = HingeR4.GetComponent<GeneralRotate>();

    }


    // Use this for initialization
    void Start () {

        for (int i = 0; i < 6; i++)
        {
            Models[i].SetActive(false);
        }

        Mapped = false;


    }
	
	// Update is called once per frame
	void Update () {

        updateAngles();

        if (Input.GetKeyDown("h"))
        {
            if (approachCounter == 0)
            {
                System.Random ap = new System.Random();
                randomApproach = randomApproach.OrderBy(x => ap.Next()).ToArray();
            }


            if (approachCounter < 3)
            {
            stringApproach = "Haptic Approach: " + stringApproaches[randomApproach[approachCounter]];
                approachCounter++;
                //approachCounter = approachCounter % 3;

            }
            else if (approachCounter == 3 )
            {
                stringApproach = "Haptic Approach: Approach 4";

                approachCounter = 0;
            }

            print("string approach: " + stringApproach);
            approachText.text = stringApproach;
        }

        if (Input.GetKeyDown("j"))
        {
            textOn = !textOn;
            if(textOn == true)
            {
                approachText.enabled = true;
            }
            else
            {
                approachText.enabled = false;
            }

        }



        if (Input.GetKeyDown("v"))
        {
            //posToggle++;
            //posToggle = posToggle % 2;

            modelOn = !modelOn;

            //if (posToggle == 0)
            if (modelOn == true)
            {

                if (counter == 1)
                {
                    System.Random r = new System.Random();
                    random = random.OrderBy(x => r.Next()).ToArray();

                }
                timer = 0f;
                currentModel = random[counter];
                counter++;
                counter = counter % 6;
                print("counter " + counter);
                print("Model Number " + random[counter]);

                Models[currentModel].SetActive(true);
                Models[currentModel].transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                Models[currentModel].transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);


                for (int i = 0; i < 4; i++)
                {
                    Device[i].SetActive(false);
                }
            }
            //else if (posToggle == 1)
            if (modelOn == false)
            {
                Models[currentModel].SetActive(false);
                Targets[currentModel].SetActive(false);

                for (int i = 0; i < 4; i++)
                {
                    Device[i].SetActive(true);
                }
            }
        }
            if (Input.GetKeyDown("b") && modelOn == true)
            {
                System.Random rt = new System.Random();
                randomTarget = random.OrderBy(x => rt.Next()).ToArray();

            targetLocation = targetLocations[randomTarget[counter]];
            targetRotation = targetRotations[randomTarget[counter]];

            //Color color = Targets[currentModel].GetComponent<Renderer>().material.color;
            //color.a = 1f;

            Targets[currentModel].SetActive(true);

            //Device[i].GetComponent<Renderer>().material.SetColor("_Color", color);

            Targets[currentModel].transform.localPosition = targetLocation;
                Targets[currentModel].transform.eulerAngles = targetRotation;

            }
        if (Input.GetKeyDown("n") && modelOn == true)
        {
            timerOn = true;
            locationStart = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        if (timerOn == true)
        {
            timer += Time.deltaTime;
        }

        if (Input.GetKeyDown("m") && modelOn == true)
        {
            timerOn = false;
            locationEnd = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            locationDiff = targetLocation - locationStart;
            rotationEnd = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
            rotationDiff = targetRotation - rotationEnd;

            makeStringFile();
        }

        if (Input.GetKeyDown("c"))
        {
            mapToggle++;
            mapToggle = mapToggle % 2;

            if (mapToggle == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    Device[i].SetActive(false);
                }
                imucap = new Quaternion(qX, qZ, qY, qW);
                Mapped = true;
            }
            if (mapToggle == 1)
            {
                //Models[random[counter]].transform.rotation = Quaternion.identity;
                for (int i = 0; i < 4; i++)
                {
                    Device[i].SetActive(true);
                }
                Mapped = false;
            }
        }
        //if (Mapped == false)
        //{
        //    qW = Arduino.currentVals[6];
        //    qY = Arduino.currentVals[7];
        //    qX = Arduino.currentVals[8];
        //    qZ = Arduino.currentVals[9];
        //    //qW = qW - difference[0];
        //    //qY = qY - difference[1];
        //    //qZ = qZ - difference[2];
        //    //qX = qX - difference[3];
        //    //Quaternion diff = new Quaternion(-difference[0], -difference[1], -difference[2], -difference[3]);
        //    Quaternion imu = new Quaternion(qW, qY, qX, qZ);
        //    Quaternion newimu = imu*Quaternion.Inverse(imucap);
        //    //RotateObject(bendValue / 2);
        //    //Models[random[counter]].transform.rotation = newimu;
        //    Models[random[counter]].transform.rotation = Quaternion.identity;
        //}
    }

    //Loads string variable with expression array
    void makeStringFile()
    {
        string s = null;
        string stringParticipantNumber;
        string stringVirtualObject;
        string stringTargetLocation;
        string stringTargetRotation;

        string stringLocationStart;
        string stringLocationEnd;
        string stringRotationEnd;
        string stringLocationDiff;
        string stringRotationDiff;
        string stringTimer;

        //stringApproach = ;

        stringParticipantNumber = "Participant Number: " + targetLocation.ToString("N");
        s += stringParticipantNumber;
        s += "/ ";

        stringTargetLocation = "Target Location: " + targetLocation.ToString("N");
        s += stringTargetLocation;
        s += "/ ";

        stringTargetRotation = "Target Rotatio: " + targetRotation.ToString("N");
        s += stringTargetRotation;
        s += "/ ";

        stringLocationStart = "LocationStart: " + locationStart.ToString("N");
        s += stringLocationStart;
        s += "/ ";

        stringLocationEnd = "Location End: " + locationEnd.ToString("N");
        s += stringLocationEnd;
        s += "/ ";

        stringRotationEnd = "Rotation End: " + rotationEnd.ToString("N");
        s += stringRotationEnd;
        s += "/ ";

        stringLocationDiff = "Location Diff: " + locationDiff.ToString("N");
        s += stringLocationDiff;
        s += "/ ";

        stringRotationDiff = "Rotation Diff: " + rotationDiff.ToString("N");
        s += stringRotationDiff;
        s += "/ ";

        stringTimer = "Timer: " + timer.ToString("N");
        s += stringTimer;
        s += "/ ";

        fileName = "Assets/textFiles/test_" + participantNumber;
        fileName += ".txt";



        System.IO.File.WriteAllText(fileName, s);



        ////		s = Convert.ToString (expressions);
        //for (int i = 0; i < 15; i++)
        //{
        //    //
        //    s += expressions[i].ToString("N");
        //    s += ",";
        //    //			s += string.Format("{N}\n", expressions[i]);
        //}
        ////		s += "Emotions\n";
        //for (int i = 0; i < 9; i++)
        //{
        //    s += emotions[i].ToString("N");
        //    s += ",";
        //    //			s += string.Format("{2}\n", emotions[i]);
        //}

        participantNumber++;

        //return s;
    }

    void updateAngles()
        {
            bendValue = Arduino.currentVals[0];
            qW = Arduino.currentVals[6];
            qY = Arduino.currentVals[7];
            qX = Arduino.currentVals[8];
            qZ = Arduino.currentVals[9];
        }
    void RotateObject(float Value)
    {


        zAngle = (Value - 511.5f) * 0.352f;

        float zAngleChange = zAngle - zAnglePrev;

        Models[random[counter]].transform.Rotate(zAngleChange, 0, 0, Space.Self);
        zAnglePrev = zAngle;
    }

    //void trigger()
    //{
    //    rotateLeftVal2 = RotateL2.zangle;
    //    rotateLeftVal3 = RotateL3.zangle;
    //    rotateLeftVal4 = RotateL4.zangle;

    //    rotateRightVal2 = RotateR2.zangle;
    //    rotateRightVal3 = RotateR3.zangle;
    //    rotateRightVal4 = RotateR4.zangle;

    //    if (rotateRightVal2 > 65 && rotateLeftVal2 > 65 )
    //    {
    //        Models[1].SetActive(true);
    //        Models[1].transform.position = new Vector3(0, 6, 0);
    //        for (int i = 0; i < 4; i++)
    //        {
    //            Device[i].SetActive(false);
    //        }
    //        //imucap = new Quaternion(qX, qZ, qY, qW);
    //        Mapped = true;

    //    }
    //    else
    //    {
    //        Models[1].SetActive(false);
    //        for (int i = 0; i < 4; i++)
    //        {
    //            Device[i].SetActive(true);
    //        }
    //        Mapped = false;
    //    }

    //    if (rotateRightVal2 < -65 && rotateRightVal2 > -100 && rotateLeftVal2 < -65)
    //    {
    //        Models[1].SetActive(true);
    //        Models[1].transform.position = new Vector3(0, 6, 0);
    //        for (int i = 0; i < 4; i++)
    //        {
    //            Device[i].SetActive(false);
    //        }
    //        //imucap = new Quaternion(qX, qZ, qY, qW);
    //        Mapped = true;

    //    }
    //    else
    //    {
    //        Models[1].SetActive(false);
    //        for (int i = 0; i < 4; i++)
    //        {
    //            Device[i].SetActive(true);
    //        }
    //        Mapped = false;
    //    }

    //}
}
