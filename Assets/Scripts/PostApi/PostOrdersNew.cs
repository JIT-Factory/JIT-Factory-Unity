using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using OrderData;

public class PostOrdersNew : MonoBehaviour
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

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/api/orders/new", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("POST success!");
            }
            else
            {
                Debug.Log("POST failed!");
            }
        }
        
    }
    
}
