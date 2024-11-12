using System;
using Unity.Collections;
using UnityEngine;

public class Pendulum : MonoBehaviour
{

    LineRenderer Arm;
    GameObject Pivot;

    public float Damping;

    float AngularVelocityX = 0;
    float AngularVelocityZ = 0;



    float g = -9.82f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Arm = GetComponent<LineRenderer>();   
       Pivot = GameObject.Find("Pivot");

     
    }

    // Update is called once per frame
    void Update()
    {

        Arm.SetPosition(0, transform.position);
        Arm.SetPosition(1, Pivot.transform.position);
        
       float r = Vector3.Distance(transform.position, Pivot.transform.position);

        
        //x
        float DeltaX = transform.position.x - Pivot.transform.position.x;
        float AngleX = Mathf.Asin(DeltaX / r);

        float AngularAccelearationX = (Mathf.Sin(AngleX) * g) / r;
        AngularVelocityX += AngularAccelearationX * Time.deltaTime;
        AngularVelocityX *= Damping;

        AngleX += AngularVelocityX * Time.deltaTime;

        //y
        float DeltaZ = transform.position.z - Pivot.transform.position.z;
        float AngleZ = Mathf.Asin(DeltaZ / r);
        float AngularAccelearationZ = (Mathf.Sin(AngleZ) * g) / r;
        AngularVelocityZ += AngularAccelearationZ * Time.deltaTime;
        AngularVelocityZ *= Damping;
        AngleZ += AngularVelocityZ * Time.deltaTime;

        float x = Mathf.Sin(AngleX) * r + Pivot.transform.position.x;
        float y = -Mathf.Cos(AngleX) * r + Pivot.transform.position.y;
        float z = Mathf.Sin(AngleZ) * r + Pivot.transform.position.z;
        transform.position = new Vector3(x, y, z);


        //Debug.Log(Angle * Mathf.Rad2Deg);
    }
}
