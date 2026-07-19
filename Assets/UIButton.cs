using UnityEngine;

public class UIButton : MonoBehaviour, IClickable
{
    int index;
    UIManager uiM;
    public void SetUp(UIManager uiM, int index) 
    { 
        this.uiM = uiM;
        this.index = index;
    }

    public void OnClick() 
    {
        uiM.DoMenuButton(index);
    }
}
