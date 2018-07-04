using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System;
using UnityEngine;
using UnityEngine.UI;

public class ModelMap : MonoBehaviour {
    string s = null;
    string stringParticipantNumber;
    string stringCurrentModel;
    string stringVirtualObject;
    string stringTargetLocation;
    string stringTargetRotation;

    string stringLocationStart;
    string stringLocationEnd;
    string stringRotationEnd;
    string stringLocationDiff;
    string stringLocationDiffCM;
    string stringRotationDiff;
    string stringTimer;

    public Text approachText;

    private Quaternion imucap;
    private float zAngle = 0f;
    private float zAnglePrev = 0f;
    private float bendValue = 0f;

    private JohnArduinoManager Arduino;
    public GameObject GameObject;

    public GameObject[] Device = new GameObject[4];


    private int[] randomApproach = new int[3] { 0, 1, 2};

    private float[] difference = new float[4];

    private int counter = 0;
    private int targetLocationCounter = 0;
    private int targetRotationCounter = 0;

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

    public GameObject handController;


    ////6 virtual objects//
    //Vector3[] targetLocations = new[] {
    //    new Vector3(5f, 5f, -5f),
    //    new Vector3(5f, -5f, -5f),
    //    new Vector3(-5f, 5f, -5f),
    //    new Vector3(-5f, -5f, -5f),
    //    new Vector3(7.07f, 0f, -5f),
    //    new Vector3(-7.07f, 0f, -5f)
    //};

    //Vector3[] targetRotations = new[] {
    //    new Vector3(60f, 30f, 0f),
    //    new Vector3(0f, 60f, 30f),
    //    new Vector3(30f, 0f, 60f),
    //    new Vector3(60f, 0f, 30f),
    //    new Vector3(30f, 60f, 0f),
    //    new Vector3(0f, 30f, 60f)
    //};

    //string[] stringModels = new string[6] { "book", "phone", "tablet", "hammer", "pen", "flashlight" };

    //public GameObject[] Models = new GameObject[6];
    //public GameObject[] Targets = new GameObject[6];
    //private int[] random = new int[6] { 0, 1, 2, 3, 4, 5 };
    //private int[] randomTarget = new int[6] { 0, 1, 2, 3, 4, 5 };

    //4 virtual objects//
    Vector3[] targetLocations = new[] {
        //new Vector3(0.093f*31.45f, 0.285f*31.45f, -0.2f*31.45f),
        //new Vector3(-0.093f*31.45f, 0.285f*31.45f, -0.2f*31.45f),
        new Vector3(0.243f*31.45f, 0.177f*31.45f, 0.2f*31.45f),
        new Vector3(-0.243f*31.45f, 0.177f*31.45f, 0.2f*31.45f),
        new Vector3(0f, 9.4f, 0.2f*31.45f),
        new Vector3(0.243f*31.45f, 0.177f*31.45f, 0.2f*31.45f),
        new Vector3(-0.243f*31.45f, 0.177f*31.45f, 0.2f*31.45f),
        new Vector3(0f, 9.4f, 0.2f*31.45f),
        new Vector3(0.243f*31.45f, 0.177f*31.45f, 0.2f*31.45f),
        new Vector3(-0.243f*31.45f, 0.177f*31.45f, 0.2f*31.45f),
        new Vector3(0f, 9.4f, 0.2f*31.45f)
    };

    Vector3[] targetRotations = new[] {
        new Vector3(360f-60f, 360f-30f, 360f-15f),
        new Vector3(360f-15f, 360f-60f, 360f-30f),
        new Vector3(360f-30f, 360f-15f, 360f-60f),
        new Vector3(360f-15f, 360f-60f, 360f-30f),
        new Vector3(360f-30f, 360f-15f, 360f-60f),
        new Vector3(360f-60f, 360f-30f, 360f-15f),
        new Vector3(360f-30f, 360f-15f, 360f-60f),
        new Vector3(360f-60f, 360f-30f, 360f-15f),
        new Vector3(360f-15f, 360f-60f, 360f-30f)


        //new Vector3(60f, 15f, 120f),
    };

   

