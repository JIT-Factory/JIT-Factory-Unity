using UnityEngine;

[CreateAssetMenu(fileName = "NewWineBottleData", menuName = "Create ProData")]
public class WineBottleData : ScriptableObject
{
    public string factoryName = "PackagingFactory";
    public string productName = "WineBottle";
    public string status = "success";
    public int sales = 50000;
    public string reason = "-";
}

