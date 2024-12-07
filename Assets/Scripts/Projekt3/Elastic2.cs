using UnityEngine;
using System.Collections.Generic;

public class Elastic2 : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector3[] originalVertices;
    public float elasticity = 5f;
    Vector3[] velocities;

    public float damping = 0.99f;

    HashSet<int> cornerIndices = new HashSet<int>{0, 9, 90, 99};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        originalVertices = (Vector3[])vertices.Clone();
        velocities = new Vector3[vertices.Length];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i = 0; i < vertices.Length; i++)
        {
            if(cornerIndices.Contains(i)) 
            continue;

            Vector3 elasticForce = (originalVertices[i] - vertices[i]) * elasticity;
            velocities[i] += elasticForce * Time.deltaTime;

            vertices[i] += velocities[i] * Time.deltaTime;

            velocities[i] *= damping;

        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }
}
