using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class GetConveyorBelt : MonoBehaviour
{
    private string baseUrl = "http://localhost:8080/api/process/FrontProcess";
    public string processName;
    public float updateInterval = 1f; // 업데이트 간격
    GameObject conveyorBelt;

    // Start is called before the first frame update
    void Start()
    {
        conveyorBelt  = GameObject.Find("ConveyorManagement");
        StartCoroutine(GetProcessData());
    }

    IEnumerator GetProcessData()
    {
        while (true) // 무한 반복
        {
            UnityWebRequest request = UnityWebRequest.Get(baseUrl + processName);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                ProcessData processData = JsonConvert.DeserializeObject<ProcessData>(jsonResponse);

                if (processData.processStatus == "start")
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

            yield return new WaitForSeconds(updateInterval); // 일정 시간만큼 대기 후 반복
        }
    }

    // 데이터 모델 클래스
    public class ProcessData
    {
        public int id;
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