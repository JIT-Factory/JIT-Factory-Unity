using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackPlayMachine : MonoBehaviour
{
    public List<GameObject> sideBelts;
   public List<GameObject> belts;
 
     bool machineOperPack;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
   
        machineOperPack = false;
    }
  public void MachineOperationConTruePack()
    {
        machineOperPack = true;
        SoundManager.Instance.PlaySound(audioClip); // ????ве вовп??
        foreach (GameObject belt in sideBelts)
        {
            belt.GetComponent<ConveyorBeltBack>().machineOperation = machineOperPack;
        }
        foreach (GameObject belt in belts)
        {
            belt.GetComponent<ConveyorBeltLeft>().machineOperation = machineOperPack;
        }
    }
    public void MachineOperationConFalsePack()
    {
        machineOperPack = false;
        SoundManager.Instance.PauseSound(audioClip);
        foreach (GameObject belt in sideBelts)
        {
            belt.GetComponent<ConveyorBeltBack>().machineOperation = machineOperPack;
        }
        foreach (GameObject belt in belts)
        {
            belt.GetComponent<ConveyorBeltLeft>().machineOperation = machineOperPack;
        }
    }
}
