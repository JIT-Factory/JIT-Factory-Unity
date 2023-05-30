using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

public class PostProductData : MonoBehaviour
{
    private ProData proData;
    public AudioClip audioClip;
    void OnTriggerEnter(Collider other)
    {
        SoundManager.Instance.PlaySound(audioClip); // �Ҹ� ���
        Destroy(other.gameObject);
        StartCoroutine(PostData());
    }

    IEnumerator PostData()
    {
        // ProductData �ν��Ͻ� ���� �� �� ����
        proData = new ProData();

        string url = "http://localhost:8080/api/product/add";
        // JSON���� ����ȭ
        string jsonBody = JsonUtility.ToJson(proData);
        // string jsonBody = 
        // "{\"factoryName\":\"CarFactory\",\"productName\":\"ProductA\",\"status\":\"success\",\"sales\":100,\"reason\":\"-\"}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Product add request sent successfully.");
        }
        else
        {
            Debug.Log("Failed to send product add request. Error: " + request.error);
        }
    }
}