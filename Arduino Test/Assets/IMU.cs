using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMU : MonoBehaviour {
    private GeneralRotate bend;
    private JohnArduinoManager Arduino;
    Quaternion imucap;
    public GameObject quant;
    public GameObject angle;

    private float zAngleR = 0f;
    private float zAngleL = 0f;


    private float zAnglePrevR = 0f;
    private float zAnglePrevL = 0f;

    private float bendValueL = 0f;
    private float bendValueR = 0f;

    public GameObject Rotate;
    public GameObject InvRotate;
    public GameObject cylinder;

    private float qW = 0;
    private float qY = 0;
    private float qX = 0;
    private float qZ = 0;

    Quaternion imuinter;

    private int callibrate = 1;

    float zCap = 0;
    float lowPassFactor = 0.2f; //Value should be between 0.01f and 0.99f. Smaller value is more damping.
    bool init = true;
    

    private bool align = false;
    //private int zeroToggle = 0;
    void Awake()
    {
        bend = angle.GetComponent<GeneralRotate>();
        Arduino = quant.GetComponent<JohnArduinoManager>();

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        bendValueL = bend.zangle;
        //bendValueR = Arduino.currentVals[5];
        //print("bendValueL: " + bendValueL);


        qW = Arduino.currentVals[6];
        qY = Arduino.currentVals[7];
        qX = Arduino.currentVals[8];
        qZ = Arduino.currentVals[9];


    // Quaternion imu = new Quaternion(qW, qY, qZ, qX);
    Quaternion imu = new Quaternion(qX, qZ, qY, qW);

        if (Input.GetKeyDown("x"))
        {
            callibrate++;
            callibrate = callibrate % 2;
            //imucap = new Quaternion(qW, qY, qZ, qX); ;
            imucap = new Quaternion(qX, qZ, qY, qW);

        }

        

        //if (callibrate == 0)
        //{
        //    align = true;
        //    cylinder.SetActive(false);
        //}

        //if (callibrate == 1)
        //{
        //    align = false;
        //    cylinder.SetActive(true);
        //}

        
        //Quaternion imu = new Quaternion(qX, qY, qZ, qW);
        Quaternion centre = Quaternion.identity;

        if (Input.GetKeyDown("z"))
        {
            zCap = bendValueL;
        }



        if (align == false)
        {
            Rotate.transform.localRotation = Quaternion.identity;
            InvRotate.transform.localRotation = Quaternion.identity;
            //Centre.transform.localRotation = Quaternion.identity;
        }


         if(align == true)
        {
            Quaternion newimu = imu * Quaternion.Inverse(imucap);

            //transform.rotation = Quaternion.Inverse(lowPassFilterQuaternion(imuinter, newimu, lowPassFactor, init));


            init = false;
            zAngleL = bendValueL;
            //zAngleL = (bendValueL - 255) * (-0.53f);
            //zAngleR = (bendValueR - 255) * (-0.53f);

            //RotateObject(bendValue);
            //transform.Rotate(0, 180, 0, Space.World);

            //    zAngle = (bendValueL - zCap) * 0.352f * 0.666f;

            //float zAngleChangeR = zAngleR - zAnglePrevR;
            float zAngleChangeL = zAngleL - zAnglePrevL;
            //print("zAngleChangeL: " + zAngleChangeL);
            //print("zAngleL: " + zAngleL);
            //print("zCap: " + zCap);
            //transform.Rotate(0, 0, -zAngleChangeR, Space.Self);

           // //transform.Rotate(0, 0, zAngleL, Space.Self);

            //transform.Rotate(0, 0, zAngleChange/2, Space.Self);
            //Quaternion offSet = Quaternion.Euler(0, 0,-zAngle/2);

            //Quaternion offSet = Quaternion.Euler(0, 0, 0);

            //transform.rotation = Quaternion.Inverse(offSet);
            //Quaternion finalimu = newimu * Quaternion.Inverse(offSet);
            //transform.localRotation = Quaternion.Inverse(finalimu);

            //transform.localRotation = Quaternion.Inverse(newimu);

            //transform.localRotation = Quaternion.Inverse(imu);
            zAnglePrevL = zAngleL;
            //zAnglePrevR = zAngleR;

        }


    }

    //void RotateObject(float Value)
    //{

    //    zAngle = (Value - 511.5f) * 0.352f;

    //    float zAngleChange = zAngle - zAnglePrev;

    //    transform.Rotate(zAngleChange, 0, 0, Space.Self);
    //    transform.Rotate(-zAngleChange, 0, 0, Space.Self);
    //    zAnglePrev = zAngle;
    //}
    Quaternion lowPassFilterQuaternion(Quaternion intermediateValue, Quaternion targetValue, float factor, bool init)
    {

        //intermediateValue needs to be initialized at the first usage.
        if (init)
        {
            intermediateValue = targetValue;
        }

        intermediateValue = Quaternion.Lerp(intermediateValue, targetValue, factor);
        return intermediateValue;
    }
}
