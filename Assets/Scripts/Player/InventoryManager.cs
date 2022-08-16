using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public bool[] allAmmo;

    public GameObject selectedPanel;
    public int indexOfSelected = 0;

    public int appleAmmo;
    public GameObject applePanel;
    public GameObject appleProjectile;

    public int orangeAmmo;
    public GameObject orangePanel;
    public GameObject orangeProjectile;

    public int cornAmmo;
    public GameObject cornPanel;
    public GameObject cornProjectile;

    public int cantaloupeAmmo;
    public GameObject cantaloupePanel;
    public GameObject cantaloupeProjectile;

	public int strawberryAmmo;
	public GameObject strawberryPanel;
	public GameObject strawberryProjectile;

    void Start()
    {
        allAmmo = new bool[5];

        allAmmo[0] = (appleAmmo > 0);
        allAmmo[1] = (orangeAmmo > 0);
        allAmmo[2] = (cornAmmo > 0);
        allAmmo[3] = (cantaloupeAmmo > 0);
		allAmmo[4] = (strawberryAmmo > 0);

        PullFromGameManager();
    }

	void Update ()
    {

        PushToGameManager();

        applePanel.SetActive(appleAmmo > 0);
        allAmmo[0] = (applePanel.activeSelf);

        orangePanel.SetActive(orangeAmmo > 0);
        allAmmo[1] = (orangePanel.activeSelf);

        cornPanel.SetActive(cornAmmo > 0);
        allAmmo[2] = (cornPanel.activeSelf);

        cantaloupePanel.SetActive(cantaloupeAmmo > 0);
        allAmmo[3] = (cantaloupePanel.activeSelf);

		strawberryPanel.SetActive(strawberryAmmo > 0);
		allAmmo[4] = (strawberryPanel.activeSelf);

        PositionPanel(applePanel, 0);
        PositionPanel(orangePanel, 1);
        PositionPanel(cornPanel, 2);
        PositionPanel(cantaloupePanel, 3);
		PositionPanel (strawberryPanel, 4);

        ResizePanel(GetComponent<RectTransform>());

        IdentifySelection();

    }

    void PushToGameManager()
    {
        GameManagement.appleAmmo = appleAmmo;
        GameManagement.orangeAmmo = orangeAmmo;
        GameManagement.cornAmmo = cornAmmo;
        GameManagement.cantaloupeAmmo = cantaloupeAmmo;
        GameManagement.strawberryAmmo = strawberryAmmo;
    }

    void PullFromGameManager()
    {
        appleAmmo = GameManagement.appleAmmo;
        orangeAmmo = GameManagement.orangeAmmo;
        cornAmmo = GameManagement.cornAmmo;
        cantaloupeAmmo = GameManagement.cantaloupeAmmo;
        strawberryAmmo = GameManagement.strawberryAmmo;
    }

    void PositionPanel(GameObject panel, int index)
    {
        RectTransform panelAbove = GetActivePanelAbove(panel.GetComponent<RectTransform>(), index);
        if (panelAbove == null)
        {
            panel.GetComponent<RectTransform>().localPosition = new Vector2(0, -5);
            return;
        }
        panel.GetComponent<RectTransform>().localPosition = new Vector2(0, panelAbove.localPosition.y - 80);

    }

    void IdentifySelection()
    {
        selectedPanel.SetActive(GetPanelAtIndex(indexOfSelected).activeSelf);
        if (!selectedPanel.activeSelf)
        {
            for (int i = 0; i < allAmmo.Length; i++)
            {
                if (allAmmo[i])
                {
                    SetActiveSelection(GetPanelAtIndex(i));
                    break;
                }
            }
        }

        if (selectedPanel.activeSelf)
        {
            selectedPanel.GetComponent<RectTransform>().localPosition = 
                new Vector2(GetPanelAtIndex(indexOfSelected).GetComponent<RectTransform>().localPosition.x,
                            GetPanelAtIndex(indexOfSelected).GetComponent<RectTransform>().localPosition.y + 3);
        }
        
    }

    public void SetActiveSelection(GameObject panelSelected)
    {
        indexOfSelected = GetIndexOfObject(panelSelected);
    }

    public GameObject GetProjectile()
    {
        if (indexOfSelected == 0) return appleProjectile;
        if (indexOfSelected == 1) return orangeProjectile;
        if (indexOfSelected == 2) return cornProjectile;
        if (indexOfSelected == 3) return cantaloupeProjectile;
		if (indexOfSelected == 4) return strawberryProjectile;
        return null;
    }

    public void DecrementActiveAmmo()
    {
        if (indexOfSelected == 0) appleAmmo--;
        if (indexOfSelected == 1) orangeAmmo--;
        if (indexOfSelected == 2) cornAmmo--;
        if (indexOfSelected == 3) cantaloupeAmmo--;
		if (indexOfSelected == 4) strawberryAmmo--;
    }

    public int GetAmmoCount(GameObject panel)
    {
        if (panel.Equals(applePanel)) return appleAmmo;
        if (panel.Equals(orangePanel)) return orangeAmmo;
        if (panel.Equals(cornPanel)) return cornAmmo;
        if (panel.Equals(cantaloupePanel)) return cantaloupeAmmo;
		if (panel.Equals(strawberryPanel)) return strawberryAmmo;
        return 0;
    }

    public void AddAmmo(int index, int count)
    {
        if (index == 0) appleAmmo += count;
        if (index == 1) orangeAmmo += count;
        if (index == 2) cornAmmo += count;
        if (index == 3) cantaloupeAmmo += count;
		if (index == 4) strawberryAmmo += count;
    }

    RectTransform GetActivePanelAbove(RectTransform panel, int index)
    {
        for (int i = (index - 1); (i > -1) ; i--)
        {
            if (allAmmo[i])
            {
                return GetPanelAtIndex(i).GetComponent<RectTransform>();
            }

        }

        return null;
    }

    void ResizePanel(RectTransform rt)
    {
        int panelSize = 0;
        foreach (bool value in allAmmo)
        {
            if (value) panelSize++;
        }
        rt.sizeDelta = new Vector2(90, (80 * panelSize) + 5);
    }

    GameObject GetPanelAtIndex(int index)
    {
        if (index == 0) return applePanel;
        if (index == 1) return orangePanel;
        if (index == 2) return cornPanel;
        if (index == 3) return cantaloupePanel;
		if (index == 4) return strawberryPanel;
        return null;
    }

    int GetIndexOfObject(GameObject panelObject)
    {
        if (panelObject.Equals(applePanel)) return 0;
        if (panelObject.Equals(orangePanel)) return 1;
        if (panelObject.Equals(cornPanel)) return 2;
        if (panelObject.Equals(cantaloupePanel)) return 3;
		if (panelObject.Equals(strawberryPanel)) return 4;
        return 0;
    }


}
