using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltBtn : MonoBehaviour
{
    GameObject conveyorBelt;
    
    // Start is called before the first frame update
    private void Awake() {
        conveyorBelt = GameObject.Find("ConveyorManagement");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayMachineTrue()
    {
        conveyorBelt.GetComponent<PlayMachine>().MachineOperationConTrue();
    }
     public void PlayMachineFalse()
    {
        conveyorBelt.GetComponent<PlayMachine>().MachineOperationConFalse();
    }
}
