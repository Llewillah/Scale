using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIButton[] uiButtons;
    public BuildButton[] buildButtons;
    public GameObject displayBox;
    public TMP_Text displayText;

    GridManager gM;
    public void SetUp(GridManager gM)
    {
        this.gM = gM;
        displayBox.SetActive(false);

        for (int i = 0; i < uiButtons.Length; i++) 
        {
            uiButtons[i].SetUp(this, i);
        }

        foreach (BuildButton b in buildButtons)
        {
            b.SetUp(gM);
            b.gameObject.SetActive(false);
        }
    }

    //====================================================

    //Basic UI

    void SetBaseUI() 
    {
        foreach (UIButton b in uiButtons) 
        {
            b.gameObject.SetActive(true);
        }
    }

    public void DoMenuButton(int index) 
    {
        switch (index) 
        {
            case 0:
                SetBuildMenu(!buildButtons[0].gameObject.activeSelf);
                break;
            case 1:
                break;
        }
    }

    public void HideUI() 
    {
        SetBuildMenu(false);
        foreach (UIButton b in uiButtons)
        {
            b.gameObject.SetActive(false);
        }
    }

    public void UnhideUI() 
    {
        SetBaseUI();
        SetBuildMenu(true);
    }

    //====================================================

    // BUILDING BUTTONS

    public void SetFactoryButtons(Factory[] factories)
    {
        ResetBuildButtons();
        for (int i = 0; i < buildButtons.Length; i++)
        {
            if (i < factories.Length)
            {
                buildButtons[i].SetButton(i);
                buildButtons[i].gameObject.SetActive(true);
                buildButtons[i].text.text = factories[i].name;
            }
        }
    }

    public void SetCollectorButtons(Collector[] collectors)
    {
        ResetBuildButtons();
        for (int i = 0; i < buildButtons.Length; i++)
        {
            if (i < collectors.Length)
            {
                buildButtons[i].SetButton(i);
                buildButtons[i].gameObject.SetActive(true);
                buildButtons[i].text.text = collectors[i].name;
            }   
        }
    }

    public void SetBuildMenu(bool active)
    {
        if (active) 
        {
            gM.BackButton();
        }

        ResetBuildButtons();
        
        displayBox.SetActive(active);
        buildButtons[0].SetButton(0);
        buildButtons[0].gameObject.SetActive(active);
        buildButtons[0].text.text = "Collectors";

        buildButtons[1].SetButton(1);
        buildButtons[1].gameObject.SetActive(active);
        buildButtons[1].text.text = "Factories";

    }

    void ResetBuildButtons()
    {
        foreach (BuildButton b in buildButtons)
        {
            b.gameObject.SetActive(false);
            b.text.text = "Shouldnt see this";
        }
    }

    //========================================

    // Display Stuff

    public void DisplayCollector(CollectorBuilding col) 
    {
        SetBuildMenu(false);

        displayBox.SetActive(true);
        displayText.gameObject.SetActive(true);
        displayText.text = "";

        displayText.text += col.colScriptable.name;


    }

    public void DisplayFactory(FactoryBuilding fac) 
    {
        SetBuildMenu(false);

        displayBox.SetActive(true);
        displayText.gameObject.SetActive(true);
        displayText.text = "";
    }
}
