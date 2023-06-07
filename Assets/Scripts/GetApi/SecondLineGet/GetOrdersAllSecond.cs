using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OrderData;

public class GetOrdersAllSecond : MonoBehaviour
{
    [SerializeField]
    private GameObject orderObjectPrefab;
    [SerializeField]
    private GameObject spawn;
    public AudioClip audioClip;
    private List<Order> orders = new List<Order>();

    private bool isProcessingOrders = false; // 처리 중인 주문이 있는지 여부를 나타냅니다.
    private bool isMachineRunning = true; // 기계의 작동 여부를 나타냅니다. 처음에는 기계가 작동 중인 상태로 설정합니다.

    [SerializeField]
    private string factoryName; // 검색할 공장 이름
    [SerializeField]
    private string productName; // 검색할 제품 이름

    public GameObject particlePrefab;
    public GameObject particleSpawn;
    public float particleDuration = 2.0f;

    public GameObject belt;
    void Start()
    {
        StartCoroutine(WaitForGet());
    }

    IEnumerator WaitForGet()
    {
        while (true)
        {
            if (!isProcessingOrders && belt.GetComponent<GetApiProcessSecond>().GetStaus()) // 처리 중인 주문이 없고 기계가 작동 중인 경우에만 GET을 받습니다.
            {
                using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/api/orders/name/" + factoryName))
                {
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        string json = www.downloadHandler.text;

                        List<Order> newOrders = new List<Order>();
                        if (!string.IsNullOrEmpty(json))
                        {
                            OrderList orderList = JsonUtility.FromJson<OrderList>("{\"orders\":" + json + "}");
                            if (orderList != null && orderList.orders != null)
                            {
                                newOrders = orderList.orders;
                            }
                        }

                        List<Order> addedOrders = new List<Order>();
                        foreach (Order newOrder in newOrders)
                        {
                            bool found = false;
                            foreach (Order order in orders)
                            {
                                if (order.productName == newOrder.productName && order.count != newOrder.count)
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

                        if (addedOrders.Count > 0)
                        {
                            StartCoroutine(GetOrders(addedOrders));
                        }

                        orders = newOrders;
                    }
                }
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator GetOrders(List<Order> orders)
    {
        isProcessingOrders = true;

        foreach (Order order in orders)
        {
            if (order.factoryName == factoryName && order.productName == productName)
            {
                for (int i = 0; i < order.count; i++)
                {
                    Instantiate(orderObjectPrefab, spawn.transform.position, Quaternion.Euler(0, 180, 0));
                    StartCoroutine(CreateParticle());
                    yield return new WaitForSeconds(20.0f);
                }
            }
        }

        isProcessingOrders = false;
    }

    IEnumerator CreateParticle()
    {
        GameObject particle = Instantiate(particlePrefab, particleSpawn.transform.position, Quaternion.Euler(0, 90, 0));
        yield return new WaitForSeconds(particleDuration);
        Destroy(particle);
    }

    public void StartMachine()
    {
        isMachineRunning = true;
    }

    public void StopMachine()
    {
        isMachineRunning = false;
    }
}

[System.Serializable]
public class OrderListSecond
{
    public List<Order> orders;
}

