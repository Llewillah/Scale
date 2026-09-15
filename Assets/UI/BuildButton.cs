using TMPro;
using UnityEngine;


public class BuildButton : MonoBehaviour, IClickable
{
    GridManager gM;
    public TMP_Text text;
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
        gM.SelectBuilding(index);
    }

    public void OnRightClick() { }
}
