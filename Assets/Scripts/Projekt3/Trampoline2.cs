using UnityEngine;

public class Trampoline2 : MonoBehaviour
{
    public float Mass;
    public float K;
    public float Damping;
    float G = -9.82f;
    float Y0;

    Vector3 Velocity;

    Mesh mesh;
    Vector3[] originalVertices;
    Vector3[] displacedVertices;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Velocity = new Vector3(0, 0, 0);
        Y0 = transform.position.y;

        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float averageY = 0f;
        for(int i = 0; i < originalVertices.Length; i++){
            averageY += displacedVertices[i].y;
        }
        averageY /= originalVertices.Length;

        float DeltaY = Y0 - averageY;
        //Beräknar tyngkraften
        float Fg = Mass * G;

        //Beräknar fjäderkraften
        
        float Ff = DeltaY * K;

        //BEräknar den resulterande kraften
        float Fr = Ff + Fg;

        //Omvandlar resulterande kraften i acceleration
        float acceleration = Fr/Mass;

        //Adderar accelerationen till hastigheten
        Velocity.y += acceleration * Time.deltaTime;

        //Friktion
        Velocity *= Damping;
        
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            displacedVertices[i] = originalVertices[i];  // Copy the original vertex position
            displacedVertices[i].y = + Velocity.y * Time.deltaTime;  // Modify the Y position only
        }

        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals();

        //Adderar hastigheten till positionen
        transform.Translate(Velocity * Time.deltaTime);
    }
}
