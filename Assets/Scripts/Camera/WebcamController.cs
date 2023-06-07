using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class WebcamController : MonoBehaviour
{
    private string baseUrl = "http://localhost:8080/api/camera/show/";

    public List<Camera> cameraList; // 카메라 오브젝트 리스트
    private long currentCameraNumber; // 현재 활성화된 카메라 번호

    public float updateInterval = 5f; // 업데이트 간격 (초)

    private void Start()
    {
        //cameraList = new List<Camera>(); // 카메라 오브젝트 리스트 초기화
        currentCameraNumber = -1; // 초기값 설정

        // 일정 시간 간격으로 GetCameraData 메서드 호출
        InvokeRepeating("GetCameraData", 0f, updateInterval);
    }

    private void GetCameraData()
    {
        StartCoroutine(GetCameraDataCoroutine("CarFactory"));
        //StartCoroutine(GetCameraDataCoroutine("PackagingFactory"));
    }

    public IEnumerator GetCameraDataCoroutine(string factoryName)
    {
        string url = baseUrl + factoryName;
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // 응답 데이터 처리
            string responseJson = request.downloadHandler.text;
            CameraData cameraData = JsonUtility.FromJson<CameraData>(responseJson);

            // 처리된 데이터 사용
            Debug.Log("ID: " + cameraData.id);
            Debug.Log("Factory Name: " + cameraData.factoryName);
            Debug.Log("Camera Number: " + cameraData.cameraNumber);

            // 카메라 생성 및 제어
            ControlCameras(cameraData);
        }
    }

    private void ControlCameras(CameraData cameraData)
    {
        // 현재 활성화된 카메라와 새로운 카메라 번호가 같으면 아무 작업도 수행하지 않음
        if (currentCameraNumber == cameraData.cameraNumber)
        {
            return;
        }

        // 모든 카메라를 끔
        foreach (Camera camera in cameraList)
        {
            camera.enabled = false;
        }
        
        // 해당 번호의 카메라를 켬
        Camera activeCamera = GetCameraByNumber(cameraData.cameraNumber);
        if (activeCamera != null)
        {
            activeCamera.enabled = true;
            currentCameraNumber = cameraData.cameraNumber; // 현재 카메라 번호 업데이트
        }
    }

    private Camera GetCameraByNumber(long cameraNumber)
    {
        // cameraNumber에 맞는 카메라를 찾아서 반환
        string cameraName = "Camera" + cameraNumber; // 예시: "Camera1", "Camera2", ...
        GameObject cameraObject = GameObject.Find(cameraName);
        if (cameraObject != null)
        {
            Camera cameraComponent = cameraObject.GetComponent<Camera>();
            return cameraComponent;
        }

        return null;
    }
}

[System.Serializable]
public class CameraData
{
    public int id;
    public string factoryName;
    public long cameraNumber;
}
