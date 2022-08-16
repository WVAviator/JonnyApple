using UnityEngine;
using System.Collections;

public class Popcorn : MonoBehaviour {

    public AudioSource popSound;

	void Start()
    {
        SoundUtility.PlaySound(popSound, 0.1f, true);

        GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-800, 800);
    }
}
