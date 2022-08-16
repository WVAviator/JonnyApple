using UnityEngine;
using System.Collections;

public class CornItem : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.Equals(GameObject.FindGameObjectWithTag("Player")))
        {
            GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>().AddAmmo(2, Random.Range(2, 5));
            Destroy(gameObject);
        }
    }
}
