using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour {

    public bool isMusicSlider;
    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.normalizedValue = (isMusicSlider ? SoundUtility.musicVolume : SoundUtility.soundVolume);
        slider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    void OnValueChanged()
    {
        SoundUtility.UpdateVolume(slider.normalizedValue, isMusicSlider);
    }


}
