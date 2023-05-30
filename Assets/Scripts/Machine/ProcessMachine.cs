using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessMachine : MonoBehaviour
{
    public GameObject firstProcess;
    public GameObject firstSpawn;
    public GameObject SecondProcess;
    public GameObject SecondSpawn;
    public GameObject ThirdProcess;
    public GameObject ThirdSpawn;
    public AudioClip audioClip;
   private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Interior"))
        {
            SoundManager.Instance.PlaySound(audioClip); // 家府 犁积
            Destroy(other.gameObject);
            Instantiate(firstProcess, firstSpawn.transform.position, Quaternion.Euler(0,-90,0));
        }
        else if(other.gameObject.CompareTag("1stProcess"))
        {
            SoundManager.Instance.PlaySound(audioClip); // 家府 犁积
            Destroy(other.gameObject);
            Instantiate(SecondProcess, SecondSpawn.transform.position, Quaternion.Euler(0,-90,0));
        }
        else if(other.gameObject.CompareTag("2stProcess"))
        {
            SoundManager.Instance.PlaySound(audioClip); // 家府 犁积
            Destroy(other.gameObject);
            Instantiate(ThirdProcess, ThirdSpawn.transform.position, Quaternion.Euler(0,-90,0));
        }
        else{
             Destroy(other.gameObject);
        }
    }
}
