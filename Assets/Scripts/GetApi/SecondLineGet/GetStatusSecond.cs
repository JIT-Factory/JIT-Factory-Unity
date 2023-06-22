using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStatusSecond : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.transform.CompareTag("Wheel"))
        {
            GameObject.Find("GetMaterialWheelSecond").GetComponent<GetMaterialSecond>().createstatus = true;
        }
        
    }
}
