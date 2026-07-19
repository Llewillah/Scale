using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum BuildState
{
    factory, collector, road, none
}

public class GridManager : MonoBehaviour
{
    public int width, height;
    public float cellSize;
    public Vector2 gridStartPos;

    Grid grid;

    
    public Collector[] collectors;
    public Factory[] factories;

    public GameObject tempBuildIcon;

    BuildState state = BuildState.none;

    int curSelected;

    BuildingsManager bM;
    UIManager uiM;
    public void SetUp(SpawningManager sM, BuildingsManager bM, UIManager ui)
    {
        this.bM = bM;
        uiM = ui;
        grid = new Grid(width, height, cellSize, gridStartPos, sM);
        tempBuildIcon.SetActive(false);
        tempBuildIcon.GetComponent<TempBuildIcon>().SetUp(this);
    }

    public void DoUpdate()
    {
        grid.DrawGrid();

        if (tempBuildIcon.activeSelf)
        {
            tempBuildIcon.transform.position = grid.GetWorldGridTest(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        }
    }

    public void SelectBuilding(int index)
    {
        curSelected = index;
        tempBuildIcon.SetActive(true);

        switch (state)
        {
            case BuildState.none:
                tempBuildIcon.SetActive(false);
                if (index == 0)
                {
                    uiM.SetCollectorButtons(collectors);
                    state = BuildState.collector;
                }
                else if (index == 1) 
                {
                    uiM.SetFactoryButtons(factories);
                    state = BuildState.factory;
                }
                break;
            case BuildState.factory:
                tempBuildIcon.GetComponent<SpriteRenderer>().sprite = factories[index].sprite;
                uiM.HideUI();
                break;
            case BuildState.collector:
                tempBuildIcon.GetComponent<SpriteRenderer>().sprite = collectors[index].sprite;
                uiM.HideUI();
                break;
            case BuildState.road:
                break;
        }
    }

    public void Build()
    {
        if (grid.CheckEmpty(tempBuildIcon.transform.position)) 
        {
            switch (state)
            {
                case BuildState.factory:
                    bM.AddFactory(factories[curSelected], tempBuildIcon.transform.position);
                    break;
                case BuildState.collector:
                    bM.AddCollector(collectors[curSelected], tempBuildIcon.transform.position);
                    break;
                case BuildState.road:
                    break;
            }

            state = BuildState.none;
            tempBuildIcon.SetActive(false);
            grid.SetBuilding(tempBuildIcon.transform.position);
            uiM.UnhideUI();
        }
    }

    public void BackButton() 
    {
        state = BuildState.none;
    }
}
