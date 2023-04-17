using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMachine : MonoBehaviour
{
    public GameObject Belt1;
    public GameObject Belt2;
    public GameObject Belt3;
    public GameObject Belt4;

    public bool machineOper;

    // Start is called before the first frame update
    void Start()
    {
        machineOper = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MachineOperationCon()
    {
       machineOper = !machineOper;
       
       Belt1.GetComponent<ConveyorBelt>().machineOperation = !Belt1.GetComponent<ConveyorBelt>().machineOperation;

       Belt2.GetComponent<ConveyorBelt>().machineOperation = !Belt1.GetComponent<ConveyorBelt>().machineOperation;

       Belt3.GetComponent<ConveyorBelt>().machineOperation = !Belt1.GetComponent<ConveyorBelt>().machineOperation;

       Belt4.GetComponent<ConveyorBelt>().machineOperation = !Belt1.GetComponent<ConveyorBelt>().machineOperation;

       Debug.Log(machineOper);
    }

    public bool GetmachineOper()
    {
        return machineOper;
    }
     public bool SetmachineOper()
    {
        return machineOper;
    }
}
