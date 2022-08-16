using UnityEngine;
using System.Collections;

public class BridgeManager : MonoBehaviour {

    public int distanceDespawn = 70;
    public int distanceSpawn = 50;
    GameObject[] bridges;
    Transform cam;

    void Start()
    {
        bridges = GameObject.FindGameObjectsWithTag("Bridge");
        cam = Camera.main.transform;
        foreach (GameObject go in bridges) go.SetActive(false);
    }

    void Update()
    {
        foreach (GameObject go in bridges)
        {
            float sqrDistance = Vector2.SqrMagnitude(go.transform.position - cam.position);
            if (sqrDistance < distanceSpawn * distanceSpawn) go.SetActive(true);
            if (sqrDistance > distanceDespawn * distanceDespawn) go.SetActive(false);
        }

    }
}
