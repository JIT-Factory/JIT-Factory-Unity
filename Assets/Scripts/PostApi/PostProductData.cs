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
        // ��û ���� ����
        string json = "{\"productName\":\"VolvoCar\",\"status\":\"success\",\"sales\":1000000,\"reason\":\"-\"}";
        byte[] data = Encoding.UTF8.GetBytes(json);

        // HTTP ��û �޽��� ����
        string url = "http://localhost:8080/api/product/add";
        WWW www = new WWW(url, data, new Dictionary<string, string>() {
            { "Content-Type", "application/json" }
        });

        yield return www; // �����κ����� ���� ���
        if (www.error != null) {
            Debug.Log("Error: " + www.error);
        } else {
            Debug.Log("Response: " + www.text);
        }
    }
}
