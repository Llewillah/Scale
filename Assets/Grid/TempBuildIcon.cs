using UnityEngine;

public class TempBuildIcon : MonoBehaviour, IClickable
{
    GridManager gM;
    public void SetUp(GridManager gm)
    {
        gM = gm;
    }

    public void OnClick() 
    {
        gM.Build();
    }

    public void OnRightClick() 
    {
        gM.CancelBuild();
    }
}
