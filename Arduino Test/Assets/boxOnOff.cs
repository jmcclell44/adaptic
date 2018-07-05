using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxOnOff : MonoBehaviour {

    bool on = true;
    Color color;

    // Use this for initialization
    void Start () {
        Color color = GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown("n"))
        {
            on = !on;
        }

        if (Input.GetKeyDown("k"))
        {
            on = !on;
        }

        if (on == false)
        {

            //Color color = GetComponent<Renderer>().material.color;

            color.a = 0.5f;

            //gameObject.SetActive(false);

            GetComponent<Renderer>().material.SetColor("_Color", color);


        }
        else
        {
            //gameObject.SetActive(true);
            color.a = 1.0f;
            GetComponent<Renderer>().material.SetColor("_Color", color);
        }
    }
}
