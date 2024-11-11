using UnityEngine;

public class Elastic : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        for(var i = 0; i < vertices.Length; i++)
        {
            if((i%2)==0)
                vertices[i] += Vector3.up * Time.deltaTime;    
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }
}
