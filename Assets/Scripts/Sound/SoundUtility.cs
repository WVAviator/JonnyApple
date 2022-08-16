using UnityEngine;
using System.Collections;

public class SoundUtility {

    public static float musicVolume = 1;
    public static float soundVolume = 1;
    public static AudioSource activeMusic;
    public static float musicBaseVolume;

    public static void UpdateVolume(float newValue, bool isMusic)
    {
        if (isMusic)
        {
            musicVolume = newValue;
            activeMusic.volume = musicVolume * musicBaseVolume;
        }
        if (!isMusic) soundVolume = newValue;
    }

    public static void PlaySound(AudioSource sound)
    {
        PlaySound(sound, 0, false, soundVolume);
    }

    public static void PlaySound(AudioSource sound, float pitchVariance)
    {
        PlaySound(sound, pitchVariance, false, soundVolume);
    }

    public static void PlaySound(AudioSource sound, float pitchVariance, bool playAtDistance)
    {
        PlaySound(sound, pitchVariance, playAtDistance, soundVolume);

    }

    static void PlaySound(AudioSource sound, float pitchVariance, bool playAtDistance, float volumeModifier)
    {

        sound.pitch = 1 + Random.Range(-pitchVariance, pitchVariance);
        float distance = 1;
        if (playAtDistance) distance = Vector2.Distance(sound.transform.position, Camera.main.transform.position);
        if (distance > 100) return;
        sound.volume = (playAtDistance ? Mathf.Clamp((2 / Mathf.Abs(distance)), 0, 1) : 1) * volumeModifier;

        sound.Play();



    }

    public static void PlayMusic(AudioSource sound)
    {
        musicBaseVolume = sound.volume;
        PlaySound(sound, 0, false, musicVolume);
        activeMusic = sound;       
    }

}
