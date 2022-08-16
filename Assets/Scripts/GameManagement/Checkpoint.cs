using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public int checkpointIndex;
    int scoreAtCheckpoint;
    Animator anim;
    public AudioSource sound;
    GameObject player;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.Equals(player))
        {
            anim.SetTrigger("getCheckpoint");
            if(GameManagement.activeCheckpoint < checkpointIndex) SoundUtility.PlaySound(sound);
            GameManagement.UpdateCheckpoint(checkpointIndex);
            GameManagement.SaveGame();
        }
    }

}
