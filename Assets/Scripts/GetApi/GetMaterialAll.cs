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
    private string factoryName; // 검색할 공장 이름
    public AudioClip audioClip;

    private List<CarMaterial> materials = new List<CarMaterial>();
    private bool isProcessingOrders = false; // 처리 중인 주문이 있는지 여부를 나타냅니다.

    [SerializeField]
    private int maxCreationCount = 3; // 최대 생성 수량 설정

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
                    
                    for (int i = 0; i < Mathf.Min(material.stock, maxCreationCount); i++) // 최대 생성 수량을 제한
                    {
                        SoundManager.Instance.PlaySound(audioClip); // 소리 재생
                        Debug.Log("문 생성");
                        Instantiate(doorPrefab, doorspawn.transform.position, Quaternion.Euler(0, 180, 0));
                        yield return new WaitForSeconds(5.0f); // 대기 시간을 5초로 설정합니다.
                    }
                }
                else if (material.materialName == "CarWheels")
                {
                    maxCreationCount = material.stock;
                    
                    for (int i = 0; i < Mathf.Min(material.stock, maxCreationCount); i++) // 최대 생성 수량을 제한
                    {
                        SoundManager.Instance.PlaySound(audioClip); // 소리 재생
                        Debug.Log("바퀴 생성");
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
