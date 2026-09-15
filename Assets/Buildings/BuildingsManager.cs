using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BuildingsManager : MonoBehaviour
{
    SpawningManager sM;
    UIManager uiM;

    List<CollectorBuilding> collectors = new List<CollectorBuilding>();
    List<FactoryBuilding> factories = new List<FactoryBuilding>();

    int prevColFrame;
    int prevFactoryFrame;

    GameObject colsObj, facObj;

    public void SetUp(SpawningManager sM, UIManager uiM)
    {
        this.sM = sM;
        this.uiM = uiM;

        colsObj = new GameObject();
        colsObj.transform.position = new Vector3 (0, 0, -1);
        facObj = new GameObject();
        facObj.transform.position = new Vector3(0, 0, -1);

        colsObj.name = "Collectors";
        facObj.name = "Factories";
    }
    public void DoUpdate()
    {
        for (int i = 0; i < prevColFrame; i++)
        {
            collectors[i].DoUpdate();

            if (collectors[i].display)
            {
                uiM.DisplayCollector(collectors[i]);
                collectors[i].display = false;
            }
        }

        if (prevColFrame != collectors.Count)
        {
            prevColFrame = collectors.Count;
        }

        for (int i = 0; i < prevFactoryFrame; i++)
        {
            factories[i].DoUpdate();
            if (factories[i].display)
            {
                uiM.DisplayFactory(factories[i]);
                factories[i].display = false;
            }
        }

        if (prevFactoryFrame != factories.Count)
        {
            prevFactoryFrame = factories.Count;
        }
    }
    public void AddCollector(Collector c, Vector2 pos)
    {
        CollectorBuilding col = sM.SpawnCollector(pos, colsObj.transform);
        col.SetUp(c);
        collectors.Add(col);
    }
    public void RemoveCollector() { }
    public void AddFactory(Factory f, Vector2 pos)
    {
        FactoryBuilding fac = sM.SpawnFactory(pos, facObj.transform);
        fac.SetUp(f);
        factories.Add(fac);
    }
    public void RemoveFactory() { }


}
