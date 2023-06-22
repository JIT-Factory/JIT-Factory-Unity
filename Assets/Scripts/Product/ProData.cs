using UnityEngine;

[CreateAssetMenu(fileName = "NewProData", menuName = "Create ProData")]
public class ProData : ScriptableObject
{
    public string factoryName = "CarFactory";
    public string productName = "XC40";
    public string status = "success";
    public int sales = 62000000;
    public string reason = "-";
}
