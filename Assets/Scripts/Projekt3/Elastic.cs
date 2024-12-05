using UnityEngine;

public class Elastic : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;

    public float Mass = 1f;
    public float K = 10f;
    public float Damping = 0.99f;
    float g = -9.82f;
    float Y0;
    Vector3 Velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        Y0 = transform.position.y;
        Velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        ApplySpringForce();

        DeformMesh();

    }
    void ApplySpringForce()
    {
        float Fg = Mass*g;

        for(var i = 0; i < vertices.Length; i++)
        {
            float DeltaY = Y0 - vertices[i].y;
            float Ff = DeltaY * K;

            float Fr = Ff + Fg;

            float acceleration = Fr/Mass * Time.deltaTime;

            Velocity.y += acceleration;

            Velocity *= Damping;

            vertices[i] += Velocity * Time.deltaTime;
        }

    }

    void DeformMesh()
    {
        for(var i = 0; i < vertices.Length; i++)
        {

                vertices[i] += Vector3.down * Time.deltaTime;
        }
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }
}