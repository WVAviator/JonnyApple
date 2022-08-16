using UnityEngine;

using System.Collections;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour {

	int currentLives;
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        currentLives = GameManagement.lives;
        text.text = "x " + Mathf.Clamp(currentLives, 0, 99);
    }

}
