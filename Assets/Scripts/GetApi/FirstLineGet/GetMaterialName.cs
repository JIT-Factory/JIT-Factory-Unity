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

        // UnityWebRequest ��ü ����
        UnityWebRequest www = UnityWebRequest.Get(url);

        // ��û ������ ���� ���
        yield return www.SendWebRequest();

        // ���� ó��
        if (www.result == UnityWebRequest.Result.Success)
        {
            // ���� ���� �ؽ�Ʈ ���
            Debug.Log(www.downloadHandler.text);

            // ���� ���� �ؽ�Ʈ�� JSON �������� �Ľ�
            YourData[] responseData = JsonUtility.FromJson<YourData[]>(www.downloadHandler.text);

            // ������ ó��
            foreach (YourData data in responseData)
            {
                // ������ ���� ����
                Debug.Log("Factory Name: " + data.factoryName);
                Debug.Log("Material Name: " + data.materialName);
                Debug.Log("Stock: " + data.stock);

                // count ��ȭ�� ���� �� ��ǰ ����
                if (previousStock != 0 && data.stock != previousStock)
                {
                    GenerateProduct();
                }

                previousStock = data.stock;
            }
        }
        else
        {
            // ��û ���� �Ǵ� ���� ó��
            Debug.Log("Error: " + www.error);
        }
    }

    void GenerateProduct()
    {
        // ��ǰ ���� ���� �߰�
        Instantiate(wheelPrefab, wheelspawn.transform.position, Quaternion.identity);
        Debug.Log("�̰� ��ü" + beltDoor.GetComponent<ConveyorBeltBack>().machineOperation);
        // machineOperation ���� Ȯ�� �� �� ����
        if (beltDoor.GetComponent<ConveyorBeltBack>().machineOperation == true)
        {
            Debug.Log("��¦ ����");
        }
        if(beltWheel.GetComponent<ConveyorBeltBack>().machineOperation == true)
        {
            Debug.Log("�� ����");
        }
    }
}

// ���� �����͸� ���� Ŭ����
[System.Serializable]
public class YourData
{
    public string factoryName;
    public string materialName;
    public int stock;
}
