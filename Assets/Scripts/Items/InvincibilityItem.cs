using UnityEngine;
using System.Collections;

public class InvincibilityItem : SaveableItem {

    protected override void GetItem()
    {
        player.GetComponent<Invincibility>().ActivateInvincibility();
    }

    protected override void DeactivateParticles()
    {
        transform.FindChild("Particle System").gameObject.SetActive(false);
    }

}