    string[] stringModels = new string[4] { "book", "flashlight", "tablet", "hammer" };

    public GameObject[] Models = new GameObject[4];
    public GameObject[] Targets = new GameObject[4];
    private int[] random = new int[4] { 0, 1, 2, 3};
    private int[] randomLocationTarget = new int[9] { 0, 1, 2, 3, 4, 5, 6, 7, 8};
    private int[] randomRotationTarget = new int[4] { 0, 1, 2, 3 };


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
    Vector3 locationDiffCM;
    Vector3 rotationDiff;

    Quaternion QrotationDiff;
    Quaternion QrotationEnd;

    Quaternion rotationQTarget;

    int participantNumber = 1;
    string fileName;
    int currentModel;
    int currentLocationTarget;
    int currentRotationTarget;
    private int approachCounter = 0;
    bool textOn = true;

    bool participantKnown = false;

    public float scale = 31.45f;

    public GameObject parentHand;

    System.Random rt;
    System.Random lt;


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

        for (int i = 0; i < Models.Length; i++)
        {
            Models[i].SetActive(false);
        }

        Mapped = false;
        //for(int i = 0; i<targetLocations.Length; i++)
        //{
        //    targetLocations[i] = new Vector3(targetLocations[i].x * scale, targetLocations[i].y * scale, targetLocations[i].z * scale);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        rt = new System.Random();
        //lt = new System.Random();





        updateAngles();

        if (Input.GetKeyDown("h"))
        {
            if (approachCounter == 0)
            {
                System.Random ap = new System.Random();
                randomApproach = randomApproach.OrderBy(x => ap.Next()).ToArray();

                fileExist();
            }


            if (approachCounter < 3)
            {
                stringApproach = "Haptic Approach: " + stringApproaches[randomApproach[approachCounter]];
                approachCounter++;
                participantKnown = false;

            }
            else if (approachCounter == 3)
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
            if (textOn == true)
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
                counter = counter % Models.Length;

                print("counter:  " + counter);

                if (counter == 0)
                {
                    System.Random r = new System.Random();
                    random = random.OrderBy(x => r.Next()).ToArray();

                }
                currentModel = random[counter];
                counter++;

                //print("counter " + counter);
                //print("Model Number " + random[counter]);

                Models[currentModel].SetActive(true);
                print("transform position: " + transform.position);
                //Models[currentModel].transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                //Models[currentModel].transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);


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
            targetLocationCounter = targetLocationCounter % targetLocations.Length;
            print("target counter: " + targetLocationCounter);

            if (targetLocationCounter == 0)
            {
                randomLocationTarget = randomLocationTarget.OrderBy(x => rt.Next()).ToArray();
            }

            currentLocationTarget = randomLocationTarget[targetLocationCounter];
            targetLocationCounter++;

            //targetRotationCounter = targetRotationCounter % targetLocations.Length;
            //print("target counter: " + targetRotationCounter);

            //if (targetRotationCounter == 0)
            //{
            //    randomRotationTarget = randomRotationTarget.OrderBy(x => rt.Next()).ToArray();
            //}

            //currentRotationTarget = randomRotationTarget[targetRotationCounter];
            //targetRotationCounter++;


            print("handconroller position: " + handController.transform.position);

            targetLocation = targetLocations[currentLocationTarget];
            targetRotation = targetRotations[currentLocationTarget];
            //rotationQTarget = Quaternion.Euler(targetRotation);


            Color color = Targets[currentModel].GetComponentInChildren<Renderer>().material.color;

            color.a = 0.5f;

            Targets[currentModel].SetActive(true);

            Targets[currentModel].GetComponentInChildren<Renderer>().material.SetColor("_Color", color);

            Targets[currentModel].transform.localPosition = targetLocation;



            print("target rotation: " + targetRotation);
            //Targets[currentModel].transform.rotation = rotationQTarget;
            Targets[currentModel].transform.localEulerAngles = targetRotation;
            print("target location: " + targetLocation);
            print("target rotation: " + targetRotation);


        }
        if (Input.GetKeyDown("n") && modelOn == true && timerOn == false)
        {
            timerOn = true;
            locationStart = new Vector3(parentHand.transform.position.x, parentHand.transform.position.y, parentHand.transform.position.z);
            timer = 0f;
        }

