using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    private Transform zombietr;
    private Transform playertr;
    private NavMeshAgent nvAgent;
    private float nowTime=0;
    public AudioSource ghost;
    public float runspeed = 30f;
    public float walkRadius = 3f;
    Vector3 randomDirection;
    private bool isPlayer = false;
    private Vector3[] points;
    private int i = 4;
   
    void Start()
    {
        ghost = GetComponent<AudioSource>();
        zombietr = gameObject.GetComponent<Transform>();
        playertr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = gameObject.GetComponent<NavMeshAgent>();

        points = new Vector3[5];
        points.SetValue(new Vector3((float)-64.1, 0, (float)36.3), 0);
        points.SetValue(new Vector3((float)-64.1, 0, (float)-34.4), 1);
        points.SetValue(new Vector3(42, 0, (float)-34.4), 2);
        points.SetValue(new Vector3(60, 0, 35), 3);
        points.SetValue(new Vector3(0, 0, 0), 4);
        nvAgent.destination = points[i];
    }

    void Update()
    {
        if(nowTime + 30 < Time.time && !isPlayer)
        {
            i = Random.Range(0, 5);
            nvAgent.destination = points[i];
            nowTime = Time.time;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer = true;
            Debug.Log("on");
            nvAgent.destination = playertr.position;
            //nvAgent.Stop(false);
            //nvAgent.Resume();
            //ghost.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("stay");
            nvAgent.destination = playertr.position;
            //nvAgent.Stop(false);
            //nvAgent.Resume();
            //ghost.UnPause();
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("off");
        //nvAgent.Stop(true);
        //ghost.Pause();
        isPlayer = false;
    }
}
