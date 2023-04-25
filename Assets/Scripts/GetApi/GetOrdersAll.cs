using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OrderData;

public class GetOrdersAll : MonoBehaviour
{
    [SerializeField] 
    private GameObject orderObjectPrefab;
    [SerializeField] 
    private GameObject spawn;

    private List<Order> orders = new List<Order>();

    private bool isProcessingOrders = false; // 처리 중인 주문이 있는지 여부를 나타냅니다.

    void Start()
    {
        StartCoroutine(WaitForGet());
    }

    IEnumerator WaitForGet()
    {
        while (true)
        {
            if (!isProcessingOrders) // 처리 중인 주문이 없을 때만 GET을 받습니다.
            {
                using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/api/orders/all"))
                {
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        string json = www.downloadHandler.text;
                        Debug.Log(json);

                        List<Order> newOrders = new List<Order>();
                        if (!string.IsNullOrEmpty(json))
                        {
                            // JSON 배열을 파싱합니다.
                            OrderList orderList = JsonUtility.FromJson<OrderList>("{\"orders\":" + json + "}");
                            if (orderList != null && orderList.orders != null)
                            {
                                newOrders = orderList.orders;
                            }
                        }

                        // 이전에 처리한 주문 목록과 새로운 주문 목록을 비교합니다.
                        List<Order> addedOrders = new List<Order>();
                        foreach (Order newOrder in newOrders)
                        {
                            bool found = false;
                            foreach (Order order in orders)
                            {
                                if (order.count == newOrder.count)
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
        }
    }

    IEnumerator GetOrders(List<Order> orders)
    {
        isProcessingOrders = true; // 처리 중인 주문이 있음을 나타냅니다.

        foreach (Order order in orders)
        {
            for (int i = 0; i < order.count; i++)
            {
                Instantiate(orderObjectPrefab, spawn.transform.position, Quaternion.Euler(0,180,0));
                yield return new WaitForSeconds(5.0f); // 대기 시간을 10초로 설정합니다.
            }
        }

        isProcessingOrders = false; // 처리 중인 주문이 없음을 나타냅니다.
    }
}

[System.Serializable]
public class OrderList
{
    public List<Order> orders;
}