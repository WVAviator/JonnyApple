using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sign : MonoBehaviour {

    public string text;
    bool bySign;
    int count = 0;

    GameObject player;
    Text tutText;
    Image tutImage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tutText = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<Text>();
        tutImage = GameObject.FindGameObjectWithTag("Tutorial").GetComponentInParent<Image>();
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.gameObject.Equals(player)) return;
        bySign = true;
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (!c.gameObject.Equals(player)) return;
        bySign = false;
        HideText();
        count = 0;
    }

    void Update()
    {
        if (count == 30) DisplayText();
        if (bySign) count++;
    }

    void DisplayText()
    {
        tutText.text = text;
        tutImage.color = new Color(255, 255, 255, 255);
    }

    void HideText()
    {
        tutText.text = "";
        tutImage.color = new Color(255, 255, 255, 0);
    }
}
