using UnityEngine;
using System.Collections;

public class SixenseObjectController : MonoBehaviour {

     SixenseHands			Hand;
    bool rightHand = true;

	public Vector3				Sensitivity = new Vector3( 0.01f, 0.01f, 0.01f );

    float m_sensitivity = 0.001f; // Sixense units are in mm

    protected bool				m_enabled = false;
	protected Quaternion		m_initialRotation;
	protected Vector3			m_initialPosition;
	protected Vector3			m_baseControllerPosition;

    float scale = 31.45f;
    int callibrateRight = 0;
    Quaternion imucap;
    bool align = false;
    public GameObject cylinder;

    private GeneralRotate bend;
    public GameObject angle;
    private float zAngleL = 0f;
    private float zAnglePrevL = 0f;
    private float bendValueL = 0f;
    float zCap = 0;

    bool invert = false;


    void Awake()
    {
        //bend = angle.GetComponent<GeneralRotate>();
    }



    // Use this for initialization
    protected virtual void Start() 
	{
		m_initialRotation = this.gameObject.transform.localRotation;
		m_initialPosition = this.gameObject.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKeyDown("i"))
        {
            invert = !invert;
        }

        if (Input.GetKeyDown("p"))
        {
            rightHand = !rightHand;

 

        }

        if (rightHand == true)
        {
            Hand = SixenseHands.RIGHT;
            //m_baseControllerPosition = new Vector3(-2.36f, -2.99f, -8.9f);

        }
        else if (rightHand == false)
        {
            Hand = SixenseHands.LEFT;
            //m_baseControllerPosition = new Vector3(1.04f, -0.21f, -10.57f);
        }

        if ( Hand == SixenseHands.UNKNOWN )
		{
			return;
		}
		
		SixenseInput.Controller controller = SixenseInput.GetController( Hand );
		if ( controller != null && controller.Enabled )  
		{		
			UpdateObject(controller);
		}	
	}
	
	
	void OnGUI()
	{
		if ( !m_enabled )
		{
			GUI.Box( new Rect( Screen.width / 2 - 100, Screen.height - 40, 200, 30 ),  "Press Start To Move/Rotate" );
		}
	}
	
	
	protected virtual void UpdateObject(  SixenseInput.Controller controller )
	{
		if ( controller.GetButtonDown( SixenseButtons.START ) )
		{
			// enable position and orientation control
			m_enabled = !m_enabled;
			
			// delta controller position is relative to this point
			//m_baseControllerPosition = new Vector3(
   //                                                 //controller.Position.x * Sensitivity.x,
   //                                                 //controller.Position.y * Sensitivity.y,
   //                                                 //controller.Position.z * Sensitivity.z
   //                                                 //SixenseInput.Controller.Position.x * Sensitivity.x,
   //                                                 //controller.Position.y * Sensitivity.y,
   //                                                 //controller.Position.z * Sensitivity.z
   //                                                 //-2.36f, -2.99f, -8.9f
   //                                                 1.04f, -0.21f, -10.57f
   //                                                 );
			
			// this is the new start position
			m_initialPosition = this.gameObject.transform.localPosition;
		}
		
		if ( m_enabled )
		{
            UpdateRotation(controller);

            UpdatePosition( controller );
            //offset(controller);
        }
    }
	
	
	protected void UpdatePosition( SixenseInput.Controller controller )
	{
		Vector3 controllerPosition = new Vector3(
                                                    controller.Position.x * Sensitivity.x,
                                                    controller.Position.y * Sensitivity.y,
                                                    controller.Position.z * Sensitivity.z
                                                    //transform.position.x * Sensitivity.x,
                                                    //transform.position.y * Sensitivity.y,
                                                    //transform.position.z * Sensitivity.z
                                                    );

        // distance controller has moved since enabling positional control
        Vector3 vDeltaControllerPos = controllerPosition  - m_baseControllerPosition;

        // update the localposition of the object
        //this.gameObject.transform.localPosition = m_initialPosition + vDeltaControllerPos;

        if (callibrateRight == 1)
        {
            this.gameObject.transform.position = (controller.Position) * m_sensitivity * scale - m_baseControllerPosition;
        }
        else if (callibrateRight == 0)
        {
            this.gameObject.transform.position = (controller.Position) * m_sensitivity * scale;
        }

        if (invert == true)
        {
            this.gameObject.transform.position = -1 * this.gameObject.transform.position;
        }
    }


    //protected void UpdateRotation( SixenseInput.Controller controller )
    //{
    //	this.gameObject.transform.rotation = controller.Rotation * m_initialRotation;
    //   }

    protected void UpdateRotation(SixenseInput.Controller controller)
    { 

    //if (hand.m_hand == SixenseHands.RIGHT) {
            


                if (Input.GetKeyDown("x"))
                {
                    callibrateRight++;
                    callibrateRight = callibrateRight % 2;
                    imucap = controller.Rotation;
            m_baseControllerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
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
                    Quaternion newRotation = controller.Rotation * Quaternion.Inverse(imucap);
                //print("hi");
                this.gameObject.transform.localRotation = newRotation;
            //this.gameObject.transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
                }

     }

    protected void offset(SixenseInput.Controller controller)
    {

        bendValueL = bend.zangle;


        if (Input.GetKeyDown("z"))
        {
            zCap = bendValueL;
        }

        if (align == true)
        {
            zAngleL = bendValueL;

            float zAngleChangeL = zAngleL - zAnglePrevL;


            this.gameObject.transform.Rotate(0, 0, zAngleL, Space.Self);
            zAnglePrevL = zAngleL;
        }
    }
}
