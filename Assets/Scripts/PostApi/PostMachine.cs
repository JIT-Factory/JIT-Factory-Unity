using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PostMachine : MonoBehaviour
{
    ProductData data;
    public void SendRequest()
{
    
}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        //ProData 객체를 생성하여 제품 정보를 가져옴
        ProData proData = new ProData();
        data = other.transform.GetComponent<ProData>().GetProductData();

        StartCoroutine(PostRequest());
    }
    // private void OnCollisionEnter(Collision other) {

    //     //ProData 객체를 생성하여 제품 정보를 가져옴
    //     ProData proData = new ProData();
    //     data = other.transform.GetComponent<ProData>().GetProductData();

    //     StartCoroutine(PostRequest());

    //     //ProData proData = new ProData();

        

    //     //productDataPost.SendRequest(other);
    // }
    IEnumerator PostRequest()
{
        // 데이터 직렬화
        string json = JsonUtility.ToJson(data);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        // HTTP POST 요청 생성
        string url = "http://localhost:8080/api/product/add";
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        if (www.result != UnityWebRequest.Result.Success)
        
        yield return www.SendWebRequest();


        // 요청 결과 처리
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