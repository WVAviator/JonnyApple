using UnityEngine;
using System.Collections;

public class CantaloupeAI : MonoBehaviour {

    public float walkingSpeed;
    public float chargingSpeed;
    public float jumpForce;

    public float detectionRange;

    public float ramDamage;
    public int ramDelay;
    public float ramForce;
    int ramTimer = 1;

    public int roamFactor = 100;
    public int aggressionFactor = 100;

    public Transform ramDetect;
    public Transform groundDetect;
    public Transform cliffDetect;

    public LayerMask playerLayer;
    public LayerMask groundLayer;

    public int activationDistance = 100;

    Animator anim;
    Rigidbody2D ai;
    Transform player;

    bool facingRight;
    bool onGround;

    bool walkingRight;
    bool walkingLeft;

    bool charging;


    void Start()
    {
        anim = GetComponent<Animator>();
        ai = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.SqrMagnitude(Camera.main.transform.position - transform.position) > activationDistance * activationDistance) return;

        anim.SetBool("isWalking", ((walkingLeft || walkingRight) && (!charging)));
        anim.SetBool("isCharging", charging);
        anim.SetFloat("vSpeed", ai.velocity.y);

        if (!DetectPlayer()) Roam();
        AvoidWalls();
        if (DetectPlayer()) Attack();
        AvoidCliffs();
        

        onGround = Physics2D.OverlapCircle(groundDetect.position, 1, groundLayer);
        anim.SetBool("onGround", onGround);
        if (!onGround) return;

        if (walkingRight && charging) ChargeRight();
        if (walkingLeft && charging) ChargeLeft();
        if (charging) return;
        if (walkingRight) WalkRight();
        if (walkingLeft) WalkLeft();

    }

    void WalkRight()
    {
        ai.velocity = new Vector2(walkingSpeed, ai.velocity.y);
    }

    void WalkLeft()
    {
        ai.velocity = new Vector2(-walkingSpeed, ai.velocity.y);
    }

    void ChargeRight()
    {
        ai.velocity = new Vector2(chargingSpeed, ai.velocity.y);
    }

    void ChargeLeft()
    {
        ai.velocity = new Vector2(-chargingSpeed, ai.velocity.y);
    }

    bool DetectPlayer()
    {
        if (Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer)) return true;
        return false;
    }

    void Roam()
    {
        if (Random.Range(0, roamFactor) == 1) StartWalkingRight();
        if (Random.Range(0, roamFactor) == 2) StartWalkingLeft();
        if (Random.Range(0, roamFactor) == 3) Stop();
    }

    void Attack()
    {
        if (ramTimer == ramDelay)
        {
            Ram();
            ramTimer = 0;
        }
        ramTimer += 1;
        Vector2 playerPos = player.position;

        if (Random.Range(0, aggressionFactor) != 1) return;

        charging = true;
        if (playerPos.y > transform.position.y - 1.28) Jump();
        if (playerPos.x > transform.position.x) StartWalkingRight();
        if (playerPos.x < transform.position.x) StartWalkingLeft();
        
        
    }

    void Jump()
    {
//        Debug.Log("Jump method called");
        if (!onGround) return;
        ai.AddForce(new Vector2(0, jumpForce));
        anim.SetTrigger("jumpTrigger");
    }

    void AvoidCliffs()
    {
        if (!Physics2D.OverlapCircle(cliffDetect.position, 1, groundLayer))
        {
            if (walkingRight) walkingRight = false;
            if (walkingLeft) walkingLeft = false;
        }
    }

    void AvoidWalls()
    {
        if (Physics2D.OverlapCircle(ramDetect.position, 1, groundLayer))
        {
            Stop();
            if (facingRight) StartWalkingLeft();
            if (!facingRight) StartWalkingRight();
        }
    }

    void StartWalkingRight()
    {
        if (!facingRight) Flip();
        walkingLeft = false;
        walkingRight = true;
    }

    void StartWalkingLeft()
    {
        if (facingRight) Flip();
        walkingRight = false;
        walkingLeft = true;
    }

    void StartCharging()
    {
        charging = true;
    }

    void Stop()
    {
        walkingRight = false;
        walkingLeft = false;
        charging = false;
    }

    void Ram()
    {
        if (!Physics2D.OverlapCircle(ramDetect.position, 1, playerLayer)) return;
        anim.SetTrigger("ramTrigger");
        if (Physics2D.OverlapCircle(ramDetect.position, 1).gameObject.GetComponent<Health>() != null)
        {
            Physics2D.OverlapCircle(ramDetect.position, 1).gameObject.GetComponent<Health>().addDamage(50);
            if (facingRight) Physics2D.OverlapCircle(ramDetect.position, 1).gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ramForce, ramForce));
            if (!facingRight) Physics2D.OverlapCircle(ramDetect.position, 1).gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-ramForce, ramForce));
        }
    }

    void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

}