        if (timerOn == true)
        {  
            timer += Time.deltaTime;
            locationEnd = new Vector3(parentHand.transform.position.x, parentHand.transform.position.y, parentHand.transform.position.z);
            locationDiff = targetLocation - locationEnd;
            locationDiffCM = new Vector3((locationDiff.x / 31.78f) * 100f, (locationDiff.y / 31.78f) * 100f, (locationDiff.z / 31.78f) * 100f);
            //rotationEnd = new Vector3(parentHand.transform.localEulerAngles.x, parentHand.transform.localEulerAngles.y, parentHand.transform.localEulerAngles.z);
            rotationEnd = new Vector3(parentHand.transform.eulerAngles.x, parentHand.transform.eulerAngles.y, parentHand.transform.eulerAngles.z);

            //QrotationEnd = new Quaternion(parentHand.transform.rotation.x, parentHand.transform.rotation.y, parentHand.transform.rotation.z, parentHand.transform.rotation.w);
            angleDiff();
            //rotationDiff = targetRotation - rotationEnd;
        }

        if (Input.GetKeyDown("k") && modelOn == true && timerOn == true)
        {
            timerOn = false;
            //locationEnd = new Vector3(parentHand.transform.position.x, parentHand.transform.position.y, parentHand.transform.position.z);
            //locationDiff = targetLocation - locationEnd;
            //rotationEnd = new Vector3(parentHand.transform.localEulerAngles.x, parentHand.transform.localEulerAngles.y, parentHand.transform.localEulerAngles.z);
            //rotationDiff = targetRotation - rotationEnd;
            makeStrings();

            saveToFile();
        }


