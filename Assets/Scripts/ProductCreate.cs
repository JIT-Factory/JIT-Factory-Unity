using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductCreate : MonoBehaviour
{
    public GameObject Product;
    public GameObject CreateSpawn;
     public bool PlayButton;

    Vector3 vector3;
     void Start()
     {
        PlayButton = false;
        vector3 = new Vector3(CreateSpawn.transform.position.x,CreateSpawn.transform.position.y-1.0f,CreateSpawn.transform.position.z);
     }
    // Update is called once per frame
    void Update()
    {
        Instantiate(Product,vector3,Product.transform.rotation);
    }
}
