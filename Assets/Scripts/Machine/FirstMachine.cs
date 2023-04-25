using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMachine : MonoBehaviour
{
    public GameObject spawn;
    
    Vector3 vector3;
    // Start is called before the first frame update
    void Start()
    {
        vector3 = new Vector3(spawn.transform.position.x,spawn.transform.position.y,spawn.transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other) {
        
        if(other.CompareTag("Product"))
        {
            other.transform.position = vector3;
        }
    }
}
