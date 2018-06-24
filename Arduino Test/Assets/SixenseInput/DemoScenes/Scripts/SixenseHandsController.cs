using UnityEngine;
using System.Collections;

public class SixenseHandsController : MonoBehaviour
{

    //public GameObject trackedSegment;
    Vector3 trackedLocation;
    public GameObject device;
    SixenseHand[] m_hands;

    Vector3 m_baseOffset;
    float m_sensitivity = 0.001f; // Sixense units are in mm
    bool m_bInitialized;


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
            hand.transform.localPosition = (hand.m_controller.Position - m_baseOffset - Offset()) * m_sensitivity;
            //hand.transform.rotation = hand.m_controller.Rotation * hand.InitialRotation;
        }

        else
        {
            // use the inital position and orientation because the controller is not active
            hand.transform.localPosition = hand.InitialPosition;
            hand.transform.localRotation = hand.InitialRotation;
        }
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

        double x;
        double y;

        x = 0.068 * Mathf.Sin(angA);
        y = 0.068 * Mathf.Sin(angA);

        float floatY = (float)y;
        float floatX = (float)x;
        Offset = new Vector3(0, -floatY, -floatX);

        return Offset;
    }

}

