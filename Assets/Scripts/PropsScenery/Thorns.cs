using UnityEngine;
using System.Collections;

public class Thorns : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.GetComponent<Health>() != null)
        {
            c.gameObject.GetComponent<Health>().SetDead();
            
        }
        //Debug.Log("Collision with thorns logged");
    }
}
