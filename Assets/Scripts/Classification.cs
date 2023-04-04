using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classification : MonoBehaviour
{
    
    public GameObject product1;
    public GameObject product2;
    public GameObject product3;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    Vector3 v1;
    Vector3 v2;
    Vector3 v3;
    // Start is called before the first frame update
    void Start()
    {
        v1 = new Vector3(spawn1.transform.position.x,spawn1.transform.position.y,spawn1.transform.position.z);
        v2 = new Vector3(spawn2.transform.position.x,spawn2.transform.position.y,spawn2.transform.position.z);
        v3 = new Vector3(spawn3.transform.position.x,spawn3.transform.position.y,spawn3.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        if(other.CompareTag("Product1"))
        {
            if(GameObject.Find("Product1").GetComponent<ProData>().productName == "scissors")
            {
                
                other.transform.position = v1;
                Debug.Log("¾ø¾îÁü1");
                
            }
        }
        if(other.CompareTag("Product2"))
        {
            if(GameObject.Find("Product2").GetComponent<ProData>().productName == "rock")
            {
                other.transform.position = v2;
                Debug.Log("¾ø¾îÁü2");
                
            }
        }
        if(other.CompareTag("Product3"))
        {
            if(GameObject.Find("Product3").GetComponent<ProData>().productName == "furoshiki")
            {
                other.transform.position = v3;
                Debug.Log("¾ø¾îÁü3");
                
            }
        }
    }
}
