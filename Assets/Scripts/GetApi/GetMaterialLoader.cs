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

    // 데이터 모델 클래스
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

    // GET 요청을 보내는 메서드
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

            // {} 중괄호로 JSON 문자열 감싸주기
            jsonResponse = "{ \"materials\": " + jsonResponse + " }";

            // 변경된 JSON 문자열을 객체(Object)로 변환하기
            MaterialList materialList = JsonUtility.FromJson<MaterialList>(jsonResponse);

            // 가공된 데이터 출력하기
            foreach (MaterialInfo info in materialList.materials)
            {
                // switch (info.materialName)
                // {
                //     case "CarInterior":
                //         // Instantiate(carInterior, spawnInterior.transform.position, Quaternion.Euler(0, 180, 0));
                //         info.stock -= 1;
                //         Debug.Log("생성 CarInterior");
                //         break;

                //     case "CarBody":
                //         // Instantiate(carBody, spawnBody.transform.position, Quaternion.identity);
                //         info.stock -= 1;
                //         Debug.Log("생성 CarBody");
                //         break;

                //     case "CarWheels":
                //         // Instantiate(carWheels, spawnWheels.transform.position, Quaternion.identity);
                //         info.stock -= 4;
                //         Debug.Log("생성 CarWheels");
                //         break;

                //     case "CarDoors":
                //         // Instantiate(carDoors, spawnDoors.transform.position, Quaternion.identity);
                //         info.stock -= 2;
                //         Debug.Log("생성 CarDoors");
                //         Debug.Log(spawnDoors.transform.position);
                //         break;

                //     default:
                //         Debug.LogError("Unknown material name: " + info.materialName);
                //         break;
                // }

                // 변경된 stock 정보 출력하기
                Debug.Log("ID: " + info.id + ", MaterialName: " + info.materialName + ", Stock: " + info.stock);
            }
        }
    }
}