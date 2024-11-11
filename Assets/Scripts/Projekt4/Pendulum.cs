using System;
using Unity.Collections;
using UnityEngine;

public class Pendulum : MonoBehaviour
{

    LineRenderer Arm;
    GameObject Pivot;


    public float Damping;

    float AngularVelocity = 0;

    float r;

    float g = -9.82f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Arm = GetComponent<LineRenderer>();   
       Pivot = GameObject.Find("Front_Crane");

       r = Vector3.Distance(transform.position, Pivot.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Arm.SetPosition(0, transform.position);
        Arm.SetPosition(1, Pivot.transform.position);

        float DeltaX = transform.position.x - Pivot.transform.position.x;
        float Angle = Mathf.Asin(DeltaX / r);

        float AngularAccelearation = (Mathf.Sin(Angle) * g) / r;
        AngularVelocity += AngularAccelearation * Time.deltaTime;
        AngularVelocity *= Damping;

        Angle += AngularVelocity * Time.deltaTime;
        float x = Mathf.Sin(Angle) * r + Pivot.transform.position.x;
        float y = -Mathf.Cos(Angle) * r + Pivot.transform.position.y;

        transform.position = new Vector3(x, y, transform.position.z);


        //Debug.Log(Angle * Mathf.Rad2Deg);
    }
}
