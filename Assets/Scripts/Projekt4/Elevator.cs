using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed;
    public float maxHeight;
    public float minHeight;

    bool movingUp = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(movingUp)
        {
            if(transform.position.y < maxHeight)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else
            {
                movingUp = false;
            }
        }
        else
        {
            if(transform.position.y > minHeight)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            else
            {
                movingUp = true;
            }
        }
    }
}
