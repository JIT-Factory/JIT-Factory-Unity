using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStatusWheel : MonoBehaviour
{
     private void OnCollisionEnter(Collision other) {
        if(other.transform.CompareTag("Wheel"))
        {
             GameObject.Find("GetMaterialWheel").GetComponent<GetMaterial>().createstatus = true;
        }
       
    }
}
