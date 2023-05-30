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

    [SerializeField]
    private string factoryName; // �˻��� ���� �̸�
    public AudioClip audioClip;

    private List<CarMaterial> materials = new List<CarMaterial>();
    private bool isProcessingOrders = false; // ó�� ���� �ֹ��� �ִ��� ���θ� ��Ÿ���ϴ�.

    [SerializeField]
    private int maxCreationCount = 3; // �ִ� ���� ���� ����

    void Start()
    {
        StartCoroutine(WaitForGet());
    }

    IEnumerator WaitForGet()
    {
        while (true)
        {
            if (!isProcessingOrders)
            {
                using (UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/api/material/name/" + factoryName))
                {
                    yield return www.SendWebRequest();
                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        string json = www.downloadHandler.text;
                        Debug.Log(json);

                        List<CarMaterial> newMaterials = new List<CarMaterial>();
                        if (!string.IsNullOrEmpty(json))
                        {
                            MaterialList materialList = JsonUtility.FromJson<MaterialList>("{\"materials\":" + json + "}");
                            if (materialList != null && materialList.materials != null)
                            {
                                newMaterials = materialList.materials;
                            }
                        }

                        List<CarMaterial> addedMaterials = new List<CarMaterial>();
                        foreach (CarMaterial newMaterial in newMaterials)
                        {
                            bool found = false;
                            foreach (CarMaterial material in materials)
                            {
                                if (material.materialName == newMaterial.materialName && material.stock != newMaterial.stock)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                addedMaterials.Add(newMaterial);
                            }
                        }

                        if (addedMaterials.Count > 0)
                        {
                            StartCoroutine(GetMaterials(addedMaterials));
                        }

                        materials = newMaterials;
                    }
                }
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator GetMaterials(List<CarMaterial> materials)
    {
        isProcessingOrders = true;

        foreach (CarMaterial material in materials)
        {
            if (material.factoryName == factoryName)
            {
                if (material.materialName == "CarDoors")
                {
                    maxCreationCount = material.stock;
                    
                    for (int i = 0; i < Mathf.Min(material.stock, maxCreationCount); i++) // �ִ� ���� ������ ����
                    {
                        SoundManager.Instance.PlaySound(audioClip); // �Ҹ� ���
                        Debug.Log("�� ����");
                        Instantiate(doorPrefab, doorspawn.transform.position, Quaternion.Euler(0, 180, 0));
                        yield return new WaitForSeconds(5.0f); // ��� �ð��� 5�ʷ� �����մϴ�.
                    }
                }
                else if (material.materialName == "CarWheels")
                {
                    maxCreationCount = material.stock;
                    
                    for (int i = 0; i < Mathf.Min(material.stock, maxCreationCount); i++) // �ִ� ���� ������ ����
                    {
                        SoundManager.Instance.PlaySound(audioClip); // �Ҹ� ���
                        Debug.Log("���� ����");
                        Instantiate(wheelPrefab, wheelspawn.transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(5.0f);
                    }
                }
            }
        }

        isProcessingOrders = false;
    }
}

[System.Serializable]
public class MaterialList
{
    public List<CarMaterial> materials;
}
