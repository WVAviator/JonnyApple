using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PercentageSlider : MonoBehaviour {

    Text text;
    public bool isMusic;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (isMusic) text.text = "Music  " + ((int)(SoundUtility.musicVolume * 100)).ToString((SoundUtility.musicVolume < 0.1) ? "D1" : "D2") + "%";
        if (!isMusic) text.text = "Sound  " + ((int)(SoundUtility.soundVolume * 100)).ToString((SoundUtility.soundVolume < 0.1) ? "D1" : "D2") + "%";
    }
}
