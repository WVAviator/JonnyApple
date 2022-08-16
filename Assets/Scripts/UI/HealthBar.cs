using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    GameObject player;
    float health;
    float healthBarWidth;
    float ratio;
    RectTransform healthBar;
    Health playerHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();

        healthBarWidth = 170;
        //Debug.Log("Width: " + healthBarWidth);
        ratio = healthBarWidth / playerHealth.maxHealth;
        //Debug.Log("Ratio: " + ratio);
    }

	void Update () {

        health = playerHealth.health;
        //Debug.Log("Health: " + health);

        transform.localPosition = new Vector2(health * ratio - healthBarWidth, transform.localPosition.y);
        //Debug.Log("Position X: " + transform.localPosition.x);


	}
}
