using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltCurve : MonoBehaviour
{
   public bool machineOperation;
    public float speed = 1.0f;
    public float amplitude = 1.0f;
    public float frequency = 1.0f;

    private new Rigidbody rigidbody;
    private Vector3 startPosition;

    private void Start() {
        machineOperation = false;
        rigidbody = GetComponent<Rigidbody>();
        startPosition = rigidbody.position;
    }

    private void FixedUpdate() {
        if (machineOperation) {
            Vector3 position = startPosition + Vector3.left * speed * Time.time;

            float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
            position += Vector3.up * yOffset;

            rigidbody.MovePosition(position);
        }
    }
}
