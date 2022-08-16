using UnityEngine;
using System.Collections;

public class HealthUpdater : MonoBehaviour {

	void Update()
    {
        GameManagement.health = GetComponent<Health>().health;
    }
}
