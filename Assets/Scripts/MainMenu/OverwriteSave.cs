using UnityEngine;
using System.Collections;

public class OverwriteSave : MonoBehaviour {

    public bool yesOverwrite;
    public GameObject prompt;
    public GameObject loadScreen;

	public void ButtonPressed()
    {
        if (!yesOverwrite)
        {
            loadScreen.SetActive(false);
            prompt.SetActive(false);
            return;
        }
        if (yesOverwrite)
        {            
            GameManagement.NewGame();
            GameManagement.StartGame();
            gameObject.SetActive(false);
        }
    }
}
