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

private bool isProcessingOrders = false; // ó�� ���� �ֹ��� �ִ��� ���θ� ��Ÿ���ϴ�.

void Start()
{
    StartCoroutine(WaitForGet());
}

IEnumerator WaitForGet()
{
    while (true)
    {
        Debug.Log(isProcessingOrders);
        if (!isProcessingOrders) // ó�� ���� �ֹ��� ���� ���� GET�� �޽��ϴ�.
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
                        // JSON �迭�� �Ľ��մϴ�.
                        MaterialList orderList = JsonUtility.FromJson<MaterialList>("{\"material\":" + json + "}");
                        if (orderList != null && orderList.orders != null)
                        {
                            newOrders = orderList.orders;
                        }
                    }

                    Debug.Log("�Ծ�?");
                    // ������ ó���� �ֹ� ��ϰ� ���ο� �ֹ� ����� ���մϴ�.
                    List<CarMaterial> addedOrders = new List<CarMaterial>();

                    foreach (CarMaterial newOrder in newOrders)
                    {
                        Debug.Log("����");
                    }




                    foreach (CarMaterial newOrder in newOrders)
                    {
                        // bool found = false;
                        // Debug.Log("�Գ�?1");
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
                        Debug.Log("�Գ�?1");
                        foreach (CarMaterial order in orders)
                        {
                            Debug.Log("�Գ�?2");
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
        Debug.Log("������");
    }
}
  IEnumerator GetOrders(List<CarMaterial> orders)
{
    isProcessingOrders = true; // ó�� ���� �ֹ��� ������ ��Ÿ���ϴ�.
    Debug.Log("�Գ�?3");
    foreach (CarMaterial order in orders)
    {
        if (order.materialName == "CarDoors")
        {
            for (int i = 0; i < order.stock; i++)
            {
                Instantiate(doorPrefab, doorspawn.transform.position, Quaternion.Euler(0,180,0));
                yield return new WaitForSeconds(5.0f); // ��� �ð��� 5�ʷ� �����մϴ�.
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

        isProcessingOrders = false; // ó�� ���� �ֹ��� ������ ��Ÿ���ϴ�.
    }
}

[System.Serializable]
public class MaterialList
{
    public List<CarMaterial> orders;
}