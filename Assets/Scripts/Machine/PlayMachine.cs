using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMachine : MonoBehaviour
{
   public List<GameObject> sideBelts;
   public List<GameObject> belts;
    public bool machineOper;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        machineOper = false;
    }

    // Update is called once per frame
    void Update()
    {
        //   if (machineOperation && !SoundManager.Instance.IsPlaying(audioClip))
        // {
        //     SoundManager.Instance.PlaySound(audioClip); // ???¢´ ¡¿??
        // }
        // else if (!machineOperation)
        // {
        //     SoundManager.Instance.StopSound(audioClip); // ???¢´ ¡¿©£¡¿?
        // }
    }

    public void MachineOperationConTrue()
    {
        machineOper = true;
        SoundManager.Instance.PlaySound(audioClip); // ???¢´ ¡¿??
        foreach (GameObject belt in sideBelts)
        {
            belt.GetComponent<ConveyorBeltBack>().machineOperation = machineOper;
        }
        foreach (GameObject belt in belts)
        {
            belt.GetComponent<ConveyorBelt>().machineOperation = machineOper;
        }
    }
    public void MachineOperationConFalse()
    {
        machineOper = false;
        SoundManager.Instance.PauseSound(audioClip);
        foreach (GameObject belt in sideBelts)
        {
            belt.GetComponent<ConveyorBeltBack>().machineOperation = machineOper;
        }
        foreach (GameObject belt in belts)
        {
            belt.GetComponent<ConveyorBelt>().machineOperation = machineOper;
        }
    }
    public void MachineOperationConTrueSecond()
    {
        machineOper = true;
        SoundManager.Instance.PlaySound(audioClip); // ???¢´ ¡¿??
        foreach (GameObject belt in sideBelts)
        {
            belt.GetComponent<ConveyorBeltFron>().machineOperation = machineOper;
        }
        foreach (GameObject belt in belts)
        {
            belt.GetComponent<ConveyorBelt>().machineOperation = machineOper;
        }
    }
    public void MachineOperationConFalseSecond()
    {
        machineOper = false;
        SoundManager.Instance.PauseSound(audioClip);
        foreach (GameObject belt in sideBelts)
        {
            belt.GetComponent<ConveyorBeltFron>().machineOperation = machineOper;
        }
        foreach (GameObject belt in belts)
        {
            belt.GetComponent<ConveyorBelt>().machineOperation = machineOper;
        }
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
