using UnityEngine;


public class BuildButton : MonoBehaviour, IClickable
{
    GridManager gM;
    int index;

    public void SetUp(GridManager gm) 
    {
        gM = gm;  
    }

    public void SetButton(int index) 
    { 
        this.index = index;
    }

    public void OnClick() 
    {
        gM.Build(index);
    }
}
