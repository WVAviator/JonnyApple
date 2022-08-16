using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    int score;
    Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
    }
	
	void Update ()
    {
        score = GameManagement.score;
        scoreText.text = score.ToString("D8");
    }

    public void AddScore(int addedScore)
    {
        GameManagement.score += addedScore;
    }
}
