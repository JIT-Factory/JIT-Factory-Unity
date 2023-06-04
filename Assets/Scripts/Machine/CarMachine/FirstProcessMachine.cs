using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstProcessMachine : MonoBehaviour
{
    public GameObject firstProcess;
    public GameObject firstSpawn;
    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Interior"))
        {
            audioSource.Play();
            Destroy(other.gameObject);
            Instantiate(firstProcess, firstSpawn.transform.position, Quaternion.Euler(0,-90,0));
        }
    }
}
