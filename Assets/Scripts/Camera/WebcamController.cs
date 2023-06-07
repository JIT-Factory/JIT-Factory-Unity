using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class WebcamController : MonoBehaviour
{
    private string baseUrl = "http://localhost:8080/api/camera/show/";

    public List<Camera> cameraList; // ī�޶� ������Ʈ ����Ʈ
    private long currentCameraNumber; // ���� Ȱ��ȭ�� ī�޶� ��ȣ

    public float updateInterval = 5f; // ������Ʈ ���� (��)

    private void Start()
    {
        //cameraList = new List<Camera>(); // ī�޶� ������Ʈ ����Ʈ �ʱ�ȭ
        currentCameraNumber = -1; // �ʱⰪ ����

        // ���� �ð� �������� GetCameraData �޼��� ȣ��
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
            // ���� ������ ó��
            string responseJson = request.downloadHandler.text;
            CameraData cameraData = JsonUtility.FromJson<CameraData>(responseJson);

            // ó���� ������ ���
            Debug.Log("ID: " + cameraData.id);
            Debug.Log("Factory Name: " + cameraData.factoryName);
            Debug.Log("Camera Number: " + cameraData.cameraNumber);

            // ī�޶� ���� �� ����
            ControlCameras(cameraData);
        }
    }

    private void ControlCameras(CameraData cameraData)
    {
        // ���� Ȱ��ȭ�� ī�޶�� ���ο� ī�޶� ��ȣ�� ������ �ƹ� �۾��� �������� ����
        if (currentCameraNumber == cameraData.cameraNumber)
        {
            return;
        }

        // ��� ī�޶� ��
        foreach (Camera camera in cameraList)
        {
            camera.enabled = false;
        }
        
        // �ش� ��ȣ�� ī�޶� ��
        Camera activeCamera = GetCameraByNumber(cameraData.cameraNumber);
        if (activeCamera != null)
        {
            activeCamera.enabled = true;
            currentCameraNumber = cameraData.cameraNumber; // ���� ī�޶� ��ȣ ������Ʈ
        }
    }

    private Camera GetCameraByNumber(long cameraNumber)
    {
        // cameraNumber�� �´� ī�޶� ã�Ƽ� ��ȯ
        string cameraName = "Camera" + cameraNumber; // ����: "Camera1", "Camera2", ...
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
