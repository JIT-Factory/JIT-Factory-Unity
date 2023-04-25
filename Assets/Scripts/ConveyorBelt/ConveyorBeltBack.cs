using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltBack : MonoBehaviour
{
    public bool machineOperation;
    float speed = 1.0f;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        machineOperation = false;
        rigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate() {
        if(machineOperation == true)
        {
            Vector3 Position = rigidbody.position;
            rigidbody.position += Vector3.back * speed *Time.deltaTime;
            rigidbody.MovePosition(Position);
        }
    }
}