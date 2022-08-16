using UnityEngine;
using System.Collections;

public class LevelMusic : MonoBehaviour {

    public AudioSource music;
    Invincibility inv;
    public AudioSource invMusic;

    bool invMusicPlaying;
    bool musicPlaying;

	void Start ()
    {
        PlayLevelMusic();
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Invincibility>();
	}

    void Update()
    {
        if (inv.isInvincible) PlayInvMusic();
        if (!inv.isInvincible) PlayLevelMusic();
    }

    void PlayInvMusic()
    {
        if (invMusicPlaying) return;
        music.Stop();
        SoundUtility.activeMusic.Stop();
        SoundUtility.PlayMusic(invMusic);
        musicPlaying = false;
        invMusicPlaying = true;
    }

    void PlayLevelMusic()
    {
        if (musicPlaying) return;
        invMusic.Stop();
        SoundUtility.PlayMusic(music);
        invMusicPlaying = false;
        musicPlaying = true;
    }
	
}
