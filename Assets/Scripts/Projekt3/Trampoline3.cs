using UnityEngine;
using System.Collections.Generic;

public class Trampoline3 : MonoBehaviour
{
    public float gravity = -9.8f; 
    public float springStrength = 5.0f; 
    public float damping = 0.95f; 
    public float timeStep = 0.02f; 

    private Mesh mesh;
    private Vector3[] originalVertices; 
    private Vector3[] modifiedVertices;
    private Vector3[] velocities; 
    private HashSet<int> cornerIndices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        originalVertices = mesh.vertices;
        modifiedVertices = (Vector3[])originalVertices.Clone();
        velocities = new Vector3[originalVertices.Length];

        cornerIndices = new HashSet<int>(GetCornerIndices(originalVertices));
    }

    void Update()
    {
        for (int i = 0; i < modifiedVertices.Length; i++)
        {
            if (!cornerIndices.Contains(i))
            {
                Vector3 gravityForce = new Vector3(0, gravity, 0);

                Vector3 springForce = (originalVertices[i] - modifiedVertices[i]) * springStrength;

                Vector3 totalForce = gravityForce + springForce;

                velocities[i] += totalForce * timeStep;
                velocities[i] *= damping;

                modifiedVertices[i] += velocities[i] * timeStep;
            }
        }

        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals(); 
    }

    private int[] GetCornerIndices(Vector3[] vertices)
    {
        float minX = Mathf.Min(vertices[0].x, vertices[vertices.Length - 1].x);
        float maxX = Mathf.Max(vertices[0].x, vertices[vertices.Length - 1].x);
        float minZ = Mathf.Min(vertices[0].z, vertices[vertices.Length - 1].z);
        float maxZ = Mathf.Max(vertices[0].z, vertices[vertices.Length - 1].z);

        List<int> indices = new List<int>();

        for (int i = 0; i < vertices.Length; i++)
        {
            if ((vertices[i].x == minX || vertices[i].x == maxX) &&
                (vertices[i].z == minZ || vertices[i].z == maxZ))
            {
                indices.Add(i);
            }
        }

        return indices.ToArray();
    }
}
