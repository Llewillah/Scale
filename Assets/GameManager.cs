using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GridManager gM;
    public SpawningManager sM;
    public BuildingsManager bM;
    public UIManager uiM;

    private void Start()
    {
        uiM.SetUp(gM);
        gM.SetUp(sM, bM, uiM);
        bM.SetUp(sM, uiM);
    }

    private void Update()
    {
        gM.DoUpdate();
        bM.DoUpdate();
        uiM.DoUpdate();
    }
}
