using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    private Animator _animator;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Player")
        {
            Debug.Log("1");
            _animator.SetBool("open", true);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
