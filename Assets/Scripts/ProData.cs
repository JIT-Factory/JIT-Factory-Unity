using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProData : MonoBehaviour
{
    public string productName;
    public string status;
    public int sales;
    public string reason;

    // ��ǰ ������ ��ȯ�ϴ� �޼ҵ�
    public ProductData GetProductData()
    {
        return new ProductData(productName, status, sales, reason);
    }
}