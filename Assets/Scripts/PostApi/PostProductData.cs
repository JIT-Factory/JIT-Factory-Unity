using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
public class PostProductData : MonoBehaviour
{
void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
        StartCoroutine(PostData());
    }

    IEnumerator PostData() {
        // 요청 본문 생성
        string json = "{\"productName\":\"VolvoCar\",\"status\":\"success\",\"sales\":1000000,\"reason\":\"-\"}";
        byte[] data = Encoding.UTF8.GetBytes(json);

        // HTTP 요청 메시지 생성
        string url = "http://localhost:8080/api/product/add";
        WWW www = new WWW(url, data, new Dictionary<string, string>() {
            { "Content-Type", "application/json" }
        });

        yield return www; // 서버로부터의 응답 대기
        if (www.error != null) {
            Debug.Log("Error: " + www.error);
        } else {
            Debug.Log("Response: " + www.text);
        }
    }
}
