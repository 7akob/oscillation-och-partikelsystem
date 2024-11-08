using UnityEngine;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] vertexVelocities;
    private List<Spring> springs = new List<Spring>();  // Lista av fjädrar mellan kopplade vertexer
    private float springConstant = 10f;  // Fjäderkonstant (k)
    private float damping = 0.1f;  // Dämpning för att undvika oändlig rörelse

    void Start()
    {
        // Hämta meshen från plane-objektet
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        vertexVelocities = new Vector3[vertices.Length];

        // Skapa fjädrarna mellan alla kopplade vertexer
        CreateSprings();
    }

    void Update()
    {
        // Applicera fjäderkraften på varje vertex
        ApplySpringForces();
        mesh.vertices = vertices;  // Uppdatera meshen med de nya vertexpositionerna
        mesh.RecalculateNormals();  // Återberäkna normaler så ljuset fungerar korrekt
    }

    // Skapa fjädrarna mellan kopplade vertexer
    void CreateSprings()
    {
        int width = 10;  // Bredden på meshens rutor (anpassa efter din mesh)
        int height = 10; // Höjden på meshens rutor (anpassa efter din mesh)

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int currentIndex = y * width + x;
                
                // Koppla till grannar (vänster, höger, ovan, nedan, diagonalt)
                if (x < width - 1) springs.Add(new Spring(currentIndex, currentIndex + 1));  // Höger
                if (y < height - 1) springs.Add(new Spring(currentIndex, currentIndex + width));  // Nedan
                if (x > 0) springs.Add(new Spring(currentIndex, currentIndex - 1));  // Vänster
                if (y > 0) springs.Add(new Spring(currentIndex, currentIndex - width));  // Ovan
                if (x < width - 1 && y < height - 1) springs.Add(new Spring(currentIndex, currentIndex + width + 1));  // Diagonal nedåt höger
                if (x > 0 && y < height - 1) springs.Add(new Spring(currentIndex, currentIndex + width - 1));  // Diagonal nedåt vänster
                if (x < width - 1 && y > 0) springs.Add(new Spring(currentIndex, currentIndex - width + 1));  // Diagonal uppåt höger
                if (x > 0 && y > 0) springs.Add(new Spring(currentIndex, currentIndex - width - 1));  // Diagonal uppåt vänster
            }
        }
    }

    // Applicera fjäderkraft på alla vertexer
    void ApplySpringForces()
    {
        foreach (Spring spring in springs)
        {
            Vector3 vertexA = vertices[spring.indexA];
            Vector3 vertexB = vertices[spring.indexB];

            // Beräkna avståndet mellan de två vertexerna
            float distance = Vector3.Distance(vertexA, vertexB);

            // Beräkna riktningen och fjäderkraften (Hooke’s lag)
            Vector3 direction = (vertexB - vertexA).normalized;
            float forceMagnitude = springConstant * (distance - 1f);  // 1f är viloläget (ursprungligt avstånd)

            // Fjäderkraften mellan de två vertexerna
            Vector3 force = direction * forceMagnitude;

            // Applicera dämpning och uppdatera positioner och hastigheter
            Vector3 velocityA = vertexVelocities[spring.indexA];
            Vector3 velocityB = vertexVelocities[spring.indexB];

            vertexVelocities[spring.indexA] -= force * Time.deltaTime;  // Uppdatera hastigheten
            vertexVelocities[spring.indexB] += force * Time.deltaTime;

            // Uppdatera positioner baserat på hastigheter
            vertices[spring.indexA] += vertexVelocities[spring.indexA] * Time.deltaTime;
            vertices[spring.indexB] += vertexVelocities[spring.indexB] * Time.deltaTime;
        }
    }
}

// Fjäderklass för att representera en fjäder mellan två index
public class Spring
{
    public int indexA;
    public int indexB;

    public Spring(int indexA, int indexB)
    {
        this.indexA = indexA;
        this.indexB = indexB;
    }
}
