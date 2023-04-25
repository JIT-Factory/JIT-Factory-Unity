using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class CameraAPI : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

   /* void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
        // 카메라 정보를 JSON으로 직렬화
       // CameraData cameraData = new CameraData(mainCamera.transform.position, mainCamera.transform.rotation);
        string jsonData = JsonUtility.ToJson(cameraData);

        // 서버에 POST 요청을 보내기
        StartCoroutine(PostRequest(jsonData));
        Debug.Log(jsonData);
        }
        
    }
*/
    IEnumerator PostRequest(string jsonData)
    {
        // 요청을 보낼 URL
        string url = "http://localhost:8080/api/screenshots";

        // 요청 생성
        UnityWebRequest www = UnityWebRequest.Post(url, "");
        www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonData));
        www.SetRequestHeader("Content-Type", "application/json");
        if (www.result != UnityWebRequest.Result.Success)

        // 요청 보내기
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error sending camera data: " + www.error);
        }
    }
}
/*
[System.Serializable]
public class CameraData
{
    public Vector3 position;
    public Quaternion rotation;

    public CameraData(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}*/