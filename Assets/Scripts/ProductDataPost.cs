using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProductDataPost : MonoBehaviour
{
    
    public void SendRequest()
{
    StartCoroutine(PostRequest());
}

IEnumerator PostRequest()
{
     // ProData ��ü�� �����Ͽ� ��ǰ ������ ������
        ProData proData = new ProData();
        ProductData data = proData.GetProductData();

        // ������ ����ȭ
        string json = JsonUtility.ToJson(data);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        // HTTP POST ��û ����
        string url = "http://localhost:8080/api/product/add";
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        if (www.result != UnityWebRequest.Result.Success)
        
        yield return www.SendWebRequest();


        // ��û ��� ó��
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
        }
}
}

[System.Serializable]
public class ProductData
{
    public string productName;
    public string status;
    public int sales;
    public string reason;

    public ProductData(string productName, string status, int sales, string reason)
    {
        this.productName = productName;
        this.status = status;
        this.sales = sales;
        this.reason  = reason;
    }
}