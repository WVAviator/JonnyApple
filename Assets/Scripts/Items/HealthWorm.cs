using UnityEngine;
using System.Collections;

public class HealthWorm : SaveableItem {

    public float healthBoost = 10;
    public AudioSource chewSound;
    public bool collected;
    public int collectPoints = 36;

    protected override void GetItem()
    {
        GameManagement.score += collectPoints;
        if(player.GetComponent<Health>() != null) player.GetComponent<Health>().addHealth(healthBoost);
        SoundUtility.PlaySound(chewSound);       
    }

}
