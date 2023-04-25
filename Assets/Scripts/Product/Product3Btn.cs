using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product3Btn : MonoBehaviour
{
    public bool ProPlayButton3;
    // Start is called before the first frame update
    void Start()
    {
        ProPlayButton3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ProButton3()
    {
        ProPlayButton3 = !ProPlayButton3;
        Debug.Log("btn3");
    }
    public bool GetProButton3()
    {
        return ProPlayButton3;
    }
}
