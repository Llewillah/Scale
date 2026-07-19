using UnityEngine;

[CreateAssetMenu(fileName = "Collector", menuName = "Scriptable Objects/Collector")]
public class Collector : ScriptableObject
{
    public Sprite sprite;
    public int buildCost;
    public int maxResource;
    public float collectTime;

    public Resource[] outputs;
    public int[] outputAmounts;
}
