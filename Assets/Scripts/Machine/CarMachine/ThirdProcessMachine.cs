using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdProcessMachine : MonoBehaviour
{
    public GameObject ThirdProcess;
     public GameObject ThirdSpawn;
     AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("2stProcess"))
        {
            audioSource.Play();
            Destroy(other.gameObject);
            Instantiate(ThirdProcess, ThirdSpawn.transform.position, Quaternion.Euler(0,-90,0));
        }
    }
}
