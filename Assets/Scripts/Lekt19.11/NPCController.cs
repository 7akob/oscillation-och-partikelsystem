using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    NavMeshAgent Agent;
    GameObject WayPoint, WayPoint2;
    Animator Anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        WayPoint = GameObject.Find("Waypoint");
        WayPoint2 = GameObject.Find("Waypoint2");
        Agent.SetDestination(WayPoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Agent.remainingDistance > Agent.stoppingDistance)
        {
            Anim.SetBool("Walking", true);
        }
        else
        {
            Anim.SetBool("Walking", false);
            StartCoroutine(DelayedFunctionCall(Random.Range(2, 6)));
        }
    }

    IEnumerator DelayedFunctionCall(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetNextDestination();
    }

    void SetNextDestination()
    {
        Agent.SetDestination(WayPoint2.transform.position);

    }
}
