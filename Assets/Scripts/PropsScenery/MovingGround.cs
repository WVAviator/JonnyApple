using UnityEngine;
using System.Collections;

public class MovingGround : MonoBehaviour {

    public Transform[] pathPoints;
    public float groundSpeed = 1;
    private bool reversed = false;
    private Transform targetPoint;
    int point;

    void Start()
    {
        transform.position = pathPoints[0].position;
        targetPoint = pathPoints[1];
        point = 1;
    }

    void Update()
    {
        UpdateTarget();
        UpdateMovement();
    }

    void UpdateMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, Time.deltaTime * groundSpeed);
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
