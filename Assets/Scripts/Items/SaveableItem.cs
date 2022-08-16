using UnityEngine;
using System.Collections;

public class SaveableItem : MonoBehaviour {

    public bool isCollected;
    protected GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (GameManagement.WasCollected(gameObject)) ShowCollected();
    }

    public void ShowCollected()
    {
        isCollected = true;
        transform.localScale = Vector3.zero;
        DeactivateParticles();
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.gameObject.Equals(player)) return;
        GetItem();
        ShowCollected();
    }

    protected virtual void DeactivateParticles()
    {

    }

    protected virtual void GetItem()
    {

    }

}
