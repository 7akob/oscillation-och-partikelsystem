using UnityEngine;

public class Movement : MonoBehaviour
{
 public float Speed;
    public float RotSpeed;
    public float Rot2Speed;

    GameObject Arm;

    // Start is called before the first frame update
    void Start()
    {
        Arm = GameObject.Find("Front_Crane");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow) == true)
        {
            transform.Translate(Speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            transform.Translate(-Speed * Time.deltaTime,0,0);
        }
        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            transform.Rotate(0, -RotSpeed *Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            transform.Rotate(0, RotSpeed * Time.deltaTime, 0);

        }
        if (Input.GetKey(KeyCode.W) == true)
        {
            Arm.transform.Rotate(0,0, -Rot2Speed * Time.deltaTime, Space.Self);

        }
        if (Input.GetKey(KeyCode.S) == true)
        {
            Arm.transform.Rotate(0, 0, Rot2Speed * Time.deltaTime, Space.Self);

        }
    }
}
