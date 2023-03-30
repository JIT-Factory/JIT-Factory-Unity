using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    float maxDistance = 15f;

    public GameObject raycast;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Raycast()
    {
        

        if (Physics.Raycast(raycast.transform.position, raycast.transform.forward , out hit, float.MaxValue))
        {
           
            if (hit.distance < 1 && Input.GetKeyDown(KeyCode.G))
            {
                
            }
        }
    }
}
