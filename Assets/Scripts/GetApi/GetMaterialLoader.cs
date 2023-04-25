using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetMaterialLoader : MonoBehaviour
{
    private string baseUrl = "http://localhost:8080/api/material/all";
    public GameObject carInterior;
    public GameObject carBody;
    public GameObject carWheels;
    public GameObject carDoors;

    public GameObject spawnInterior;
    public GameObject spawnBody;
    public GameObject spawnWheels;
    public GameObject spawnDoors;

    // ������ �� Ŭ����
    [System.Serializable]
    public class MaterialInfo
    {
        public int id;
        public string materialName;
        public int stock;
    }

    [System.Serializable]
    public class MaterialList
    {
        public List<MaterialInfo> materials;
    }

    // GET ��û�� ������ �޼���
    public void SendGetRequest()
    {
        StartCoroutine(GetMaterials());
    }

    IEnumerator GetMaterials()
    {
        UnityWebRequest request = UnityWebRequest.Get(baseUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("HTTP Error: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;

            // {} �߰�ȣ�� JSON ���ڿ� �����ֱ�
            jsonResponse = "{ \"materials\": " + jsonResponse + " }";

            // ����� JSON ���ڿ��� ��ü(Object)�� ��ȯ�ϱ�
            MaterialList materialList = JsonUtility.FromJson<MaterialList>(jsonResponse);

            // ������ ������ ����ϱ�
            foreach (MaterialInfo info in materialList.materials)
            {
                // switch (info.materialName)
                // {
                //     case "CarInterior":
                //         // Instantiate(carInterior, spawnInterior.transform.position, Quaternion.Euler(0, 180, 0));
                //         info.stock -= 1;
                //         Debug.Log("���� CarInterior");
                //         break;

                //     case "CarBody":
                //         // Instantiate(carBody, spawnBody.transform.position, Quaternion.identity);
                //         info.stock -= 1;
                //         Debug.Log("���� CarBody");
                //         break;

                //     case "CarWheels":
                //         // Instantiate(carWheels, spawnWheels.transform.position, Quaternion.identity);
                //         info.stock -= 4;
                //         Debug.Log("���� CarWheels");
                //         break;

                //     case "CarDoors":
                //         // Instantiate(carDoors, spawnDoors.transform.position, Quaternion.identity);
                //         info.stock -= 2;
                //         Debug.Log("���� CarDoors");
                //         Debug.Log(spawnDoors.transform.position);
                //         break;

                //     default:
                //         Debug.LogError("Unknown material name: " + info.materialName);
                //         break;
                // }

                // ����� stock ���� ����ϱ�
                Debug.Log("ID: " + info.id + ", MaterialName: " + info.materialName + ", Stock: " + info.stock);
            }
        }
    }
}