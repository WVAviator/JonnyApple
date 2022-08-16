using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour {

    public AudioSource sound;
    public bool playing = false;
    public float pitchVariance = 0.1f;

    public void Update()
    {
        if (playing)
        {
            SoundUtility.PlaySound(sound, pitchVariance);

            playing = false;
        }
        
    }
}
