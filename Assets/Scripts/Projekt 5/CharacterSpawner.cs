using UnityEngine;
using System.Collections;
public class CharacterSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public int Characters;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawn_dudes());

    }

    IEnumerator spawn_dudes()
    {
        for(int i = 0; i < Characters; i++)
        {
            yield return new WaitForSeconds(2);
            Vector3 Position = new Vector3(Random.Range(-1, -2), 0, Random.Range(1, 2));
            Instantiate(Prefab, Position, Prefab.transform.rotation);
            print(i);
            
        }
       
    }


}
