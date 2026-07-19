using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GridManager gM;
    public SpawningManager sM;

    private void Start()
    {
        gM.SetUp(sM);
    }

    private void Update()
    {
        gM.DoUpdate();
    }
}
