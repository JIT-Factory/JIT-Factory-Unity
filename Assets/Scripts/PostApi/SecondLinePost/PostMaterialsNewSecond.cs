using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialData;

public class PostMaterialsNewSecond : MonoBehaviour
{
    [SerializeField]
    private string factoryName = "CarFactory";
    [SerializeField]
    public string materialName1 = "CarWheels_Second";
    [SerializeField]
    public string materialName2 = "CarDoors_Second";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("3stProcess"))
        {
            StartCoroutine(PostWheelMaterial());
            StartCoroutine(PostDoorMaterial());
        }
    }

    IEnumerator PostWheelMaterial()
    {
        CarMaterial order = new CarMaterial
        {
            factoryName = factoryName,
            materialName = materialName1,
            stock = -1
        };
        string json = JsonUtility.ToJson(order);

        UnityWebRequest request = UnityWebRequest.Post("http://localhost:8080/api/material/new", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("POST success!");
        }
        else
        {
            Debug.Log("POST failed!");
        }

        request.Dispose();
    }

    IEnumerator PostDoorMaterial()
    {
        CarMaterial order = new CarMaterial
        {
            factoryName = factoryName,
            materialName = materialName2,
            stock = -1
        };
        string json = JsonUtility.ToJson(order);

        UnityWebRequest request = UnityWebRequest.Post("http://localhost:8080/api/material/new", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("POST success!");
        }
        else
        {
            Debug.Log("POST failed!");
        }

        request.Dispose();
    }
}

