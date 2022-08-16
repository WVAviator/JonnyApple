using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {


	void Start ()
    {
        Invoke("ReturnMainMenu", 5);
	}

    void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
