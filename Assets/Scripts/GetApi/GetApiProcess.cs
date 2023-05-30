using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class GetApiProcess : MonoBehaviour
{
    public string baseUrl = "http://localhost:8080/api/process";
    public string factoryName;
    public string processName;
    public float updateInterval = 1f; // ������Ʈ ����
    GameObject conveyorBelt;

    // Start is called before the first frame update
    void Start()
    {
        conveyorBelt = GameObject.Find("ConveyorManagement");
        StartCoroutine(GetProcessData());
        
    }

    IEnumerator GetProcessData()
    {
        while (true) // ���� �ݺ�
        {
            string url = string.Format("{0}?factoryName={1}&processName={2}", baseUrl, factoryName, processName);
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                ProcessData[] processArray = JsonConvert.DeserializeObject<ProcessData[]>(jsonResponse);

                if (processArray.Length > 0)
                {
                    ProcessData processData = processArray[0];
                    if (processData.processStatus == "run")
                    {
                        conveyorBelt.GetComponent<PlayMachine>().MachineOperationConTrue();
                        // ��Ÿ �ڵ�
                        Debug.Log("��� ���� ����");
                    }
                    else if (processData.processStatus == "stop")
                    {
                        conveyorBelt.GetComponent<PlayMachine>().MachineOperationConFalse();
                        // ��Ÿ �ڵ�
                        Debug.Log("��� ���� ����");
                    }
                }
            }

            yield return new WaitForSeconds(updateInterval); // ���� �ð���ŭ ��� �� �ݺ�
        }
    }

    // ������ �� Ŭ����
    public class ProcessData
    {
        public int id;
        public string factoryName;
        public string processName;
        public string processStatus;
        public string conveyorBeltWheel;
        public string conveyorBeltDoor;
        public string firstProcessMachineConveyorBelt;
        public string secondProcessMachineConveyorBelt;
        public string thirdProcessMachineConveyorBelt;
        public string fourthProcessMachineConveyorBelt;
    }
}
