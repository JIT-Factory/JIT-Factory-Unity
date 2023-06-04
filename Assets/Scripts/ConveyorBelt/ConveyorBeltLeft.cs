using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltLeft : MonoBehaviour
{
    public bool machineOperation;
    AudioSource audioSource;
    //public AudioClip audioClip;
    
    float speed = 1.0f;

    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        machineOperation = false;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
void FixedUpdate()
    {
        if (machineOperation)
        {
            Vector3 position = rigidbody.position;
            rigidbody.position += Vector3.left * speed * Time.deltaTime;
            rigidbody.MovePosition(position);
        }
    }

    void Update()
    {
       
        // if (machineOperation && !SoundManager.Instance.IsPlaying(audioClip))
        // {
        //     SoundManager.Instance.PlaySound(audioClip); // ???¢´ ¡¿??
        // }
        // else if (!machineOperation)
        // {
        //     SoundManager.Instance.StopSound(audioClip); // ???¢´ ¡¿©£¡¿?
        // }
       
    
    }

    public void GetmachineOper()
    {
        machineOperation = !machineOperation;
    }
}

