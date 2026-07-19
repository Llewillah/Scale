using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    public GameObject tile, collector, factory;

    public Tile SpawnTile(Vector2 pos, Transform parent) 
    {
        GameObject obj = Instantiate(tile, pos, Quaternion.identity, parent);
        return obj.GetComponent<Tile>();
    }

    public CollectorBuilding SpawnCollector(Vector2 pos, Transform parent) 
    {
        GameObject obj = Instantiate(collector, pos, Quaternion.identity, parent);
        return obj.GetComponent<CollectorBuilding>();
    }

    public FactoryBuilding SpawnFactory(Vector2 pos, Transform parent)
    {
        GameObject obj = Instantiate(factory, pos, Quaternion.identity, parent);
        return obj.GetComponent<FactoryBuilding>();
    }
}
