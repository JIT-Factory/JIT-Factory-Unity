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
    public float updateInterval = 1f; // 업데이트 간격
    GameObject conveyorBelt;

    // Start is called before the first frame update
    void Start()
    {
        conveyorBelt = GameObject.Find("ConveyorManagement");
        StartCoroutine(GetProcessData());
        
    }

    IEnumerator GetProcessData()
    {
        while (true) // 무한 반복
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
                        // 기타 코드
                        Debug.Log("기계 동작 시작");
                    }
                    else if (processData.processStatus == "stop")
                    {
                        conveyorBelt.GetComponent<PlayMachine>().MachineOperationConFalse();
                        // 기타 코드
                        Debug.Log("기계 동작 중지");
                    }
                }
            }

            yield return new WaitForSeconds(updateInterval); // 일정 시간만큼 대기 후 반복
        }
    }

    // 데이터 모델 클래스
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
