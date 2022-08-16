using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonColor : MonoBehaviour {

    Color savedColor;
    public Color depressedColor;
    public AudioSource buttonPress;

    void Start()
    {
        savedColor = GetComponent<Image>().color;
    }

    public void ButtonDepressed()
    {
        GetComponent<Image>().color = depressedColor;
        SoundUtility.PlaySound(buttonPress);
    }

    public void ButtonReleased()
    {
        GetComponent<Image>().color = savedColor;
    }
}
