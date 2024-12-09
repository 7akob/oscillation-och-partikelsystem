using UnityEngine;
using System.Collections.Generic;

public class Vertex
{
    public int MeshIndex;
    public Vector3 Position;
    public Vector3 Velocity;
    public int leftIndex, rightIndex, topIndex, bottomIndex;

    public Vertex(int m, Vector3 p)
    {
        MeshIndex = m;
        Position = p;
        Velocity = Vector3.zero;

        leftIndex = MeshIndex - 1;
        rightIndex = MeshIndex + 1;
        topIndex = MeshIndex - 11;
        bottomIndex = MeshIndex + 11;
    }
}

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


    public Vertex[] VertexPoints;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        originalVertices = mesh.vertices;
        modifiedVertices = (Vector3[])originalVertices.Clone();
        velocities = new Vector3[originalVertices.Length];

        VertexPoints = new Vertex[81];



        InitiateVertexPoints();
    }

    void Update()
    {
        for (int i = 0; i < VertexPoints.Length; i++)
        {
            Vertex vertex = VertexPoints[i];
            Vector3 totalForce = new Vector3(0, gravity, 0);

            AddSpringForce(vertex, vertex.leftIndex, ref totalForce);
            AddSpringForce(vertex, vertex.rightIndex, ref totalForce);
            AddSpringForce(vertex, vertex.topIndex, ref totalForce);
            AddSpringForce(vertex, vertex.bottomIndex, ref totalForce);

            vertex.Velocity += totalForce * timeStep;
            vertex.Velocity *= damping;
            vertex.Position += vertex.Velocity * timeStep;

            modifiedVertices[vertex.MeshIndex] = vertex.Position;
        }

        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals();
    }

    void AddSpringForce(Vertex vertex, int neighborIndex, ref Vector3 totalForce)
    {
        if (neighborIndex < 0 || neighborIndex >= originalVertices.Length) return;

        Vector3 neighborPos = modifiedVertices[neighborIndex];
        Vector3 direction = neighborPos - vertex.Position;
        float currentDistance = direction.magnitude;
        float restDistance = (originalVertices[vertex.MeshIndex] - originalVertices[neighborIndex]).magnitude;

        totalForce += springStrength * (currentDistance - restDistance) * direction.normalized;
    }

    void InitiateVertexPoints()
    {
        for (int i = 0, j = 0; i < originalVertices.Length; i++)
        {
            if (i % 11 == 0 || i % 11 == 10 || i < 11 || i >= originalVertices.Length - 11)
                continue;

            VertexPoints[j] = new Vertex(i, originalVertices[i]);
            j++;
        }
    }
}
