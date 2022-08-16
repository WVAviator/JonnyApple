using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SaveAndQuit : MonoBehaviour {

	public void SQ()
    {
        GameManagement.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }
}
