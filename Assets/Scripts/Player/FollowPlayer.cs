using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    GameObject player;
    public float followSpeed;
    public float leadDistance = 2;
    Vector3 destination;
    MyPlayerController mpc;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mpc = player.GetComponent<MyPlayerController>();
    }

    void Update () {

        bool facingRight = mpc.facingRight;
        int leadMultiplier = facingRight ? 1 : -1;
        float lead = leadMultiplier * leadDistance;

        destination = new Vector3(player.transform.position.x + lead,
                        player.transform.position.y, transform.position.z);
        float distanceToDest = Vector2.Distance(transform.localPosition, destination);

        transform.localPosition = Vector3.MoveTowards(transform.position, destination, followSpeed * Time.deltaTime * distanceToDest);


	}
}
