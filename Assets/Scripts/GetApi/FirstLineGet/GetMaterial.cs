using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMaterial : MonoBehaviour
{
    [SerializeField]
    private GameObject doorPrefab;
    [SerializeField]
    private GameObject doorspawn;
    public GameObject belt;
    public bool createstatus;
    void Start()
    {
       createstatus = true;
    }

    private void Update() {
        if(belt.GetComponent<GetApiProcess>().GetStaus() == true && createstatus == true)
        {
            createstatus = false;
            Create();
        }
    }
    void Create()
    {
        Instantiate(doorPrefab, doorspawn.transform.position, Quaternion.Euler(0, 180, 0));
    }
    public bool Status()
    {
        createstatus  = true;
        return createstatus;
    }
}
