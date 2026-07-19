using UnityEngine;

[CreateAssetMenu(fileName = "Factory", menuName = "Scriptable Objects/Factory")]
public class Factory : ScriptableObject
{
    public Sprite sprite;
    public int buildCost;
    public int maxResource;
    public float produceTime;

    public Resource[] inputs;
    public int[] inputAmounts;

    public Resource[] outputs;
    public int[] outputAmounts;
}
