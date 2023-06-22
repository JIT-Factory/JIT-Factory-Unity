using UnityEngine;

[CreateAssetMenu(fileName = "NewWineJugData", menuName = "Create ProData")]
public class WineJugData : ScriptableObject
{
    public string factoryName = "PackagingFactory";
    public string productName = "WineJug";
    public string status = "success";
    public int sales = 23000;
    public string reason = "-";
}


