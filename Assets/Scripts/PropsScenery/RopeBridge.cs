using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeBridge : MonoBehaviour {

    public Transform HandrailRope;
    public Transform RailingRopes;
    public Transform PlankRope;
    RopeBridgeJoint[] allJoints;
    public float respawnTime = 90;
    bool isRespawning;
    Vector2 pos;
    Quaternion rot;

    void Start()
    {
        pos = transform.position;
        rot = transform.rotation;
        allJoints = GetAllPieces();
        StartCoroutine(CheckForCollapse());
    }

    IEnumerator CheckForCollapse()
    {
        for (;;)
        {
            yield return new WaitForSeconds(5);
            if (!HasCollapsed(allJoints)) continue;
            if (isRespawning) continue;
            isRespawning = true;
            StartCoroutine(RespawnBridge());
        }
    }

    bool HasCollapsed(RopeBridgeJoint[] joints)
    {
        foreach (RopeBridgeJoint joint in joints)
        {
            if (joint.isBroken) return true;
        }
        return false;
    }

    IEnumerator RespawnBridge()
    {
        yield return new WaitForSeconds(respawnTime);
        GameObject.FindGameObjectWithTag("BridgeRespawner").GetComponent<BridgeRespawner>().RespawnBridge(pos, rot);
        Destroy(gameObject);
    }

    RopeBridgeJoint[] GetAllPieces()
    {
        List<RopeBridgeJoint> list = new List<RopeBridgeJoint>();
        
        foreach (RopeBridgeJoint joint in HandrailRope.GetComponentsInChildren<RopeBridgeJoint>())
        {
            list.Add(joint);
        }
        foreach (RopeBridgeJoint joint in RailingRopes.GetComponentsInChildren<RopeBridgeJoint>())
        {
            list.Add(joint);
        }
        foreach (RopeBridgeJoint joint in PlankRope.GetComponentsInChildren<RopeBridgeJoint>())
        {
            list.Add(joint);
        }

        return list.ToArray();
    }
}
