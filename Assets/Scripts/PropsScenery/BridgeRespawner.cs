using UnityEngine;
using System.Collections;

public class BridgeRespawner : MonoBehaviour {

    public GameObject bridgePrefab;

    public void RespawnBridge(Vector2 position, Quaternion rotation)
    {
        Instantiate((GameObject)bridgePrefab, position, rotation);
    }


}
