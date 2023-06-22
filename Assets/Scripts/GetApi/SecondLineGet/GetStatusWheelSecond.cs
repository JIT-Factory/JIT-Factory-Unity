using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStatusWheelSecond : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        GameObject.Find("GetMaterialWheelSecond").GetComponent<GetMaterial>().createstatus = true;
    }
}
