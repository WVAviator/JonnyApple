using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Debugger : MonoBehaviour {


	
    public void DisplayMessage(string message)
    {
        GetComponent<Text>().text += "\n" + message;
    }
}
