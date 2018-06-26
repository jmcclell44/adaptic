using UnityEngine;
using System.Collections;

public class SixenseHandsController : MonoBehaviour
{

    private GeneralRotate bend;
    Quaternion imucap;
    //public GameObject quant;
    //public GameObject angle;

    private float zAngleR = 0f;
    private float zAngleL = 0f;


    private float zAnglePrevR = 0f;
    private float zAnglePrevL = 0f;

    private float bendValueL = 0f;
    private float bendValueR = 0f;

    //public GameObject Rotate;
    //public GameObject InvRotate;
    public GameObject cylinder;

    private float qW = 0;
    private float qY = 0;
    private float qX = 0;
    private float qZ = 0;

    Quaternion imuinter;

    public int callibrateLeft = 0;
    public int callibrateRight = 0;

    float zCap = 0;
    float lowPassFactor = 0.2f; //Value should be between 0.01f and 0.99f. Smaller value is more damping.
    bool init = true;


    private bool align = false;


    //public GameObject trackedSegment;
    Vector3 trackedLocation;
    public GameObject device;
    SixenseHand[] m_hands;


    bool switchController = false;
    bool first = true;

    Quaternion rotation;

    Vector3 offset = new Vector3(0, 0, 500);
    Vector3 zero = new Vector3(0, 0, 0);


    Vector3 m_baseOffset;
    float m_sensitivity = 0.001f; // Sixense units are in mm
    bool m_bInitialized;

    Vector3 locationOffset;
    bool offsetOn = false;
    float scale = 1f;

    // Use this for initialization
    void Start()
    {
        m_hands = GetComponentsInChildren<SixenseHand>();
    }


    // Update is called once per frame
    void Update()
    {
        //trackedLocation = new Vector3(trackedSegment.transform.position.x, trackedSegment.transform.position.y, trackedSegment.transform.position.z);
        //print("Segment Location: " + trackedLocation);


        //if (Input.GetKeyDown("0"))
        //{
        //    switchController = !switchController;
        //}

        //if (Input.GetKeyDown("l"))
        //{
        //    offsetOn = !offsetOn;
        //}

        //if (offsetOn == true)
        //{
        //    locationOffset = Offset();
        //}
        //else
        //{
        //    locationOffset = new Vector3(0, 0, 0);
        //}


        bool bResetHandPosition = false;

        foreach (SixenseHand hand in m_hands)
        {
            if (IsControllerActive(hand.m_controller) && hand.m_controller.GetButtonDown(SixenseButtons.START))
            {
                bResetHandPosition = true;
            }

            if (m_bInitialized)
            {
                
                UpdateHand(hand);
            }
        }

        if (bResetHandPosition)
        {
            m_bInitialized = true;

            m_baseOffset = Vector3.zero;

            // Get the base offset assuming forward facing down the z axis of the base
            foreach (SixenseHand hand in m_hands)
            {
                m_baseOffset += hand.m_controller.Position;
            }

            m_baseOffset /= 2;
        }
    }