        makeStrings();
        //OnGUI();
    }
    //Loads string variable with expression array

    void angleDiff()
    {
        float xdiff;
        float ydiff;
        float zdiff;

        if(rotationEnd.x < targetRotation.x - 180)
        {
            xdiff = 360 - targetRotation.x + rotationEnd.x;
        }
        else
        {
            xdiff = rotationEnd.x - targetRotation.x;
        }

        if (rotationEnd.y < targetRotation.y - 180)
        {
            ydiff = 360 - targetRotation.y + rotationEnd.y;
        }
        else
        {
            ydiff = rotationEnd.y - targetRotation.y;
        }

        if (rotationEnd.z < targetRotation.z - 180)
        {
            zdiff = 360 - targetRotation.z + rotationEnd.z;
        }
        else
        {
            zdiff = rotationEnd.z - targetRotation.z;
        }


        rotationDiff = new Vector3(xdiff, ydiff, zdiff); 
    }




    void makeStrings()
    {


        //stringApproach = ;

        //fileName = "Assets/textFiles/test_1.txt";

        stringParticipantNumber = "Participant Number: " + participantNumber.ToString("N");

        stringCurrentModel = "Current Model: " + stringModels[currentModel];

        stringTargetLocation = "Target Location: " + targetLocation.ToString("N");


        stringTargetRotation = "Target Rotation: " + targetRotation.ToString("N");

        stringLocationStart = "Location Start: " + locationStart.ToString("N");

        stringLocationEnd = "Location End: " + locationEnd.ToString("N");

        stringRotationEnd = "Rotation End: " + rotationEnd.ToString("N");

        stringLocationDiff = "Location Diff: " + locationDiff.ToString("N");

        stringLocationDiffCM = "Location Diff CM" + locationDiffCM.ToString("N");

        stringRotationDiff = "Rotation Diff: " + rotationDiff.ToString("N");

        stringTimer = "Timer: " + timer.ToString("N");

        //fileName = "Assets/textFiles/test_" + participantNumber;
        //fileName += ".txt";
        //System.IO.File.WriteAllText(fileName, s);



        //WriteFile();
        //participantNumber++;
    }

    void saveToFile()
    {
        System.IO.File.AppendAllText(fileName, stringParticipantNumber + System.Environment.NewLine);
        System.IO.File.AppendAllText(fileName, stringApproach + System.Environment.NewLine);
        System.IO.File.AppendAllText(fileName, stringCurrentModel + System.Environment.NewLine);
        System.IO.File.AppendAllText(fileName, stringTargetLocation + System.Environment.NewLine);
        System.IO.File.AppendAllText(fileName, stringTargetRotation + System.Environment.NewLine);
        System.IO.File.AppendAllText(fileName, stringLocationStart + System.Environment.NewLine);
        System.IO.File.AppendAllText(fileName, stringLocationEnd + System.Environment.NewLine);
        System.IO.File.AppendAllText(fileName, stringRotationEnd + System.Environment.NewLine);
        System.IO.File.AppendAllText(fileName, stringLocationDiff + System.Environment.NewLine);
        System.IO.File.AppendAllText(fileName, stringLocationDiffCM + System.Environment.NewLine);
        System.IO.File.AppendAllText(fileName, stringRotationDiff + System.Environment.NewLine);
        System.IO.File.AppendAllText(fileName, stringTimer + System.Environment.NewLine);
        System.IO.File.AppendAllText(fileName, " " + System.Environment.NewLine);


    }


    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 250, 250), 
            stringParticipantNumber + "\n" +
            stringApproach + "\n" +
            stringCurrentModel + "\n" +
            "Target Number: " + targetLocationCounter + "\n" +
            stringTargetLocation + "\n" +
            stringTargetRotation + "\n" +
            stringLocationStart + "\n" +
            stringLocationEnd + "\n" +
            stringRotationEnd + "\n" +
            stringLocationDiff + "\n" +
            stringLocationDiffCM + "\n" +
            stringRotationDiff + "\n" +
            stringTimer
            );

        //stringParticipantNumber = "Participant Number: " + participantNumber.ToString("N");

        //stringTargetLocation = "Target Location: " + targetLocation.ToString("N");


        //stringTargetRotation = "Target Rotation: " + targetRotation.ToString("N");

        //stringLocationStart = "Location Start: " + locationStart.ToString("N");

        //stringLocationEnd = "Location End: " + locationEnd.ToString("N");

        //stringRotationEnd = "Rotation End: " + rotationEnd.ToString("N");

        //stringLocationDiff = "Location Diff: " + locationDiff.ToString("N");

        //stringRotationDiff = "Rotation Diff: " + rotationDiff.ToString("N");

        //stringTimer = "Timer: " + timer.ToString("N");

    }


    void fileExist()
    {

        while (participantKnown == false)
        {
            fileName = "Assets/textFiles/test_" + participantNumber;
            fileName += ".txt";
            if (System.IO.File.Exists(fileName))
            {
                Debug.Log(fileName + " already exists.");
                participantNumber++;
            }
            else
            {
                participantKnown = true;
            }
        }
    }

    void updateAngles()
        {
            bendValue = Arduino.currentVals[0];
            qW = Arduino.currentVals[6];
            qY = Arduino.currentVals[7];
            qX = Arduino.currentVals[8];
            qZ = Arduino.currentVals[9];
        }
    //void RotateObject(float Value)
    //{


    //    zAngle = (Value - 511.5f) * 0.352f;

    //    float zAngleChange = zAngle - zAnglePrev;

    //    Models[random[targetCounter]].transform.Rotate(zAngleChange, 0, 0, Space.Self);
    //    zAnglePrev = zAngle;
    //}

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
