using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectorBuilding : MonoBehaviour, IClickable
{
    Collector colScriptable;
    float timer = 0;

    //Resource, amount stored
    Dictionary<Resource, int> storage = new Dictionary<Resource, int>();

    public bool display = false;
    bool active = true;

    public void SetUp(Collector col) 
    {
        colScriptable = col;
        GetComponent<SpriteRenderer>().sprite = col.sprite;

        foreach (Resource r in colScriptable.outputs) 
        { 
            storage.Add(r, 0);
        }
    }

    public void DoUpdate() 
    {
        if (active)
        {
            timer += Time.deltaTime;

            if (timer >= colScriptable.collectTime)
            {
                CollectResources();
                timer = 0;
            }
        }
    }

    void CollectResources() 
    {
        for (int i = 0; i < colScriptable.outputs.Length; i++) 
        {
            if (storage[colScriptable.outputs[i]] + colScriptable.outputAmounts[i] <= colScriptable.maxResource)
            {
                storage[colScriptable.outputs[i]] += colScriptable.outputAmounts[i];
            }
        }
    }

    public void OnClick() 
    {
        display = true;
    }
}
