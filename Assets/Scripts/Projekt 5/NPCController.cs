using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    NavMeshAgent Agent;
    GameObject WayPoint, WayPoint2, WayPoint3, WayPoint4, WayPoint5, WayPoint6, WayPoint7;
    Animator Anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject current_waypoint;

    bool wait = false;
    GameObject[] waypoints = new GameObject [7];
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        WayPoint = GameObject.Find("Waypoint");
        WayPoint2 = GameObject.Find("Waypoint2");
        WayPoint3 = GameObject.Find("Waypoint3");
        WayPoint4 = GameObject.Find("Waypoint4");
        WayPoint5 = GameObject.Find("Waypoint5");
        WayPoint6 = GameObject.Find("Waypoint6");
        WayPoint7 = GameObject.Find("Waypoint7");

        

        waypoints[0]=WayPoint;
        waypoints[1]=WayPoint2;
        waypoints[2]=WayPoint3;
        waypoints[3]=WayPoint4;
        waypoints[4]=WayPoint5;
        waypoints[5]=WayPoint6;
        waypoints[6]=WayPoint7;

        Agent.SetDestination(WayPoint.transform.position);
        current_waypoint = WayPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if(Agent.remainingDistance > Agent.stoppingDistance)
        {
            Anim.SetBool("Walking", true);
        }
        
        else if(wait == false)
        {
            wait = true;
            Anim.SetBool("Walking", false);
            StartCoroutine(DelayedFunctionCall(UnityEngine.Random.Range(1, 3)));
            
            
        }
    }

    IEnumerator DelayedFunctionCall(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetNextDestination();
    }

    void SetNextDestination()
    {
        if(Array.IndexOf(waypoints, current_waypoint)+1 == 6)
        {
            Destroy(this);
            print ("kaka");
        }
        Agent.SetDestination(waypoints[Array.IndexOf(waypoints, current_waypoint)+1].transform.position);
        current_waypoint = waypoints[Array.IndexOf(waypoints, current_waypoint) + 1];
        wait = false;
    }
}
