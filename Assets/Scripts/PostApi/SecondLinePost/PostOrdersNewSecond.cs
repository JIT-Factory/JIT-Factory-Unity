using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using OrderData;

public class PostOrdersNewSecond : MonoBehaviour
{
    [SerializeField]
    private string factoryName;
    [SerializeField]
    private string productName;


    private void OnTriggerEnter(Collider other) {
         StartCoroutine(PostNewOrder());
    }


        IEnumerator PostNewOrder()
    {
        Order order = new Order
        {
            factoryName = factoryName,
            productName = productName,
            count = -1
        };
        string json = JsonUtility.ToJson(order);

        UnityWebRequest request = UnityWebRequest.Post("http://localhost:8080/api/orders/new", "POST");
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

