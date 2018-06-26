using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchControllers : MonoBehaviour {

    public GameObject handLeft;
    public GameObject handRight;

    //public GameObject number1;
    //public GameObject number2;
    bool switchController = false;
    bool first = true;

    Vector3 offset = new Vector3(0,0,500);
    Vector3 zero = new Vector3(0, 0, 0);
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("0"))
        {



            if (switchController == false)
            {
                //number1.SetActive(true);
                //number2.SetActive(false);


                handLeft.transform.localPosition = handLeft.transform.localPosition + offset;
                if (first == false)
                {
                    handRight.transform.localPosition = handRight.transform.localPosition - offset;
                }
                else if (first == true)
                {
                    first = false;
                }
            }
            else if (switchController == true)
            {
                //number1.SetActive(false);
                //number2.SetActive(true);

                handRight.transform.localPosition = handRight.transform.localPosition + offset;
                handLeft.transform.localPosition = handLeft.transform.localPosition - offset;
            }
            switchController = !switchController;
        }

        if (switchController == false && handLeft.transform.localPosition != zero)
        {
            //number1.SetActive(true);
            //number2.SetActive(false);


            handLeft.transform.localPosition = handLeft.transform.localPosition + offset;
            if (first == false)
            {
                handRight.transform.localPosition = handRight.transform.localPosition - offset;
            }
            else if (first == true)
            {
                first = false;
            }
        }
        else if (switchController == true && handRight.transform.localPosition != zero)
        {
            //number1.SetActive(false);
            //number2.SetActive(true);

            handRight.transform.localPosition = handRight.transform.localPosition + offset;
            handLeft.transform.localPosition = handLeft.transform.localPosition - offset;
        }
    }
}