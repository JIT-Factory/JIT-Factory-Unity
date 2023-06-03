using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OrderData;

public class GetOrdersAllSecond  : MonoBehaviour
{
    [SerializeField]
    private GameObject orderObjectPrefab;
    [SerializeField]
    private GameObject spawn;
    public AudioClip audioClip;
    private List<Order> orders = new List<Order>();

    private bool isProcessingOrders = false; // 처리 중인 주문이 있는지 여부를 나타냅니다.

    [SerializeField]
    private string factoryName; // 검색할 공장 이름
     [SerializeField]
    private string productName; // 검색할 제품 이름

    public GameObject particlePrefab;
    public GameObject particleSpawn;
    public float particleDuration = 2.0f;

    void Start()
    {
        StartCoroutine(WaitForGet());
    }

    IEnumerator WaitForGet()
    {
        while (true)
        {
            //Debug.Log(isProcessingOrders);
            if (!isProcessingOrders) // 처리 중인 주문이 없을 때만 GET을 받습니다.
            {
                using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/api/orders/name/" + factoryName))
                {
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        string json = www.downloadHandler.text;
                        //Debug.Log(json);

                        List<Order> newOrders = new List<Order>();
                        if (!string.IsNullOrEmpty(json))
                        {
                            // JSON 배열을 파싱합니다.
                            OrderListSecond orderList = JsonUtility.FromJson<OrderListSecond>("{\"orders\":" + json + "}");
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
            if (order.factoryName == factoryName && order.productName == productName)
            {
                for (int i = 0; i < order.count; i++)
                {
                    SoundManager.Instance.PlaySound(audioClip); // 소리 재생
                    Debug.Log("두번째 라인 물건 생산 소리");
                    Instantiate(orderObjectPrefab, spawn.transform.position, Quaternion.Euler(0, 180, 0));
                    StartCoroutine(CreateParticle());
                    yield return new WaitForSeconds(5.0f); // 대기 시간을 10초로 설정합니다.
                }
            }
        }

        isProcessingOrders = false; // 처리 중인 주문이 없음을 나타냅니다.
    }
    IEnumerator CreateParticle()
    {
        
        GameObject particle = Instantiate(particlePrefab, particleSpawn.transform.position, Quaternion.Euler(0, 90, 0));
        yield return new WaitForSeconds(particleDuration);
        Destroy(particle);
    }
}

[System.Serializable]
public class OrderListSecond
{
    public List<Order> orders;
}

