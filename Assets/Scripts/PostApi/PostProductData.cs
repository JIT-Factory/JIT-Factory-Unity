using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

public class PostProductData : MonoBehaviour
{
    private ProData proData;
    public AudioClip audioClip;
    public GameObject particlePrefab;
    public float particleDuration = 2.0f;
    void OnTriggerEnter(Collider other)
    {
        SoundManager.Instance.PlaySound(audioClip); // 소리 재생
         StartCoroutine(CreateParticle());
        Destroy(other.gameObject);
        StartCoroutine(PostData());
    }

    IEnumerator PostData()
    {
        // ProductData 인스턴스 생성 및 값 설정
        proData = new ProData();

        string url = "http://localhost:8080/api/product/add";
        // JSON으로 직렬화
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
    IEnumerator CreateParticle()
    {
        Vector3 particlePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject particle = Instantiate(particlePrefab, particlePosition, Quaternion.Euler(0, 90, 0));
        yield return new WaitForSeconds(particleDuration);
        Destroy(particle);
    }
}