using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class SendFactoryUpdate : MonoBehaviour
{
    public string url = "http://localhost:8080/api/factory/update";
    public string factoryName = "JITFactory";
    public string factoryStatus = "stop";
    public string insultBelt = "stop";
    public string productionBelt = "stop";
    public string inspectionProductABelt = "stop";
    public string inspectionProductBBelt = "stop";
    public string inspectionProductCBelt = "stop";

    public void SendUpdate()
    {
        // Create a JSON object
        Dictionary<string, string> json = new Dictionary<string, string>();
        json.Add("factoryName", factoryName);
        json.Add("factoryStatus", factoryStatus);
        json.Add("insultBelt", insultBelt);
        json.Add("productionBelt", productionBelt);
        json.Add("inspectionProductABelt", inspectionProductABelt);
        json.Add("inspectionProductBBelt", inspectionProductBBelt);
        json.Add("inspectionProductCBelt", inspectionProductCBelt);

        // Convert the dictionary to JSON
        string jsonData = JsonConvert.SerializeObject(json);

        // Create a UnityWebRequest object
        UnityWebRequest www = UnityWebRequest.Post(url, jsonData);

        // Set the content type of the request to JSON
        www.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for a response
        StartCoroutine(SendRequest(www));
    }

    IEnumerator SendRequest(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending factory update: " + www.error);
        }
        else
        {
            Debug.Log("Factory update sent successfully!");
        }
    }
}