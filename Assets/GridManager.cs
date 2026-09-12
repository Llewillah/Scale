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

    bool buildingRoad = false;

    BuildState state = BuildState.none;

    int curSelected;

    List<Vector2> curRoads = new List<Vector2>();

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


        if (prevFrameTempPos != grid.GetWorldGridTest(tempBuildIcon.transform.position) && buildingRoad) 
        {
            prevFrameTempPos = grid.GetWorldGridTest(tempBuildIcon.transform.position);
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
                    grid.SetBuilding(tempBuildIcon.transform.position);
                    CancelBuild();
                    break;
                case BuildState.collector:
                    bM.AddCollector(collectors[curSelected], tempBuildIcon.transform.position);
                    grid.SetBuilding(tempBuildIcon.transform.position);
                    CancelBuild();
                    break;
                case BuildState.road:
                    if (!buildingRoad)
                    {
                        Debug.Log("Set Start Pos");
                        startRoadPos = tempBuildIcon.transform.position;
                        buildingRoad = true;
                    }
                    else
                    {
                        Debug.Log("end the roads");
                        grid.SetRoad(curRoads);
                        CancelBuild();
                    }
                    break;
            }            
        }
    }


    public void CancelBuild() 
    {
        buildingRoad = false;
        tempBuildIcon.SetActive(false);
        state = BuildState.none;
        uiM.UnhideUI();
    }

    public void SetRoads(Vector2 startPos, Vector2 endPos) 
    {
        grid.ResetTileColour();
        curRoads = new List<Vector2>();

        grid.GetGridPos(startPos, out int startX, out int startY);
        grid.GetGridPos(endPos, out int endX, out int endY);

        int difX = WorkingSign(endX - startX);
        int difY = WorkingSign(endY - startY);

        while (startX != endX + difX) 
        {
            curRoads.Add(new Vector2(startX, startY));
            startX += difX;
        }

        startX -= difX;

        while (startY != endY + difY) 
        {
            curRoads.Add(new Vector2(startX, startY));
            startY += difY;
        }


        if (curRoads.Count == 0) 
        {
            curRoads.Add(new Vector2(startX, startY));
        }

        grid.CheckRoad(curRoads);
    }

    public void BackButton() 
    { 
        state = BuildState.none;
    }

    int WorkingSign(int num) 
    {
        if (num > 0)
        {
            return 1;
        }
        else if (num < 0) 
        {
            return -1;
        }

        return 0;
    }
}
