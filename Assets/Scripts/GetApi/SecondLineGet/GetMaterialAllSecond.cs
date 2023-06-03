using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialData;

public class GetMaterialAllSecond : MonoBehaviour
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

    
    public GameObject particlePrefab;
    public GameObject particleWheelSpawn;
    public GameObject particleDoorSpawn;
    public float particleDuration = 2.0f;

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
                            MaterialListSecond materialList = JsonUtility.FromJson<MaterialListSecond>("{\"materials\":" + json + "}");
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
                if (material.materialName == "CarWheels_Second")
                {
                    for (int i = 0; i < material.stock; i++) // �ִ� ���� ������ ����
                    {
                        Debug.Log("�Ҹ���");
                        SoundManager.Instance.PlaySound(audioClip); // �Ҹ� ���
                        StartCoroutine(CreateWheelParticle());
                        Debug.Log("���� ����");
                        Instantiate(wheelPrefab, wheelspawn.transform.position, Quaternion.identity);
                        yield return new WaitForSeconds(5.0f);
                    }
                }
                // if (material.materialName == "CarDoors")
                // {
                    
                    
                //     for (int i = 0; i < material.stock; i++) // �ִ� ���� ������ ����
                //     {
                //         SoundManager.Instance.PlaySound(audioClip); // �Ҹ� ���
                //         StartCoroutine(CreateDoorParticle());
                //         Debug.Log("�� ����");
                //         Instantiate(doorPrefab, doorspawn.transform.position, Quaternion.Euler(0, 180, 0));
                //         yield return new WaitForSeconds(5.0f); // ��� �ð��� 5�ʷ� �����մϴ�.
                //     }
                // }
                
            }
        }

        isProcessingOrders = false;
    }
    // IEnumerator CreateDoorParticle()
    // {
    //     Vector3 particlePosition = new Vector3(particleDoorSpawn.transform.position.x, particleDoorSpawn.transform.position.y, particleDoorSpawn.transform.position.z - 2f);
    //     GameObject particle = Instantiate(particlePrefab, particlePosition, Quaternion.Euler(0, 180, 0));
    //     yield return new WaitForSeconds(particleDuration);
    //     Destroy(particle);
    // }
    IEnumerator CreateWheelParticle()
    {
        Vector3 particlePosition = new Vector3(particleWheelSpawn.transform.position.x, particleWheelSpawn.transform.position.y, particleWheelSpawn.transform.position.z - 2f);
        GameObject particle = Instantiate(particlePrefab, particlePosition, Quaternion.Euler(0, 180, 0));
        yield return new WaitForSeconds(particleDuration);
        Destroy(particle);
    }
}

[System.Serializable]
public class MaterialListSecond
{
    public List<CarMaterial> materials;
}

