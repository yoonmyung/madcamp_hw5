using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemmanager : MonoBehaviour {

    public GameObject map;
    public GameObject pickupbutton;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pickupitem()
    {
        if(mappickup.isMap)
        {
            Destroy(map);
            pickupbutton.SetActive(false);
        }
        
    }
}
