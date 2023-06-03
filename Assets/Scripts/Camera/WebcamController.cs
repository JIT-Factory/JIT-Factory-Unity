using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class WebcamController : MonoBehaviour
{
    // 카메라들의 배열
    public Camera[] cameras;

    // 카메라 제어를 위한 API 주소
    public string apiUrl = "http://localhost:8080/api/camera/show";

    // 데이터를 업데이트할 주기 (초 단위)
    public float updateInterval = 2f;

    private bool isUpdating = false;

    private void Start()
    {
        // 초기 카메라 데이터 요청
        StartCoroutine(GetCameraData());
    }

    private void Update()
    {
        // 데이터 업데이트 중이 아닌 경우에만 업데이트 요청
        if (!isUpdating)
        { 
            StartCoroutine(UpdateCameraData());
        }
    }

    IEnumerator UpdateCameraData()
    {
        isUpdating = true;

        // 일정 시간마다 데이터 요청
        yield return new WaitForSeconds(updateInterval);

        yield return StartCoroutine(GetCameraData());

        isUpdating = false;
    }

    IEnumerator GetCameraData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("API 요청 실패: " + www.error);
                yield break;
            }

            // JSON 데이터 파싱
            string json = www.downloadHandler.text;
            CameraDataList cameraDataList = JsonUtility.FromJson<CameraDataList>("{\"items\":" + json + "}");

            if (cameraDataList != null && cameraDataList.items.Count > 0)
            {
                // cameraNumber 값에 맞는 카메라로 전환
                long cameraNumber = cameraDataList.items[0].cameraNumber;
                SwitchCamera(cameraNumber);
            }
        }
    }
    void SwitchCamera(long cameraNumber)
{
    foreach (Camera camera in cameras)
    {
        // cameraNumber와 일치하는 카메라를 활성화
        if (camera.name == "Camera" + cameraNumber)
        {
            camera.gameObject.SetActive(true);

            // 해당 카메라에 연결된 Audio Listener를 활성화
            AudioListener audioListener = camera.GetComponent<AudioListener>();
            if (audioListener != null)
            {
                audioListener.enabled = true;
            }
        }
        else
        {
            camera.gameObject.SetActive(false);

            // 다른 카메라들에 연결된 Audio Listener를 비활성화
            AudioListener audioListener = camera.GetComponent<AudioListener>();
            if (audioListener != null)
            {
                audioListener.enabled = false;
            }
        }
    }
}

    // void SwitchCamera(long cameraNumber)
    // {
    //     foreach (Camera camera in cameras)
    //     {
    //         // cameraNumber와 일치하는 카메라를 활성화
    //         if (camera.name == "Camera" + cameraNumber)
    //         {
    //             camera.gameObject.SetActive(true);
    //         }
    //         else
    //         {
    //             camera.gameObject.SetActive(false);
    //         }
    //     }
    // }
}

[System.Serializable]
public class CameraData
{
    public long id;
    public string factoryName;
    public long cameraNumber;
}

[System.Serializable]
public class CameraDataList
{
    public List<CameraData> items;
}
