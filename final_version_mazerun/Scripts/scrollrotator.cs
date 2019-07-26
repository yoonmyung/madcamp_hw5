using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollrotator : MonoBehaviour {

    private Transform trans;
    public float rotatespeed = 50f;

	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        trans.Rotate(new Vector3(0,1,0) * Time.deltaTime * rotatespeed, Space.World);
	}
}
