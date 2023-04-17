    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Product1Create : MonoBehaviour
    {
        public GameObject[] products;  // 생성할 오브젝트들을 배열로 선언
        public GameObject ProductButton1;
        bool ProductBool1;
        public GameObject CreateSpawn;
        Vector3 vector3;
        public GameObject ProductButton;
        float lastSpawnTime; // 마지막으로 오브젝트를 생성한 시간
    float spawnInterval = 3f; // 오브젝트 생성 주기
        void Awake() {
            ProductBool1 = ProductButton1.GetComponent<Product1Btn>().GetProButton1();
        }
        // Start is called before the first frame update
        void Start()
        {
            vector3 = new Vector3(CreateSpawn.transform.position.x,CreateSpawn.transform.position.y-1.0f,CreateSpawn.transform.position.z);
        }

        // Update is called once per frame
        void Update()
        {
            ProductBool1 = ProductButton1.GetComponent<Product1Btn>().GetProButton1();
            Debug.Log( "제품1 버튼"+ ProductBool1);

            if( ProductButton.GetComponent<PlayMachine>().GetmachineOper() == true && ProductBool1 == true)
            {
                 if (Time.time - lastSpawnTime >= spawnInterval) {
                lastSpawnTime = Time.time;
                int randomIndex = Random.Range(0, products.Length);
                Instantiate(products[randomIndex], vector3, Quaternion.identity);
            }
            }
        }
    }
