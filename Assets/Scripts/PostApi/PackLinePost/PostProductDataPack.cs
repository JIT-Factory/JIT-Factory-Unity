using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

public class PostProductDataPack : MonoBehaviour
{
    BeerData beerData;
    PotionBigData potionBigData;
    WineBottleData wineBottleData;
    WineJugData wineJugData;
    public GameObject particlePrefab;
    public float particleDuration = 2.0f;

private void OnCollisionEnter(Collision other) {
    StartCoroutine(CreateParticle());
    Destroy(other.gameObject);

    if (other.transform.CompareTag("Wrapped_Boxes_Beer"))
    {
        Debug.Log("1");
        beerData = ScriptableObject.CreateInstance<BeerData>();
        StartCoroutine(PostData(beerData));
    }
    else if (other.transform.CompareTag("Wrapped_Boxes_PotionBig"))
    {
        potionBigData = ScriptableObject.CreateInstance<PotionBigData>();
        StartCoroutine(PostData(potionBigData));
    }
    else if (other.transform.CompareTag("Wrapped_Boxes_WineBottle"))
    {
        wineBottleData = ScriptableObject.CreateInstance<WineBottleData>();
        StartCoroutine(PostData(wineBottleData));
    }
    else if (other.transform.CompareTag("Wrapped_Boxes_WineJug"))
    {
        wineJugData = ScriptableObject.CreateInstance<WineJugData>();
        StartCoroutine(PostData(wineJugData));
    }
    else
    {
        Debug.LogWarning("Unhandled object tag: " + other.transform.tag);
    }
}
    void OnTriggerEnter(Collider other)
{
    
}


    IEnumerator PostData(ScriptableObject data)
    {
        string url = "http://localhost:8080/api/product/add";
        string jsonBody = JsonUtility.ToJson(data);

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
