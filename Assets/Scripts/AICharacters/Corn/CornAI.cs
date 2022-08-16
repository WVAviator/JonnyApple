using UnityEngine;
using System.Collections;

public class CornAI : MonoBehaviour {

    bool movingLeft;
    bool movingRight;
    bool facingRight;
    bool onGround;
    public Transform groundCheck;
    public Transform cliffCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    Animator anim;
    Rigidbody2D ai;
    public float walkingSpeed = 5;
    public float jumpForce = 100;
    public Transform projectileSpawn;
    public GameObject projectile;
    public float bulletSpeed = 30;
    public int randomRoamFactor = 250;
    public int randomAggressionFactor = 100;
    bool isTrackingPlayer = false;
    public int detectionRange = 15;
    Transform player;
    public Transform arm;
    public Transform aimReference;
    public int activationDistance = 100;

    void Start()
    {
        anim = GetComponent<Animator>();
        ai = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }



    void Update()
    {
        if (Vector2.SqrMagnitude(Camera.main.transform.position - transform.position) > activationDistance * activationDistance) return;

        onGround = (Physics2D.OverlapCircle(groundCheck.position, 1, groundLayer));
        //Debug.Log("OnGround: " + onGround);

        anim.SetBool("onGround", onGround);
        anim.SetBool("isWalking", (movingRight || movingLeft));
        anim.SetFloat("vSpeed", ai.velocity.y);

        if (!onGround) return;

        if (movingRight) MoveRight();
        if (movingLeft) MoveLeft();

        //Debug.Log("Move right: " + movingRight + ", Move left: " + movingLeft);

        DetectPlayer();

        //Debug.Log("Player detected: " + isTrackingPlayer);

        if (!isTrackingPlayer) Roam();
        if (isTrackingPlayer) AttackPlayer();
        AvoidWalls();
        AvoidCliffs();


    }

    void Roam()
    {
        if (Random.Range(0, randomRoamFactor) == 1) StartMovingRight();      
        if (Random.Range(0, randomRoamFactor) == 2) StartMovingLeft();
        if (Random.Range(0, randomRoamFactor) == 3) StopMoving();
    }

    void LateUpdate()
    {
        if (!isTrackingPlayer) return;
        AimAtPlayer();
    }

    void AimAtPlayer()
    {
        Vector2 aimPos = player.transform.position;
        Vector2 aimDir = new Vector2(aimPos.x - aimReference.position.x, aimPos.y - aimReference.position.y);
        float aimAngle = (Mathf.Atan2(aimDir.x, aimDir.y) * Mathf.Rad2Deg);
        if (facingRight)
        {
            arm.eulerAngles = new Vector3(0, 0, (aimAngle - 90));
        }
        if (!facingRight)
        {
            arm.eulerAngles = new Vector3(0, 0, ((aimAngle + 90) * -1));
        }

    }

    void AttackPlayer()
    {
        if (player.position.y > transform.position.y) Jump();
        if (Random.Range(0, randomAggressionFactor) == 1) Shoot();

        if (player.position.x < transform.position.x) StartMovingLeft();       
        if (player.position.x > transform.position.x) StartMovingRight();

        if (Physics2D.OverlapCircle(transform.position, 5, playerLayer)) StopMoving();

    }

    void AvoidCliffs()
    {
        if (Physics2D.OverlapCircle(cliffCheck.position, 1, groundLayer)) return;
        if (movingRight) StopMovingRight();
        if (movingLeft) StopMovingLeft();
    }

    void AvoidWalls()
    {
        if (!Physics2D.OverlapCircle(wallCheck.position, 2, groundLayer)) return;
        if (movingRight) StopMovingRight();
        if (movingLeft) StopMovingLeft();
    }

    void DetectPlayer()
    {
        if (Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer))
        {
            isTrackingPlayer = true;
        } else
        {
            isTrackingPlayer = false;
        }
    }

    void StopMoving()
    {
        StopMovingRight();
        StopMovingLeft();
    }

    void Jump()
    {
        if (!onGround) return;
        ai.AddForce(new Vector2(0, jumpForce));
        anim.SetTrigger("jumpTrigger");
    }

    void Shoot()
    {
        anim.SetTrigger("shootTrigger");
        StartCoroutine("ShootDelay");

        
        
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(Time.deltaTime);

        GameObject projectileClone = (GameObject)Instantiate((GameObject)projectile, projectileSpawn.position, projectileSpawn.rotation);
        if (!facingRight)
            projectileClone.transform.eulerAngles = new Vector3(0, 0, Mathf.Repeat(projectileClone.transform.eulerAngles.z + 180, 360));
        if (facingRight)
            projectileClone.transform.eulerAngles = new Vector3(0, 0, Mathf.Repeat(-projectileClone.transform.eulerAngles.z, 360));
        projectileClone.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(bulletSpeed, 0));
        Destroy(projectileClone, 8);

    }

    void MoveRight()
    {
        ai.velocity = new Vector2(walkingSpeed, ai.velocity.y);
    }

    void MoveLeft()
    {
        ai.velocity = new Vector2(-walkingSpeed, ai.velocity.y);
    }

    void StartMovingRight()
    {
        if (movingLeft) StopMovingLeft();
        if (movingRight) return;
        if (!facingRight) Flip();
        movingRight = true;
    }

    void StartMovingLeft()
    {
        if (movingRight) StopMovingRight();
        if (movingLeft) return;
        if (facingRight) Flip();
        movingLeft = true;
    }

    void StopMovingRight()
    {
        movingRight = false;
    }

    void StopMovingLeft()
    {
        movingLeft = false;
    }

    void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

}
