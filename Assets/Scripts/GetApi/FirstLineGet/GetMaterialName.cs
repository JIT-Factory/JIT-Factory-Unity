using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetMaterialName : MonoBehaviour
{
    [SerializeField] 
    private GameObject wheelPrefab;
    [SerializeField] 
    private GameObject wheelspawn;
    [SerializeField] 
    private GameObject doorPrefab;
    [SerializeField] 
    private GameObject doorspawn;
    [SerializeField] 
    private GameObject beltDoor;
    [SerializeField] 
    private GameObject beltWheel;

    [SerializeField] 
    private string factoryName = "CarFactory";
    private int previousStock = 0;

    void Start()
    {
        StartCoroutine(WaitForGet());
    }

    IEnumerator WaitForGet()
    {
        string url = "http://localhost:8080/api/material/name/" + UnityWebRequest.EscapeURL(factoryName);

        // UnityWebRequest 객체 생성
        UnityWebRequest www = UnityWebRequest.Get(url);

        // 요청 보내고 응답 대기
        yield return www.SendWebRequest();

        // 응답 처리
        if (www.result == UnityWebRequest.Result.Success)
        {
            // 응답 받은 텍스트 출력
            Debug.Log(www.downloadHandler.text);

            // 응답 받은 텍스트를 JSON 형식으로 파싱
            YourData[] responseData = JsonUtility.FromJson<YourData[]>(www.downloadHandler.text);

            // 데이터 처리
            foreach (YourData data in responseData)
            {
                // 데이터 접근 예시
                Debug.Log("Factory Name: " + data.factoryName);
                Debug.Log("Material Name: " + data.materialName);
                Debug.Log("Stock: " + data.stock);

                // count 변화가 있을 때 제품 생성
                if (previousStock != 0 && data.stock != previousStock)
                {
                    GenerateProduct();
                }

                previousStock = data.stock;
            }
        }
        else
        {
            // 요청 실패 또는 오류 처리
            Debug.Log("Error: " + www.error);
        }
    }

    void GenerateProduct()
    {
        // 제품 생성 로직 추가
        Instantiate(wheelPrefab, wheelspawn.transform.position, Quaternion.identity);
        Debug.Log("이건 대체" + beltDoor.GetComponent<ConveyorBeltBack>().machineOperation);
        // machineOperation 상태 확인 후 문 생성
        if (beltDoor.GetComponent<ConveyorBeltBack>().machineOperation == true)
        {
            Debug.Log("문짝 생성");
        }
        if(beltWheel.GetComponent<ConveyorBeltBack>().machineOperation == true)
        {
            Debug.Log("휠 생성");
        }
    }
}

// 응답 데이터를 담을 클래스
[System.Serializable]
public class YourData
{
    public string factoryName;
    public string materialName;
    public int stock;
}
