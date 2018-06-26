using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationOffset : MonoBehaviour {
    Vector3 locationOffset;
    bool offsetOn = false;
    float scale = 33.33f;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("l"))
        {
            offsetOn = !offsetOn;
        }

        if (offsetOn == true)
        {
            locationOffset = Offset();
        }
        else
        {
            locationOffset = new Vector3(0, 0, 0);
        }

        transform.localPosition = -locationOffset;
    }

    Vector3 Offset()
    {
        //GameObject device = GameObject.Find("Gameobject (1)");
        float angA = transform.localEulerAngles.z;
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
                y = 0.068 * scale * Mathf.Sin(angA*Mathf.Deg2Rad);
                x = 0.068 * scale * Mathf.Cos(angA*Mathf.Deg2Rad);
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
        Offset = new Vector3(floatX, floatY ,0);

        print("x: " + x);
        print("y: " + y);
        print("angle A: " + angA);

        return Offset;
    }
}
