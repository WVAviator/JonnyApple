using UnityEngine;
using System.Collections;

public class Continue : MonoBehaviour {

    public GameObject loadingScreen;

    public void ContinueGame()
    {
        if (!GameManagement.SaveExists()) return;

        loadingScreen.SetActive(true);
        GameManagement.LoadGame();
        GameManagement.StartGame();
    }
}
