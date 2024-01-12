using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidBody;
    public int jumpForce = 10;
    public int speed = 2;
    public int rotSpeed = 1;
    public float time = 2.0f;
    public float timer = 2.0f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {  
        if(timer >= time)
        {
            if (Input.GetKeyDown("space"))
            {
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                timer = 0;
            }

        }
        else
            timer += Time.deltaTime;

        if(Input.GetKey(KeyCode.W))
        {
            rigidBody.AddForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidBody.AddForce(Vector3.back * speed * Time.deltaTime, ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.A))
        {
            rigidBody.AddTorque(Vector3.down * rotSpeed * Time.deltaTime, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddTorque(Vector3.up * rotSpeed * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
