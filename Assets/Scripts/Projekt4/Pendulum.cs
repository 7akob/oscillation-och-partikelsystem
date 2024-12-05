using System;
using Unity.Collections;
using UnityEngine;

public class Pendulum : MonoBehaviour
{

    LineRenderer Arm;
    GameObject Pivot;

    public float Damping;

    public float elevatorSpeed = 2f;
    public float minY = 1f;
    public float maxY = 11f;

    float AngularVelocityX = 0f;
    float AngularVelocityZ = 0f;


    float r;
    float g = -9.82f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Arm = GetComponent<LineRenderer>();   
       Pivot = GameObject.Find("Pivot");
       r = Vector3.Distance(transform.position, Pivot.transform.position);

     
    }

    // Update is called once per frame
    void Update()
    {

        float newY = transform.position.y;
        if(Input.GetKey(KeyCode.W))
        {
            newY += elevatorSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S))
        {
            newY -= elevatorSpeed * Time.deltaTime;
        }

        newY = Mathf.Clamp(newY, minY, maxY);

        Arm.SetPosition(0, transform.position);
        Arm.SetPosition(1, Pivot.transform.position);
        

        
        //x kalkylationer
        float DeltaX = transform.position.x - Pivot.transform.position.x;
        float AngleX = Mathf.Asin(Mathf.Clamp(DeltaX / r, -1f, 1f));

        float AngularAccelearationX = (Mathf.Sin(AngleX) * g) / r;
        AngularVelocityX += AngularAccelearationX * Time.deltaTime;
        AngularVelocityX *= Damping;

        AngleX += AngularVelocityX * Time.deltaTime;

        //z kalkylationer
        float DeltaZ = transform.position.z - Pivot.transform.position.z;
        float AngleZ = Mathf.Asin(Mathf.Clamp(DeltaZ / r, -1f, 1f));
        float AngularAccelearationZ = (Mathf.Sin(AngleZ) * g) / r;
        AngularVelocityZ += AngularAccelearationZ * Time.deltaTime;
        AngularVelocityZ *= Damping;
        AngleZ += AngularVelocityZ * Time.deltaTime;

        //Uppdaterar positionen
        float x = Mathf.Sin(AngleX) * r + Pivot.transform.position.x;
        float y = -Mathf.Cos(AngleX) * r + Pivot.transform.position.y;
        float z = Mathf.Sin(AngleZ) * r + Pivot.transform.position.z;
        transform.position = new Vector3(x, newY, z);

    }
}
