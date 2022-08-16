using UnityEngine;
using System.Collections;

public class AIKilled : MonoBehaviour {

    Health healthComponent;

    public GameObject deadBody;
    public float deadBodyLifetime = 5;

    public GameObject[] drops;
    public int maxDrops = 2;
	public int dropsLifetime = 30;

    void Start()
    {
        healthComponent = GetComponent<Health>();
    }

    void Update()
    {
        if (healthComponent.health > 0) return;

        Vector2 pos = transform.position;
        Quaternion rot = transform.rotation;
        
        gameObject.SetActive(false);
        GameObject dead = (GameObject)Instantiate((GameObject)deadBody, pos, rot);
        Destroy(dead, deadBodyLifetime);

        int randomNum = Random.Range(0, maxDrops);
        for (int i = 0; i < randomNum; i++)
        {
            foreach (GameObject drop in drops)
            {
                GameObject droppedItem = (GameObject)Instantiate((GameObject)drop, pos, rot);
				Destroy (droppedItem, dropsLifetime);
            }
        }

        Destroy(gameObject, (deadBodyLifetime + 1));
        
    }

}
