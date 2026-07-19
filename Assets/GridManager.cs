using UnityEngine;

public enum BuildState
{
    factory, collector, road
}

public class GridManager : MonoBehaviour
{
    

    public int width, height;
    public float cellSize;
    public Vector2 gridStartPos;

    Grid grid;

    public BuildButton[] buttons;
    public Collector[] collectors;
    public Factory[] factories;

    public GameObject tempBuildIcon;

    BuildState state;

    public void SetUp(SpawningManager sM)
    {
        grid = new Grid(width, height, cellSize, gridStartPos, sM);
        tempBuildIcon.SetActive(false);

        foreach (BuildButton b in buttons)
        {
            b.SetUp(this);
            b.gameObject.SetActive(false);
        }
    }

    public void DoUpdate()
    {
        grid.DrawGrid();
    }

    public void SetFactoryButtons()
    {
        state = BuildState.factory;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < factories.Length)
            {
                buttons[i].SetButton(i);
                buttons[i].gameObject.SetActive(true);
            }
        }
    }

    public void SetCollectorButtons()
    {
        state = BuildState.collector;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < collectors.Length)
            {
                buttons[i].SetButton(i);
                buttons[i].gameObject.SetActive(true);
            }
        }
    }

    public void Build(int index)
    {
        switch (state)
        {
            case BuildState.factory:
                break;
            case BuildState.collector:
                break;
            case BuildState.road:
                break;
        }
    }
}
