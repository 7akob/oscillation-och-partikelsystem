using UnityEngine;

public class WaveSim : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float WaveSpeed = 1.0f;
    public float WaveAmplitude = 1.0f;
    public float WaveFrequency = 1.0f;

    private Vector3[] originalVertices;
    private Mesh mesh;


    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3[] vertices = new Vector3[originalVertices.Length];
        
        
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];
            
            
            vertex.y = Mathf.Sin(Time.time * WaveSpeed + vertex.x * WaveFrequency) * WaveAmplitude;
            
            vertices[i] = vertex;
        }
        
        
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}
