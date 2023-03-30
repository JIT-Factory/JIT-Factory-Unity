using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public bool machineOperation = true;
    float speed = 1.0f;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate() {
        if(machineOperation == true)
        {
            Vector3 Position = rigidbody.position;
            rigidbody.position += Vector3.left * speed *Time.deltaTime;
            rigidbody.MovePosition(Position);
        }
    }
}
