using System.Runtime.CompilerServices;
using UnityEngine;

public class Trampoline3 : MonoBehaviour
{
    Mesh mesh;

    public float Mass;
    public float SpringConstant;
    public float Damping;
    
    float G = -9.82f;
    float Y0;

    Vector3[] vertices;
    Vector3[] originalVertices;

    Vector3 Velocity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        vertices = (Vector3[])originalVertices.Clone();

        Y0 = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyForces();
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    void ApplyForces()
    {
        Vector3 gravityForce = new Vector3(0, Mass * G, 0);
        for (int i = 0; i < vertices.Length; i++)
        {
            float DeltaY = Y0 - vertices[i].y;
            float springForce = DeltaY * SpringConstant;

            float totalForce = springForce + gravityForce.y;

            float acceleration = totalForce / Mass * Time.deltaTime;

            Velocity.y += acceleration;

            Velocity *= Damping;

            vertices[i] += Velocity * Time.deltaTime;
      
        }
        transform.Translate(Velocity * Time.deltaTime);

    }
}
