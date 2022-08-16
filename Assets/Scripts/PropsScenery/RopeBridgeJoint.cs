using UnityEngine;
using System.Collections;

public class RopeBridgeJoint : MonoBehaviour {

    public bool isBroken;

	void OnJointBreak2D(Joint2D joint)
    {
        isBroken = true;
    }
}
