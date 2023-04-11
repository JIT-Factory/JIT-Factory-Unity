using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagementMachine : MonoBehaviour
{
    public GameObject Belt1;
    public GameObject Belt2;
    public GameObject Belt3;
    public GameObject Belt4;

    public bool onOff1;
    public bool onOff2;
    public bool onOff3;
    public bool onOff4;

    // Start is called before the first frame update
    void Start()
    {
        onOff1 = Belt1.GetComponent<ConveyorBelt>().machineOperation;
        // onOff2 = Belt2.GetComponent<ConveyorBelt>().machineOperation;
        // onOff3 = Belt3.GetComponent<ConveyorBelt>().machineOperation;
        // onOff4 = Belt4.GetComponent<ConveyorBelt>().machineOperation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MachineOperationCon()
    {
       Belt1.GetComponent<ConveyorBelt>().machineOperation = !Belt1.GetComponent<ConveyorBelt>().machineOperation;

       Belt2.GetComponent<ConveyorBelt>().machineOperation = !Belt1.GetComponent<ConveyorBelt>().machineOperation;

       Belt3.GetComponent<ConveyorBelt>().machineOperation = !Belt1.GetComponent<ConveyorBelt>().machineOperation;

       Belt4.GetComponent<ConveyorBelt>().machineOperation = !Belt1.GetComponent<ConveyorBelt>().machineOperation;
    }

    public bool GetMachineOnOff()
    {
        return onOff1;
    }
    
}
