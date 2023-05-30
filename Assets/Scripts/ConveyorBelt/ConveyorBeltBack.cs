using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltBack : MonoBehaviour
{
    public bool machineOperation;
    public AudioClip audioClip;
    float speed = 1.0f;
   private new Rigidbody rigidbody;
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
     void Update()
    {
        if (machineOperation && !SoundManager.Instance.IsPlaying(audioClip))
        {
            SoundManager.Instance.PlaySound(audioClip); // 家府 犁积
        }
        else if (!machineOperation)
        {
            SoundManager.Instance.StopSound(audioClip); // 家府 吝瘤
        }
       
    }
}
