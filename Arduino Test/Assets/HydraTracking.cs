using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraTracking : MonoBehaviour {
    Vector3 location = new Vector3(0.0f, 1.0f, 0.0f);
    Vector3 newLocation = new Vector3(0.0f, 1.0f, 0.0f);
    Vector3 prevLocation = new Vector3(0.0f, 1.0f, 0.0f);
    bool first;

    // Use this for initialization
    void Start () {
        location = transform.position;
        first = true;
	}

    // Update is called once per frame
    void Update()
    {
        location = transform.position;
        //GameObject guiText = null;

        //guiText = GameObject.Find("gui_text_base_connected");
        //if (guiText)
        //{
        //    guiText.GetComponent<GUIText>().text = "Base Connected = " + SixenseInput.IsBaseConnected(0);
        //}

        for (uint i = 0; i < 2; i++)
        {
            if (SixenseInput.Controllers[i] != null)
            {
                uint controllerNumber = i + 1;

                //guiText = GameObject.Find("gui_text_controller_" + controllerNumber + "_enabled");
                //if (guiText)
                //{
                    //guiText.GetComponent<GUIText>().text = "Enabled = " + SixenseInput.Controllers[i].Enabled;
                //}

                //guiText = GameObject.Find("gui_text_controller_" + controllerNumber + "_docked");
                //if (guiText)
                //{
                    //guiText.GetComponent<GUIText>().text = "Docked = ";
                    //if (SixenseInput.Controllers[i].Enabled)
                    //{
                    //    guiText.GetComponent<GUIText>().text += SixenseInput.Controllers[i].Docked;
                    //}
                //}



                //guiText = GameObject.Find("gui_text_controller_" + controllerNumber + "_position");
                //if (guiText)
                //{
                    //GetComponent<GUIText>().GetComponent<GUIText>().text = "Position = ";
                    if (SixenseInput.Controllers[i].Enabled)
                    {
                    //guiText.GetComponent<GUIText>().text += SixenseInput.Controllers[i].Position;
                    print("position " + 0 + ": " + SixenseInput.Controllers[1].Position);
                    //if (first == true)
                    //{
                    //    prevLocation = SixenseInput.Controllers[0].Position;
                    //}
                    //else if (first == false)
                    //{
                        newLocation = SixenseInput.Controllers[1].Position;
                    //}
                }
                //}

                //guiText = GameObject.Find("gui_text_controller_" + controllerNumber + "_rotation");
                //if (guiText)
                //{
                //guiText.GetComponent<GUIText>().text = "Rotation = ";
                //    if (SixenseInput.Controllers[i].Enabled)
                //    {
                //        guiText.GetComponent<GUIText>().text += SixenseInput.Controllers[i].Rotation;
                //    }
                //}
            }
        }
        if (first == false)
        {
            location += newLocation - prevLocation;
            transform.position = new Vector3(location.x, location.y, location.z);
            print("New Location: " + newLocation);
            print("Prev Location: " + prevLocation);

        }
        else if (first == true)
        {
            if (SixenseInput.Controllers[1].Enabled)
            {
                first = false;
            }
        }
            prevLocation = newLocation;
        
    }
}