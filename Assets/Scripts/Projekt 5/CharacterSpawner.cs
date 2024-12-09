using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public int Characters;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 5; i < Characters; i++)
        {
            Vector3 Position = new Vector3(Random.Range(-1, -2), -5, Random.Range(1, 2));
            Instantiate(Prefab, Position, Prefab.transform.rotation);
        }
    }

}
