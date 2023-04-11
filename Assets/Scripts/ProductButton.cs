
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductButton : MonoBehaviour
{
    public bool ProPlayButton1;
    public bool ProPlayButton2;
    public bool ProPlayButton3;

    GameObject managementMachine;
    // Start is called before the first frame update
    void Start()
    {
        ProPlayButton1 = false;
        ProPlayButton2 = false;
        ProPlayButton3 = false;

        managementMachine.GetComponent<ManagementMachine>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ProButton1()
    {
        ProPlayButton1 = !ProPlayButton1;
    }
    public void ProButton2()
    {
        ProPlayButton2 = !ProPlayButton2;
    }
    public void ProButton3()
    {
        ProPlayButton3 = !ProPlayButton3;
    }

    public bool GetProButton1()
    {
        return ProPlayButton1;
    }
    public bool GetProButton2()
    {
        return ProPlayButton2;
    }
    public bool GetProButton3()
    {
        return ProPlayButton3;
    }

}
