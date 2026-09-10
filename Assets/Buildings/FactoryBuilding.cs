using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FactoryBuilding : MonoBehaviour
{
    public Factory facScriptable;
    float timer = 0;

    //Resource, amount stored
    public Dictionary<Resource, int> storage = new Dictionary<Resource, int>();

    public bool display = false;
    bool active = false;

    public void SetUp(Factory fac)
    {
        facScriptable = fac;
        GetComponent<SpriteRenderer>().sprite = fac.sprite;


        foreach (Resource r in facScriptable.inputs)
        {
            storage.Add(r, 0);
        }

        foreach (Resource r in facScriptable.outputs)
        {
            storage.Add(r, 0);
        }
    }

    public void DoUpdate()
    {
        if (active)
        {
            if (timer == 0) 
            {
                StartProd();
            }

            timer += Time.deltaTime;

            if (timer >= facScriptable.produceTime)
            {
                CollectProduce();
                timer = 0;
            }
        }
        else 
        {
            active = true;
            for (int i = 0; i < facScriptable.inputs.Length; i++)
            {
                if (storage[facScriptable.inputs[i]] < facScriptable.inputAmounts[i]) 
                {
                    active = false;
                }
            }
        }
    }

    void StartProd() 
    {
        for (int i = 0; i < facScriptable.inputs.Length; i++)
        {
            storage[facScriptable.inputs[i]] -= facScriptable.inputAmounts[i];
        }
    }

    void CollectProduce()
    {
        for (int i = 0; i < facScriptable.outputs.Length; i++)
        {
            if (storage[facScriptable.outputs[i]] + facScriptable.outputAmounts[i] <= facScriptable.maxResource)
            {
                storage[facScriptable.outputs[i]] += facScriptable.outputAmounts[i];
            }
        }
    }

    public void OnClick()
    {
        display = true;
    }
}
