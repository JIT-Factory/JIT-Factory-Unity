    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Product2Create : MonoBehaviour
    {
        public GameObject[] products;  // ������ ������Ʈ���� �迭�� ����
        public GameObject ProductButton2;
        bool ProductBool2;
        public GameObject CreateSpawn;
        Vector3 vector3;
        public GameObject ProductButton;
        float lastSpawnTime; // ���������� ������Ʈ�� ������ �ð�
    float spawnInterval = 3f; // ������Ʈ ���� �ֱ�
        void Awake() {
            ProductBool2 = ProductButton2.GetComponent<Product2Btn>().GetProButton2();
        }
        // Start is called before the first frame update
        void Start()
        {
            vector3 = new Vector3(CreateSpawn.transform.position.x,CreateSpawn.transform.position.y-1.0f,CreateSpawn.transform.position.z);
        }

        // Update is called once per frame
        void Update()
        {
            ProductBool2 = ProductButton2.GetComponent<Product2Btn>().GetProButton2();
            Debug.Log( "��ǰ2 ��ư"+ ProductBool2);

            if( ProductButton.GetComponent<PlayMachine>().GetmachineOper() == true && ProductBool2 == true)
            {
                 if (Time.time - lastSpawnTime >= spawnInterval) {
                lastSpawnTime = Time.time;
                int randomIndex = Random.Range(0, products.Length);
                Instantiate(products[randomIndex], vector3, Quaternion.identity);
            }
            }
        }
    }
