using UnityEngine;

[CreateAssetMenu(fileName = "NewProData", menuName = "Create ProData")]
public class ProData : ScriptableObject
{
    public string factoryName = "CarFactory";
    public string productName = "ProductA";
    public string status = "success";
    public int sales = 99999;
    public string reason = "-";
}
