using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product2Btn : MonoBehaviour
{
     public bool ProPlayButton2;
    // Start is called before the first frame update
    void Start()
    {
        ProPlayButton2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ProButton2()
    {
        ProPlayButton2 = !ProPlayButton2;
        Debug.Log("btn2");
    }
     public bool GetProButton2()
    {
        return ProPlayButton2;
    }
}
