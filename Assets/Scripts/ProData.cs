using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProData : MonoBehaviour
{
    public string productName = "ss";
    public string status = "trrt";
    public int sales = 1;
    public string reason = "123";

    // 제품 정보를 반환하는 메소드
    public ProductData GetProductData()
    {
        return new ProductData(productName, status, sales, reason);
        Debug.Log("보냐짐");
    }
}