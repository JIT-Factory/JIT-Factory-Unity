using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondProcessMachine : MonoBehaviour
{
    public GameObject SecondProcess;
     public GameObject SecondSpawn;
    public AudioClip audioClip;
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("1stProcess"))
        {
            SoundManager.Instance.PlaySound(audioClip); // 소리 재생
            Destroy(other.gameObject);
            Instantiate(SecondProcess, SecondSpawn.transform.position, Quaternion.Euler(0,-90,0));
        }
    }
}
