using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mappickup : MonoBehaviour {

    public GameObject pickupbutton;
    public static bool isMap;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            pickupbutton.SetActive(true);
            isMap = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            pickupbutton.SetActive(false);
            isMap = false;
        }
    }
}
