using UnityEngine;
using System.Collections;

public class CrushDetect : MonoBehaviour {

    public LayerMask crushLayers;
    public float sensitivity = 1;

	void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, sensitivity, crushLayers))
            GetComponentInParent<Health>().SetDead();
    }
}
