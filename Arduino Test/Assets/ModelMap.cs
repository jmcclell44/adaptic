using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System;
using UnityEngine;

public class ModelMap : MonoBehaviour {
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

    private float[] difference = new float[4];

    private int counter = 1;

    private float qW = 0;
    private float qY = 0;
    private float qX = 0;
    private float qZ = 0;

    bool Mapped;

    int mapToggle = 1;
    int posToggle = 1;
    //bool reRandomize = true;

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

    float timer;

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
        //for (int i = 6; i < 6; i++)
        //{
        //    //int number = Random.Range(0, 5);

        //    random[i] = i;
        //}

        //System.Random r = new System.Random();

        //random = random.OrderBy(x => r.Next()).ToArray();

        for (int i = 0; i < 6; i++)
        {
            Models[i].SetActive(false);
        }

        Mapped = false;
    }
	
	// Update is called once per frame
	void Update () {
        bendValue = Arduino.currentVals[0];
        qW = Arduino.currentVals[6];
        qY = Arduino.currentVals[7];
        qX = Arduino.currentVals[8];
        qZ = Arduino.currentVals[9];

        trigger();

        if (Input.GetKeyDown("q"))
        {
            posToggle++;
            posToggle = posToggle % 2;

            if (posToggle == 0)
            {

                if (counter == 1)
                {
                    System.Random r = new System.Random();
                    random = random.OrderBy(x => r.Next()).ToArray();

                }

                counter++;
                counter = counter % 6;
                print("counter " + counter);
                print("Model Number " + random[counter]);

                Models[random[counter]].SetActive(true);
                Models[random[counter]].transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                Models[random[counter]].transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);


                for (int i = 0; i < 4; i++)
                {
                    //Color color = Device[i].GetComponent<Renderer>().material.color;
                    //color.a = 0.5f;
                    Device[i].SetActive(false);
                    //Device[i].GetComponent<Renderer>().material.SetColor("_Color", color);
                }

                //Color color2 = Models[random[counter]].GetComponent<Renderer>().material.color;
                //color2.a = 0.5f;

                //Models[random[counter]].GetComponent<Renderer>().material.SetColor("_Color", color2);

            }
            else if (posToggle == 1)
            {
                Models[random[counter]].SetActive(false);
                Targets[random[counter]].SetActive(false);

                for (int i = 0; i < 4; i++)
                {
                    //Color color = Device[i].GetComponent<Renderer>().material.color;
                    //color.a = 1f;
                    Device[i].SetActive(true);
                    //Device[i].GetComponent<Renderer>().material.SetColor("_Color", color);
                }
            }
        }
            if (Input.GetKeyDown("w") && posToggle == 0)
            {
                System.Random rt = new System.Random();
                randomTarget = random.OrderBy(x => rt.Next()).ToArray();

                Targets[random[counter]].SetActive(true);
                Targets[random[counter]].transform.localPosition = targetLocations[randomTarget[counter]];
                Targets[random[counter]].transform.eulerAngles = new Vector3(15, 15, 15);

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

                //Color color2 = Models[random[counter]].GetComponent<Renderer>().material.color;
                //color2.a = 1f;

                //Models[random[counter]].GetComponent<Renderer>().material.SetColor("_Color", color2);
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

    void RotateObject(float Value)
    {

        zAngle = (Value - 511.5f) * 0.352f;

        float zAngleChange = zAngle - zAnglePrev;

        Models[random[counter]].transform.Rotate(zAngleChange, 0, 0, Space.Self);
        zAnglePrev = zAngle;
    }

    void trigger()
    {
        rotateLeftVal2 = RotateL2.zangle;
        rotateLeftVal3 = RotateL3.zangle;
        rotateLeftVal4 = RotateL4.zangle;

        rotateRightVal2 = RotateR2.zangle;
        rotateRightVal3 = RotateR3.zangle;
        rotateRightVal4 = RotateR4.zangle;

        if (rotateRightVal2 > 65 && rotateLeftVal2 > 65 )
        {
            Models[1].SetActive(true);
            Models[1].transform.position = new Vector3(0, 6, 0);
            for (int i = 0; i < 4; i++)
            {
                Device[i].SetActive(false);
            }
            //imucap = new Quaternion(qX, qZ, qY, qW);
            Mapped = true;

        }
        else
        {
            Models[1].SetActive(false);
            for (int i = 0; i < 4; i++)
            {
                Device[i].SetActive(true);
            }
            Mapped = false;
        }

        if (rotateRightVal2 < -65 && rotateRightVal2 > -100 && rotateLeftVal2 < -65)
        {
            Models[1].SetActive(true);
            Models[1].transform.position = new Vector3(0, 6, 0);
            for (int i = 0; i < 4; i++)
            {
                Device[i].SetActive(false);
            }
            //imucap = new Quaternion(qX, qZ, qY, qW);
            Mapped = true;

        }
        else
        {
            Models[1].SetActive(false);
            for (int i = 0; i < 4; i++)
            {
                Device[i].SetActive(true);
            }
            Mapped = false;
        }

    }
}
