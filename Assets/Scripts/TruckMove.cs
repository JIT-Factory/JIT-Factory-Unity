using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMove : MonoBehaviour
{
    public int productcount1;
    public int productcount2;
    public int productcount3;
    List<GameObject> objectsToDelete1 = new List<GameObject>();
    List<GameObject> objectsToDelete2= new List<GameObject>();
    List<GameObject> objectsToDelete3 = new List<GameObject>();
    
    // Update is called once per frame
    void Update()
    {
        if(productcount1 == 2)
        {
            foreach(GameObject obj in objectsToDelete1)
            {
                Destroy(obj);
                productcount1=0;
            }
            objectsToDelete1.Clear();
        }
        if(productcount2 == 2)
        {
            foreach(GameObject obj in objectsToDelete2)
            {
                Destroy(obj);
                productcount2=0;
            }
            objectsToDelete2.Clear();
        }
        if(productcount3 == 2)
        {
            foreach(GameObject obj in objectsToDelete3)
            {
                Destroy(obj);
                productcount3=0;
            }
            objectsToDelete3.Clear();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Product1"))
        {
            objectsToDelete1.Add(other.gameObject);
            productcount1++;
        }
         if(other.CompareTag("Product2"))
        {
            objectsToDelete2.Add(other.gameObject);
            productcount2++;
        }
         if(other.CompareTag("Product3"))
        {
            objectsToDelete3.Add(other.gameObject);
            productcount3++;
        }
    }
}