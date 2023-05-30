using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProData : MonoBehaviour
{
    public string factoryName = "CarFactory";
    public string productName = "ProductA";
    public string status = "success";
    public int sales = 10056;
    public string reason = "-";

    // ��ǰ ������ ��ȯ�ϴ� �޼ҵ�
    public ProductData GetProductData()
    {
        return new ProductData(productName, status, sales, reason);
    }
}