using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public Transform[] pathPoints;
    public float platformSpeed = 1;
    private bool reversed = false;
    private Transform targetPoint;
    int point;

    public int activationDistance = 75;

    GameObject player;
    Collider2D[] playerColliders;
    Transform playerBase;

    Collider2D platformCollider;

    public LayerMask ignoreLayers;

    void Start()
    {
        transform.position = pathPoints[0].position;
        targetPoint = pathPoints[1];
        point = 1;

        player = GameObject.FindGameObjectWithTag("Player");
        playerColliders = GameObject.FindGameObjectWithTag("Player").GetComponents<Collider2D>();
        playerBase = GameObject.FindGameObjectWithTag("PlayerBase").transform;

        platformCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Vector2.SqrMagnitude(Camera.main.transform.position - transform.position) > activationDistance * activationDistance) return;

        UpdateTarget();
        UpdateMovement();
        OneWayPlatform();        
    }

    void OneWayPlatform()
    {
        foreach (Collider2D col in playerColliders)
            Physics2D.IgnoreCollision(platformCollider, col, (playerBase.position.y < transform.position.y));      
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == ignoreLayers) return;
        c.gameObject.transform.parent = transform;
    }


    void OnCollisionExit2D(Collision2D c)
    {
        c.gameObject.transform.parent = null;
        //Debug.Log("Collision exit Logged");
    }

    void UpdateMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, Time.deltaTime * platformSpeed);
    }

    void UpdateTarget()
    {
        if (Mathf.Abs(transform.position.x - targetPoint.position.x) < 0.01
            && Mathf.Abs(transform.position.y - targetPoint.position.y) < 0.01)
        {
            if (reversed) point -= 1;
            if (!reversed) point += 1;
            if (pathPoints.Length == point)
            {
                reversed = true;
                point -= 2;
            }
            if (point < 0)
            {
                reversed = false;
                point = 1;
            }

            targetPoint = pathPoints[point];
        }
    }
}
