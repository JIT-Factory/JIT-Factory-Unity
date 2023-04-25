using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondProcessMachine : MonoBehaviour
{
    public GameObject SecondProcess;
     public GameObject SecondSpawn;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("1stProcess"))
        {
            Destroy(other.gameObject);
            Instantiate(SecondProcess, SecondSpawn.transform.position, Quaternion.Euler(0,-90,0));
        }
    }
}
