using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStatus : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other) {
        if(other.transform.CompareTag("Door"))
        {
            GameObject.Find("GetMaterialDoor").GetComponent<GetMaterial>().createstatus = true;
        }
        
    }
}
