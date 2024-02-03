using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidBody;
    private GameObject focalPoint;

    public int jumpForce = 10;
    public int speed = 2;
    public int rotSpeed = 1;
    public float time = 2.0f;
    public float timer = 2.0f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
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

        float verticalInput = Input.GetAxis("Vertical");
        this.transform.Translate(verticalInput * focalPoint.transform.forward * speed * Time.deltaTime);
    }
}
