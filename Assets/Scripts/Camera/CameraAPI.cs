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
        // ī�޶� ������ JSON���� ����ȭ
       // CameraData cameraData = new CameraData(mainCamera.transform.position, mainCamera.transform.rotation);
        string jsonData = JsonUtility.ToJson(cameraData);

        // ������ POST ��û�� ������
        StartCoroutine(PostRequest(jsonData));
        Debug.Log(jsonData);
        }
        
    }
*/
    IEnumerator PostRequest(string jsonData)
    {
        // ��û�� ���� URL
        string url = "http://localhost:8080/api/screenshots";

        // ��û ����
        UnityWebRequest www = UnityWebRequest.Post(url, "");
        www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonData));
        www.SetRequestHeader("Content-Type", "application/json");
        if (www.result != UnityWebRequest.Result.Success)

        // ��û ������
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