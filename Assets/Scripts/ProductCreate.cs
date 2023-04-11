using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductCreate : MonoBehaviour
{
    // public GameObject Product1;
    // public GameObject Product2;
    // public GameObject Product3;
   
   GameObject Product1;
   GameObject Product2;
   GameObject Product3;
    GameObject randomProduct;
    int randomIndex;
    
    // Create an array of products
    public GameObject CreateSpawn;
    public bool PlayButton;

    GameObject ProductButton;
    bool ProductBtn1;
    bool ProductBtn2;
    bool ProductBtn3;
    private Vector3 vector3;
     void Start()
     {
        Product1 = Resources.Load("Product1") as GameObject;
        Product2 = Resources.Load("Product2") as GameObject;
        Product3 = Resources.Load("Product3") as GameObject;

        ProductButton = GameObject.Find("ProductManagement");

        
        
        PlayButton = GameObject.Find("ProductManagement");

        

        vector3 = new Vector3(CreateSpawn.transform.position.x,CreateSpawn.transform.position.y-1.0f,CreateSpawn.transform.position.z);
     }
    // Update is called once per frame
    void Update()
    {
        ProductBtn1 = ProductButton.GetComponent<ProductButton>().GetProButton1();
        ProductBtn2 = ProductButton.GetComponent<ProductButton>().GetProButton2();
        ProductBtn3 = ProductButton.GetComponent<ProductButton>().GetProButton3();
        
        if( ProductButton.GetComponent<ManagementMachine>().GetMachineOnOff() == true)
        {
            
            if(ProductBtn1 == true)
            {
                // 1초마다 Create() 함수를 호출합니다.
                InvokeRepeating("ProCreate1", 0f, 1f);
            }
            if(ProductBtn2 == true)
            {
                // 1초마다 Create() 함수를 호출합니다.
                InvokeRepeating("ProCreate2", 0f, 1f);
            }
            if(ProductBtn3 == true)
            {
                // 1초마다 Create() 함수를 호출합니다.
                InvokeRepeating("ProCreate3", 0f, 1f);
            }
            
        }
        
    }
    public void Create()
    {
        // GameObject[] products = { Product1, Product2, Product3 };

        // // Select a random product from the array
        // randomIndex = Random.Range(0, products.Length);
        // randomProduct = products[randomIndex];
        
        // Instantiate(randomProduct, vector3, randomProduct.transform.rotation);
    }
    void ProCreate1()
    {
        Instantiate(Product1, vector3, Product1.transform.rotation);
    }
    void ProCreate2()
    {
        Instantiate(Product2, vector3, Product2.transform.rotation);
    }
    void ProCreate3()
    {
        Instantiate(Product3, vector3, Product3.transform.rotation);
    }


}
