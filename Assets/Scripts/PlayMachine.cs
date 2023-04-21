using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMachine : MonoBehaviour
{
   public List<GameObject> BeltsBack;
   public List<GameObject> Belts;
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
        foreach (GameObject belt in Belts)
        {
            belt.GetComponent<ConveyorBelt>().machineOperation = machineOper;
        }
        foreach (GameObject belt in BeltsBack)
        {
            belt.GetComponent<ConveyorBeltBack>().machineOperation = machineOper;
        }
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
