using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

public class PostProductDataSecond : MonoBehaviour
{
    public ProData2 proData; // ProData를 ScriptableObject 타입으로 변경
    public AudioClip audioClip;
    public GameObject particlePrefab;
    public float particleDuration = 2.0f;
    
    void OnTriggerEnter(Collider other)
    {
        SoundManager.Instance.PlaySound(audioClip);
        StartCoroutine(CreateParticle());
        Destroy(other.gameObject);
        StartCoroutine(PostData());
    }

    IEnumerator PostData()
    {
        // ScriptableObject.CreateInstance 메서드를 사용하여 ProData 인스턴스 생성
        proData = ScriptableObject.CreateInstance<ProData2>();

        string url = "http://localhost:8080/api/product/add";
        string jsonBody = JsonUtility.ToJson(proData);

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
        request.Dispose();
    }

    IEnumerator CreateParticle()
    {
        Vector3 particlePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject particle = Instantiate(particlePrefab, particlePosition, Quaternion.Euler(0, 90, 0));
        yield return new WaitForSeconds(particleDuration);
        Destroy(particle);
    }
}

