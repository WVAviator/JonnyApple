using UnityEngine;
using System.Collections;

public class ExtraLife : SaveableItem {

    public AudioSource sound;

    protected override void GetItem()
    {
        GameManagement.AddLife();
        SoundUtility.PlaySound(sound);
    }

    protected override void DeactivateParticles()
    {
        transform.FindChild("Grouper").FindChild("Particle System").gameObject.SetActive(false);
    }
}
