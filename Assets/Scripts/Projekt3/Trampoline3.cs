using UnityEngine;

public class Trampoline3 : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector3[] initialVertices; // Store initial positions
    Vector3[] velocities;      // Per-vertex velocities

    public float Mass = 1f;
    public float K = 10f; // Spring constant
    public float Damping = 0.99f;
    private float g = -9.82f; // Gravity

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        initialVertices = mesh.vertices.Clone() as Vector3[];
        velocities = new Vector3[vertices.Length];
    }

    void Update()
    {
        ApplySpringForce();
        DeformMesh();
    }

    void ApplySpringForce()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            // Calculate displacement from initial position
            Vector3 displacement = initialVertices[i] - vertices[i];

            // Hooke's law: F = -kx
            Vector3 springForce = displacement * K;

            // Add gravity force
            Vector3 gravityForce = new Vector3(0, Mass * g, 0);

            // Total force
            Vector3 totalForce = springForce + gravityForce;

            // Acceleration: a = F/m
            Vector3 acceleration = totalForce / Mass;

            // Update velocity
            velocities[i] += acceleration * Time.deltaTime;

            // Apply damping
            velocities[i] *= Damping;

            // Update vertex position
            vertices[i] += velocities[i] * Time.deltaTime;
        }
    }

    void DeformMesh()
    {
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals(); // Recalculate normals for proper shading
    }
}

