using UnityEngine;

[CreateAssetMenu(fileName = "NewPotionBigData", menuName = "Create ProData")]
public class PotionBigData : ScriptableObject
{
    public string factoryName = "PackagingFactory";
    public string productName = "CeramicCup";
    public string status = "success";
    public int sales = 12000;
    public string reason = "-";
}


