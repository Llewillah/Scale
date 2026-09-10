using System.Collections.Generic;
using System.IO;
using UnityEditor;
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
    Vector2 prevFrameTempPos, startRoadPos;

    BuildState state = BuildState.none;

    int curSelected;

    List<Vector2Int> curRoads = new List<Vector2Int>();

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

        if (state == BuildState.road && prevFrameTempPos != (Vector2)tempBuildIcon.transform.position) 
        {
            SetRoads(startRoadPos, tempBuildIcon.transform.position);
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
                else if (index == 5) 
                {
                    state = BuildState.road;
                    tempBuildIcon.SetActive(true);
                    tempBuildIcon.GetComponent<SpriteRenderer>().sprite = factories[0].sprite;
                    uiM.HideUI();
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

            if (state == BuildState.road) 
            {
                grid.SetRoad(curRoads);
            }
            else
            {  
                grid.SetBuilding(tempBuildIcon.transform.position);
            }

            tempBuildIcon.SetActive(false);
            state = BuildState.none;
            uiM.UnhideUI();
        }
    }

    public void CancelBuild() 
    {
        state = BuildState.none;
        tempBuildIcon.SetActive(false);
        uiM.UnhideUI();
    }

    public void SetRoads(Vector2 startPos, Vector2 endPos) 
    {
        int startX = Mathf.Min((int)startPos.x, (int)endPos.x);
        int startY = Mathf.Min((int)startPos.y, (int)endPos.y);

        int endX = Mathf.Max((int)startPos.x, (int)endPos.x);
        int endY = Mathf.Max((int)startPos.y, (int)endPos.y);

        for (int x = startX; x < endX; x++) 
        {
            for (int y = startY; y < endY; y++)
            {
                if (!grid.CheckEmpty(new Vector2(x, y))) 
                { 
                    //make it so cant place roads when this is false
                }
                curRoads.Add(new Vector2Int(x, y));

            }
        }
    }

    public void BackButton() 
    {
        state = BuildState.none;
    }
}
