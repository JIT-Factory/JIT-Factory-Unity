using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    public GameObject fog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Instantiate(fog, transform.position , transform.rotation);
        }
    }
}
