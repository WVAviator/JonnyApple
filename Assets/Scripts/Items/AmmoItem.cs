using UnityEngine;
using System.Collections;

public class AmmoItem : MonoBehaviour {

    public int minimumAmmo = 2;
    public int maximumAmmo = 5;
    public int inventoryIndex;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.Equals(GameObject.FindGameObjectWithTag("Player")))
        {
            GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>().AddAmmo(inventoryIndex, Random.Range(minimumAmmo, maximumAmmo));
            Destroy(gameObject);
        }
    }
}
