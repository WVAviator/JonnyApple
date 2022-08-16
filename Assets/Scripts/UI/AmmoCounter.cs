using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour {

    Text text;
    int count;

    void Start()
    {
        text = GetComponent<Text>();
        count = 0;
    }

	void Update ()
    {
        count = gameObject.GetComponentInParent<InventoryManager>().GetAmmoCount(gameObject.transform.parent.gameObject);
        text.text = Mathf.Clamp(count, 0, 999).ToString();


    }
}
