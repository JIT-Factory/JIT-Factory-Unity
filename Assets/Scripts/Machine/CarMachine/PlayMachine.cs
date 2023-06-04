using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMachine : MonoBehaviour
{
   public List<GameObject> sideBelts;
   public List<GameObject> belts;
     bool machineOperCarFirst;
     bool machineOperCarSecond;
     bool machineOperPack;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        machineOperCarFirst = false;
        machineOperCarSecond = false;
        machineOperPack = false;
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
        machineOperCarFirst = true;
        SoundManager.Instance.PlaySound(audioClip); // ???¢´ ¡¿??
        foreach (GameObject belt in sideBelts)
        {
            belt.GetComponent<ConveyorBeltBack>().machineOperation = machineOperCarFirst;
        }
        foreach (GameObject belt in belts)
        {
            belt.GetComponent<ConveyorBelt>().machineOperation = machineOperCarFirst;
        }
    }
    public void MachineOperationConFalse()
    {
        machineOperCarFirst = false;
        SoundManager.Instance.PauseSound(audioClip);
        foreach (GameObject belt in sideBelts)
        {
            belt.GetComponent<ConveyorBeltBack>().machineOperation = machineOperCarFirst;
        }
        foreach (GameObject belt in belts)
        {
            belt.GetComponent<ConveyorBelt>().machineOperation = machineOperCarFirst;
        }
    }
    public void MachineOperationConTrueSecond()
    {
        machineOperCarSecond = true;
        SoundManager.Instance.PlaySound(audioClip); // ???¢´ ¡¿??
        foreach (GameObject belt in sideBelts)
        {
            belt.GetComponent<ConveyorBeltFron>().machineOperation = machineOperCarSecond;
        }
        foreach (GameObject belt in belts)
        {
            belt.GetComponent<ConveyorBelt>().machineOperation = machineOperCarSecond;
        }
    }
    public void MachineOperationConFalseSecond()
    {
        machineOperCarSecond = false;
        SoundManager.Instance.PauseSound(audioClip);
        foreach (GameObject belt in sideBelts)
        {
            belt.GetComponent<ConveyorBeltFron>().machineOperation = machineOperCarSecond;
        }
        foreach (GameObject belt in belts)
        {
            belt.GetComponent<ConveyorBelt>().machineOperation = machineOperCarSecond;
        }
    }

    
    // public bool GetmachineOper()
    // {
    //     return machineOper;
    // }

    // public bool SetmachineOper(bool other)
    // {
    //     machineOper = other;
    //     return machineOper;
    // }
}
