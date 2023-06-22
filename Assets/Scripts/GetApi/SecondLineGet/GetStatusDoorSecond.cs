using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStatusDoorSecond : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.transform.CompareTag("Door"))
        {
            GameObject.Find("GetMaterialDoorSecond").GetComponent<GetMaterialSecond>().createstatus = true;
        }
        
    }
}
