using UnityEngine;
using System.Collections;

public class AISound : MonoBehaviour {

    public AudioSource[] randomSounds;
    public AudioSource pain;
    public int randomSoundFactor = 500;
    float health;
    public float pitchVar = 0.1f;

    Health healthC;

    void Start()
    {
        healthC = gameObject.GetComponent<Health>();
    }

    void Update()
    {        
        if (health > healthC.health) PlayPainSound();
        health = healthC.health;

        if (Random.Range(0, randomSoundFactor) == 1) PlayRandomAISound();
    }

    void PlayRandomAISound()
    {
        int randomNum = Random.Range(0, randomSounds.Length);
        AudioSource soundToPlay = randomSounds[randomNum];

        SoundUtility.PlaySound(soundToPlay, pitchVar, true);
    }

    void PlayPainSound()
    {
        SoundUtility.PlaySound(pain, pitchVar, true);
    }
}