    /** Updates hand position and rotation */
    void UpdateHand(SixenseHand hand)
    {
        bool bControllerActive = IsControllerActive(hand.m_controller);

        if (bControllerActive)
        {
            hand.transform.localPosition = (hand.m_controller.Position - m_baseOffset) * m_sensitivity * scale;
            hand.transform.localPosition = hand.transform.localPosition - locationOffset;
            //rotation = hand.m_controller.Rotation * hand.InitialRotation;
            //hand.transform.localRotation = Quaternion.Inverse(rotation);



            if (hand.m_hand == SixenseHands.RIGHT)
            {
                if (Input.GetKeyDown("x"))
                {
                    callibrateRight++;
                    callibrateRight = callibrateRight % 2;
                    imucap = hand.m_controller.Rotation;
                }

                if (callibrateRight == 1)
                {
                    align = true;
                    cylinder.SetActive(false);
                }

                if (callibrateRight == 0)
                {
                    align = false;
                    cylinder.SetActive(true);
                }

                if (align == false)
                {
                    //Rotate.transform.localRotation = Quaternion.identity;
                    //InvRotate.transform.localRotation = Quaternion.identity;
                    //Centre.transform.localRotation = Quaternion.identity;
                }

                if (align == true)
                {
                    Quaternion newRotation = hand.m_controller.Rotation * Quaternion.Inverse(imucap);
                    print("hi");
                    hand.transform.localRotation = newRotation;
                }
            }
            else if (hand.m_hand == SixenseHands.LEFT)
            {
                if (Input.GetKeyDown("c"))
                {
                    callibrateLeft++;
                    callibrateLeft = callibrateLeft % 2;
                    imucap = hand.m_controller.Rotation;
                }

                if (callibrateLeft == 1)
                {
                    align = true;
                    //cylinder.SetActive(false);
                }

                if (callibrateLeft == 0)
                {
                    align = false;
                    //cylinder.SetActive(true);
                }

                if (align == false)
                {
                    //Rotate.transform.localRotation = Quaternion.identity;
                    //InvRotate.transform.localRotation = Quaternion.identity;
                    //Centre.transform.localRotation = Quaternion.identity;
                }

                if (align == true)
                {
                    Quaternion newRotation = hand.m_controller.Rotation * Quaternion.Inverse(imucap);
                    print("hi");
                    hand.transform.localRotation = newRotation;
                }
            }



            //if (switchController == false)
            //{
            //    if (hand.m_hand == SixenseHands.RIGHT)
            //    {
            //        hand.transform.localPosition = (hand.m_controller.Position - m_baseOffset) * m_sensitivity * scale;
            //        hand.transform.localPosition = hand.transform.localPosition - locationOffset;
            //        //hand.transform.rotation = hand.m_controller.Rotation * hand.InitialRotation;
            //    }
            //    else if(hand.m_hand == SixenseHands.LEFT)
            //    {
            //        hand.transform.localPosition = (hand.m_controller.Position - m_baseOffset) * m_sensitivity * scale;
            //        hand.transform.localPosition = hand.transform.localPosition - locationOffset;
            //        hand.transform.localPosition = hand.transform.localPosition - offset;
            //    }
            //}
            //else if (switchController == true)
            //{
            //    if (hand.m_hand == SixenseHands.LEFT)
            //    {
            //        hand.transform.localPosition = (hand.m_controller.Position - m_baseOffset) * m_sensitivity * scale;
            //        hand.transform.localPosition = hand.transform.localPosition - locationOffset;
            //        //hand.transform.rotation = hand.m_controller.Rotation * hand.InitialRotation;
            //    }
            //    else if (hand.m_hand == SixenseHands.RIGHT)
            //    {
            //        hand.transform.localPosition = (hand.m_controller.Position - m_baseOffset) * m_sensitivity * scale;
            //        hand.transform.localPosition = hand.transform.localPosition - locationOffset;
            //        hand.transform.localPosition = hand.transform.localPosition - offset;
            //    }
            //}
        }

        else
        {
            // use the inital position and orientation because the controller is not active
            hand.transform.localPosition = hand.InitialPosition;
            hand.transform.localRotation = hand.InitialRotation;
        }
        //print("hand position: "  hand.transform.localPosition);
    }


    void OnGUI()
    {
        if (!m_bInitialized)
        {
            GUI.Box(new Rect(Screen.width / 2 - 50, Screen.height - 40, 100, 30), "Press Start");
        }
    }


    /** returns true if a controller is enabled and not docked */
    bool IsControllerActive(SixenseInput.Controller controller)
    {
        return (controller != null && controller.Enabled && !controller.Docked);
    }
    Vector3 Offset()
    {
        //GameObject device = GameObject.Find("Gameobject (1)");
        float angA = device.transform.localEulerAngles.z;
        Vector3 Offset;

        double x = 0;
        double y = 0;

        if (angA == 0 || angA == 360 || angA == -360)
        {
            x = 0.068 * scale;
            y = 0;
        }
        else if (angA == 180 || angA == -180)
        {
            x = -0.068 * scale;
            y = 0;
        }
        else if (angA == 90)
        {
            x = 0;
            y = 0.068 * scale;
        }
        else if (angA == -90 || angA == 270)
        {
            x = 0;
            y = -0.068 * scale;
        }
        else
        {
            if (angA > 0 && angA < 90)
            {
                y = 0.068 * scale * Mathf.Sin(angA * Mathf.Deg2Rad);
                x = 0.068 * scale * Mathf.Cos(angA * Mathf.Deg2Rad);
            }
            else if (angA > 90 && angA < 180)
            {
                y = 0.068 * scale * Mathf.Sin(angA * Mathf.Deg2Rad);
                x = 0.068 * scale * Mathf.Cos(angA * Mathf.Deg2Rad);
            }
            else if (angA > 180 && angA < 270)
            {
                y = 0.068 * scale * Mathf.Sin(angA * Mathf.Deg2Rad);
                x = 0.068 * scale * Mathf.Cos(angA * Mathf.Deg2Rad);
            }
            else if (angA > 270 && angA < 360)
            {
                y = 0.068 * scale * Mathf.Sin(angA * Mathf.Deg2Rad);
                x = 0.068 * scale * Mathf.Cos(angA * Mathf.Deg2Rad);
            }

        }

        float floatY = (float)y;
        float floatX = (float)x;
        Offset = new Vector3(-floatX, floatY, 0);

        print("x: " + x);
        print("y: " + y);
        print("angle A: " + angA);

        return Offset;
    }

}

