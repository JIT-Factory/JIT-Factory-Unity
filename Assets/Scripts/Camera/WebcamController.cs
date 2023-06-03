using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class WebcamController : MonoBehaviour
{
    // ī�޶���� �迭
    public Camera[] cameras;

    // ī�޶� ��� ���� API �ּ�
    public string apiUrl = "http://localhost:8080/api/camera/show";

    // �����͸� ������Ʈ�� �ֱ� (�� ����)
    public float updateInterval = 2f;

    private bool isUpdating = false;

    private void Start()
    {
        // �ʱ� ī�޶� ������ ��û
        StartCoroutine(GetCameraData());
    }

    private void Update()
    {
        // ������ ������Ʈ ���� �ƴ� ��쿡�� ������Ʈ ��û
        if (!isUpdating)
        { 
            StartCoroutine(UpdateCameraData());
        }
    }

    IEnumerator UpdateCameraData()
    {
        isUpdating = true;

        // ���� �ð����� ������ ��û
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
                Debug.LogError("API ��û ����: " + www.error);
                yield break;
            }

            // JSON ������ �Ľ�
            string json = www.downloadHandler.text;
            CameraDataList cameraDataList = JsonUtility.FromJson<CameraDataList>("{\"items\":" + json + "}");

            if (cameraDataList != null && cameraDataList.items.Count > 0)
            {
                // cameraNumber ���� �´� ī�޶�� ��ȯ
                long cameraNumber = cameraDataList.items[0].cameraNumber;
                SwitchCamera(cameraNumber);
            }
        }
    }
    void SwitchCamera(long cameraNumber)
{
    foreach (Camera camera in cameras)
    {
        // cameraNumber�� ��ġ�ϴ� ī�޶� Ȱ��ȭ
        if (camera.name == "Camera" + cameraNumber)
        {
            camera.gameObject.SetActive(true);

            // �ش� ī�޶� ����� Audio Listener�� Ȱ��ȭ
            AudioListener audioListener = camera.GetComponent<AudioListener>();
            if (audioListener != null)
            {
                audioListener.enabled = true;
            }
        }
        else
        {
            camera.gameObject.SetActive(false);

            // �ٸ� ī�޶�鿡 ����� Audio Listener�� ��Ȱ��ȭ
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
    //         // cameraNumber�� ��ġ�ϴ� ī�޶� Ȱ��ȭ
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
