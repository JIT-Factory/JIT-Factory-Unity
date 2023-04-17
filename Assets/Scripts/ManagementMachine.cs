using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagementMachine : MonoBehaviour
{
    public GameObject btn;
    

    [SerializeField]
    public bool onOff1;

    // Start is called before the first frame update
    void Start()
    {
        onOff1 = btn.GetComponent<PlayMachine>().GetmachineOper();
    }

    // Update is called once per frame
    void Update()
    {
        onOff1 = btn.GetComponent<PlayMachine>().GetmachineOper();
    }

    public bool GetMachineOnOff()
    {
        return onOff1;
    }
     public bool SetMachineOnOff()
    {
        return onOff1;
    }
    
    
}
