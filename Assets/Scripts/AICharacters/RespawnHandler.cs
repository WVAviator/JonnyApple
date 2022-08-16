using UnityEngine;
using System.Collections;

public class RespawnHandler : MonoBehaviour {

	public void InitiateRespawn(GameObject objectToRespawn, Vector2 respawnPosition, int timeToRespawn)
    {
        StartCoroutine(RespawnCo(objectToRespawn, respawnPosition, timeToRespawn));
    }

    IEnumerator RespawnCo(GameObject objectToRespawn, Vector2 respawnPosition, int timeToRespawn)
    {
        yield return new WaitForSeconds(timeToRespawn);

        GameObject respawned = (GameObject)Instantiate((GameObject)objectToRespawn, respawnPosition, Quaternion.identity);

        Health respawnedHealth = respawned.GetComponent<Health>();
        respawnedHealth.respawnable = true;
        respawnedHealth.respawnTime = timeToRespawn;
        respawnedHealth.respawnPrefab = objectToRespawn;
    }
}
