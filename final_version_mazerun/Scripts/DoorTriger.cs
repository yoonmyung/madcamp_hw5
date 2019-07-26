using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriger : MonoBehaviour {
    public Animator _animator;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _animator.SetBool("close", true);
            _animator.SetBool("open", false);
        }
        
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
