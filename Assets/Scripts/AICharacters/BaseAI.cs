using UnityEngine;
using System.Collections;

public class BaseAI : MonoBehaviour {

    bool movingRight;
    bool movingLeft;
    public bool facingRight;
    bool onGround;

    public Transform cliffCheck;
    public Transform wallCheck;
    public Transform groundCheck;

    public LayerMask groundLayer;
    public LayerMask playerLayer;

    public float movementSpeed = 7;
    public float jumpVel = 30;

    public int roaming = 300;
    public int aggression = 150;

    public float playerDetectionRange = 19;
    public float maintainDistance = 5;
    protected bool isTrackingPlayer;

    public int activationDistance = 100;

    protected Transform player;
    protected Animator anim;
    protected Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (Vector2.SqrMagnitude(Camera.main.transform.position - transform.position) > activationDistance * activationDistance) return;

        anim.SetBool("onGround", onGround);
        anim.SetBool("isMoving", (movingLeft || movingRight));
        anim.SetFloat("vSpeed", rb.velocity.y);

        DetectPlayer();

        Roam();
        if (isTrackingPlayer) NavigateToPlayer();

        AdditionalMovement();

		AvoidWalls();
		AvoidCliffs();

        GroundCheck();
        if (!onGround) return;

        if (movingRight) rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        if (movingLeft) rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);

    }

    void DetectPlayer()
    {
        if (Physics2D.OverlapCircle(transform.position, playerDetectionRange, playerLayer))
        {
            isTrackingPlayer = true;
            return;
        }

        isTrackingPlayer = false;

    }

    void NavigateToPlayer()
    {
        if (player.position.x > transform.position.x) StartMovingRight();
        if (player.position.x < transform.position.x) StartMovingLeft();
        if (Vector2.Distance(player.position, transform.position) < maintainDistance) Stop();

        if (Random.Range(0, aggression) != 1) return;

        if (player.position.y - 1 > transform.position.y) Jump();
        Attack();

    }

    void Roam()
    {
        int randomMovement = Random.Range(0, roaming);

        if (randomMovement == 1) StartMovingRight();
        if (randomMovement == 2) StartMovingLeft();
        if (randomMovement == 3) Stop();
    }

    protected virtual void Attack()
    {

    }

    protected virtual void AdditionalMovement()
    {

    }

    void AvoidCliffs()
    {
        if (Physics2D.OverlapCircle(cliffCheck.position, 1, groundLayer)) return;
        Stop();
    }

    void AvoidWalls()
    {
        if (!Physics2D.OverlapCircle(wallCheck.position, 1, groundLayer)) return;
        Stop();
    }

    void GroundCheck()
    {
        onGround = Physics2D.OverlapCircle(groundCheck.position, 1, groundLayer);
    }

    void StartMovingRight()
    {
        if (movingLeft) Stop();
        if (!facingRight) Flip();
        movingRight = true;
    }

    void StartMovingLeft()
    {
        if (movingRight) Stop();
        if (facingRight) Flip();
        movingLeft = true;
    }

    void Stop()
    {
        if (movingRight) movingRight = false;
        if (movingLeft) movingLeft = false;
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        facingRight = !facingRight;
    }

    void Jump()
    {
        anim.SetTrigger("jumpTrigger");
        rb.velocity = new Vector2(rb.velocity.x, jumpVel);
    }

}
