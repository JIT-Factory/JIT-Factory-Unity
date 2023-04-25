using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialData;
public class GetMaterialAll : MonoBehaviour
{
    [SerializeField] 
    private GameObject wheelPrefab;
    [SerializeField] 
    private GameObject wheelspawn;
     [SerializeField] 
    private GameObject doorPrefab;
    [SerializeField] 
    private GameObject doorspawn;

    private List<CarMaterial> orders = new List<CarMaterial>();

private bool isProcessingOrders = false; // 처리 중인 주문이 있는지 여부를 나타냅니다.

void Start()
{
    StartCoroutine(WaitForGet());
}

IEnumerator WaitForGet()
{
    while (true)
    {
        Debug.Log(isProcessingOrders);
        if (!isProcessingOrders) // 처리 중인 주문이 없을 때만 GET을 받습니다.
        {
            using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/api/material/all"))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    string json = www.downloadHandler.text;
                    Debug.Log(json);

                    List<CarMaterial> newOrders = new List<CarMaterial>();
                    if (!string.IsNullOrEmpty(json))
                    {
                        // JSON 배열을 파싱합니다.
                        MaterialList orderList = JsonUtility.FromJson<MaterialList>("{\"material\":" + json + "}");
                        if (orderList != null && orderList.orders != null)
                        {
                            newOrders = orderList.orders;
                        }
                    }

                    Debug.Log("왔어?");
                    // 이전에 처리한 주문 목록과 새로운 주문 목록을 비교합니다.
                    List<CarMaterial> addedOrders = new List<CarMaterial>();

                    foreach (CarMaterial newOrder in newOrders)
                    {
                        Debug.Log("싯팔");
                    }




                    foreach (CarMaterial newOrder in newOrders)
                    {
                        // bool found = false;
                        // Debug.Log("왔냐?1");
                        // foreach (CarMaterial order in orders)
                        // {
                        //         if (order.materialName == "CarDoors" && newOrder.materialName == "CarDoors" && order.stock != newOrder.stock)
                        //         {
                        //             found = true;
                        //             break;
                        //         }
                        // }
                        // if (!found)
                        // {
                        //         addedOrders.Add(newOrder);
                        // }
                        bool found = false;
                        Debug.Log("왔나?1");
                        foreach (CarMaterial order in orders)
                        {
                            Debug.Log("왔나?2");
                            if (order.materialName == "CarDoors" && newOrder.materialName == "CarDoors" && order.stock != newOrder.stock)
                            {
                                found = true;
                                break;
                            }
                            else if (order.materialName == "CarWheels" && newOrder.materialName == "CarWheels" && order.stock != newOrder.stock)
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            addedOrders.Add(newOrder);
                        }
                    }

                    // 새로운 주문 항목에 대한 처리를 시작합니다.
                    if (addedOrders.Count > 0)
                    {
                        StartCoroutine(GetOrders(addedOrders));
                    }

                    // 주문 목록을 업데이트합니다.
                    orders = newOrders;
                }
            }
        }

        yield return new WaitForSeconds(1.0f);
        Debug.Log("왔을까");
    }
}
  IEnumerator GetOrders(List<CarMaterial> orders)
{
    isProcessingOrders = true; // 처리 중인 주문이 있음을 나타냅니다.
    Debug.Log("왔나?3");
    foreach (CarMaterial order in orders)
    {
        if (order.materialName == "CarDoors")
        {
            for (int i = 0; i < order.stock; i++)
            {
                Instantiate(doorPrefab, doorspawn.transform.position, Quaternion.Euler(0,180,0));
                yield return new WaitForSeconds(5.0f); // 대기 시간을 5초로 설정합니다.
            }
        }
        else if (order.materialName == "CarWheels")
        {
            for (int i = 0; i < order.stock; i++)
            {
                Instantiate(wheelPrefab, wheelspawn.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(5.0f); //
                }
            }
        }

        isProcessingOrders = false; // 처리 중인 주문이 없음을 나타냅니다.
    }
}

[System.Serializable]
public class MaterialList
{
    public List<CarMaterial> orders;
}