using UnityEngine;
using System.Collections;

public class Item_OrangeSeed : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag.Equals("Player"))
        {
            GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>().AddAmmo(1, Random.Range(3, 6));
            Destroy(gameObject);
        }
    }
}
