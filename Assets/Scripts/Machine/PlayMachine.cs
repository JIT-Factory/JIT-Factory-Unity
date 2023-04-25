using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMachine : MonoBehaviour
{
   public List<GameObject> sideBelts;
   public List<GameObject> belts;
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

    public void MachineOperationConTrue()
    {
        machineOper = true;
        foreach (GameObject belt in sideBelts)
        {
            belt.GetComponent<ConveyorBeltBack>().machineOperation = machineOper;
        }
        foreach (GameObject belt in belts)
        {
            belt.GetComponent<ConveyorBelt>().machineOperation = machineOper;
        }
        Debug.Log("전체 기계 가동" + machineOper);
    }
    public void MachineOperationConFalse()
    {
        machineOper = false;
        foreach (GameObject belt in sideBelts)
        {
            belt.GetComponent<ConveyorBeltBack>().machineOperation = machineOper;
        }
        foreach (GameObject belt in belts)
        {
            belt.GetComponent<ConveyorBelt>().machineOperation = machineOper;
        }
        Debug.Log("전체 기계 가동" + machineOper);
    }

    public bool GetmachineOper()
    {
        return machineOper;
    }

    public bool SetmachineOper(bool other)
    {
        machineOper = other;
        return machineOper;
    }
}
