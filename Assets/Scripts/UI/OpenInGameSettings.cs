using UnityEngine;
using System.Collections;

public class OpenInGameSettings : MonoBehaviour {

    RectTransform rt;
    public GameObject settingsMenu;

    public void ToggleSettingsMenu()
    {
        //if (settingsMenu.activeSelf) CloseMenu();
        //if (!settingsMenu.activeSelf) OpenMenu();
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void CloseMenu()
    {
        settingsMenu.SetActive(false);
        Debug.Log("Close menu");       
    }

    public void OpenMenu()
    {
        settingsMenu.SetActive(true);       
    }

}
