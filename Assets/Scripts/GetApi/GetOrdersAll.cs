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

    private bool isProcessingOrders = false; // ó�� ���� �ֹ��� �ִ��� ���θ� ��Ÿ���ϴ�.

    void Start()
    {
        StartCoroutine(WaitForGet());
    }

    IEnumerator WaitForGet()
    {
        while (true)
        {
            if (!isProcessingOrders) // ó�� ���� �ֹ��� ���� ���� GET�� �޽��ϴ�.
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
                            // JSON �迭�� �Ľ��մϴ�.
                            OrderList orderList = JsonUtility.FromJson<OrderList>("{\"orders\":" + json + "}");
                            if (orderList != null && orderList.orders != null)
                            {
                                newOrders = orderList.orders;
                            }
                        }

                        // ������ ó���� �ֹ� ��ϰ� ���ο� �ֹ� ����� ���մϴ�.
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

                        // ���ο� �ֹ� �׸� ���� ó���� �����մϴ�.
                        if (addedOrders.Count > 0)
                        {
                            StartCoroutine(GetOrders(addedOrders));
                        }

                        // �ֹ� ����� ������Ʈ�մϴ�.
                        orders = newOrders;
                    }
                }
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator GetOrders(List<Order> orders)
    {
        isProcessingOrders = true; // ó�� ���� �ֹ��� ������ ��Ÿ���ϴ�.

        foreach (Order order in orders)
        {
            for (int i = 0; i < order.count; i++)
            {
                Instantiate(orderObjectPrefab, spawn.transform.position, Quaternion.Euler(0,180,0));
                yield return new WaitForSeconds(5.0f); // ��� �ð��� 10�ʷ� �����մϴ�.
            }
        }

        isProcessingOrders = false; // ó�� ���� �ֹ��� ������ ��Ÿ���ϴ�.
    }
}

[System.Serializable]
public class OrderList
{
    public List<Order> orders;
}