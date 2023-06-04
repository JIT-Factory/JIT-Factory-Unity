using UnityEngine;

[CreateAssetMenu(fileName = "NewBeerData", menuName = "Create ProData")]
public class BeerData : ScriptableObject
{
    public string factoryName = "PackagingFactory";
    public string productName = "Beer";
    public string status = "success";
    public int sales = 12000;
    public string reason = "-";
}

