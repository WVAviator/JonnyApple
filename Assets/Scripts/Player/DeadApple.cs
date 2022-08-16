using UnityEngine;
using System.Collections;

public class DeadApple : MonoBehaviour {

    public GameObject player;
    public GameObject deadApple;
    public Transform spawnPosition;
    Vector2 playerScale;
    public bool hasDied;
    Health playerHealth;

    void Start()
    {
        playerHealth = player.GetComponent<Health>();
    }

    void Update () {

        float health = playerHealth.health;
        if (health <= 0 && !hasDied)
        {
            hasDied = true;
            GameManagement.DeductLife(); 
            GameObject clone = (GameObject)Instantiate((GameObject)deadApple, player.transform.position, player.transform.rotation);
            player.SetActive(false);
            Invoke("Respawn", 5);

            Destroy(clone, 5);
        }
	}

    void Respawn()
    {
        player.SetActive(true);
        player.transform.parent = null;
        player.transform.position = GameManagement.GetActiveCheckpointRespawn().position;
        //player.transform.localScale = playerScale;
        player.GetComponent<Health>().Restore();
        hasDied = false;
        player.GetComponent<MyPlayerController>().isFallingToDeath = false;
    }
}
