using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartNewGame : MonoBehaviour {

    public GameObject loadingScreen;
    public GameObject overwriteSavePrompt;

	public void NewGame()
    {
        loadingScreen.SetActive(true);
        overwriteSavePrompt.SetActive(GameManagement.SaveExists());
        if (!GameManagement.SaveExists())
        {
            GameManagement.NewGame();
            GameManagement.StartGame();
        }
        
    }
}
