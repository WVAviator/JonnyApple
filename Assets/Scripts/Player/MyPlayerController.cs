using UnityEngine;
using System.Collections;

public class MyPlayerController : MonoBehaviour {

    Rigidbody2D player;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;
    public KeyCode shootKey;
    public KeyCode debugKey;
    public bool facingRight = true;
    public float playerSpeed;
    Animator anim;
    public bool onGround = false;
    public float jumpForce = 100;
    public Transform groundTrigger;
    public LayerMask groundLayer;
    public Transform gunSpawn;
    GameObject projectile;
    public float projectileVelocity = 20;
    public bool isFallingToDeath = false;
    private bool moveRight = false;
    private bool moveLeft = false;
    private int doubleTapCountRight = 0;
    private int doubleTapCountLeft = 0;
    public int doubleTapThreshold = 15;
    public float runSpeed;
    private bool runRight;
    private bool runLeft;
    public AudioSource footstep;
    float move;
    public float acceleration = 0.9f;
    public Rigidbody2D defaultAmmo;
    public float aerialMovementFactor = 0.4f;
    bool hasJumped;
    float xVel;
    public float jumpVelocity = 30;
    InventoryManager inv;
 

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameManagement.MoveToActiveCP(transform);
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
    }

    void Update()
    {

        if (inv.GetProjectile() != null)
            projectile = inv.GetProjectile();


        if (runRight || runLeft) anim.SetBool("isRunning", true);
        if (moveLeft || moveRight) anim.SetBool("isWalking", true);
        if (!runRight && !runLeft) anim.SetBool("isRunning", false);
        if (!moveLeft && !moveRight) anim.SetBool("isWalking", false);

        if (doubleTapCountRight > 0) doubleTapCountRight--;
        if (doubleTapCountLeft > 0) doubleTapCountLeft--;

        onGround = Physics2D.OverlapCircle(groundTrigger.position, 1, groundLayer);
        hasJumped = onGround ? false : hasJumped;

        anim.SetBool("OnGround", onGround);
        anim.SetFloat("VSpeed", player.velocity.y);

        if (player.velocity.y < -100)
        {
            isFallingToDeath = true;
        }
        anim.SetBool("DeathFall", isFallingToDeath);

        if (Input.GetKeyDown(shootKey) && (!isFallingToDeath)) Shoot();

        if (Input.GetKeyDown(jumpKey)) Jump();
        if (Input.GetKeyDown(debugKey)) Debug.Log("Logged Speed: " + xVel);
    }

    void FixedUpdate()
    { 

        if (GetComponent<DeadApple>().hasDied == true) return;

        xVel = player.velocity.x;
        if (xVel == 0) move = 0;

        if (moveRight) MoveRight();
        if (moveLeft) MoveLeft();
        if (runRight) RunRight();
        if (runLeft) RunLeft();
        if (!runLeft && !runRight && !moveLeft && !moveRight) Mathf.MoveTowards(move, 0, acceleration);

    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    public void Jump()
    {
        if (!onGround && hasJumped) return;
        hasJumped = true;
        player.velocity = new Vector2(player.velocity.x, jumpVelocity);
        anim.SetTrigger("JumpTrigger");
    }

    public void MoveRight()
    {
        if (!facingRight) Flip();
        move = Mathf.MoveTowards(move, 1, acceleration);

        if (!onGround && (xVel >= (playerSpeed * aerialMovementFactor))) return;
        player.velocity = new Vector2(playerSpeed * move, player.velocity.y);

    }

    public void RunRight()
    {
        if (!facingRight) Flip();
        move = Mathf.MoveTowards(move, 1, acceleration);
        if (!onGround && (xVel >= (playerSpeed * aerialMovementFactor))) return;
        player.velocity = new Vector2(move * runSpeed, player.velocity.y);

    }

    public void MoveLeft()
    {
        if (facingRight) Flip();
        move = Mathf.MoveTowards(move, -1, acceleration);
        if (!onGround && (xVel <= (-playerSpeed * aerialMovementFactor))) return;
        player.velocity = new Vector2(move * playerSpeed, player.velocity.y);

    }

    public void RunLeft()
    {
        if (facingRight) Flip();
        move = Mathf.MoveTowards(move, -1, acceleration);
        if (!onGround && (xVel <= (-playerSpeed *  aerialMovementFactor))) return;
        player.velocity = new Vector2(move * runSpeed, player.velocity.y);

    }

    public void StartMoveRight()
    {
        if (doubleTapCountRight > 0)
        {
            runRight = true;
        }
        else
        {
            moveRight = true;
            doubleTapCountRight = doubleTapThreshold;
        }
    
    }

    public void StopMoveRight()
    {
        moveRight = false;
        runRight = false;

    }

    public void StartMoveLeft()
    {
        if (doubleTapCountLeft > 0)
        {
            runLeft = true;
        }
        else
        {
            moveLeft = true;
            doubleTapCountLeft = doubleTapThreshold;
        }
    }

    public void StopMoveLeft()
    {
        runLeft = false;
        moveLeft = false;
    }

    public void Shoot()
    {
        if (isFallingToDeath) return;
        if (runLeft || runRight) return;

        anim.SetTrigger("Shoot");

        StartCoroutine("ShootDelay");

        
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(Time.deltaTime);

        BaseProjectile clone = ((GameObject)Instantiate((GameObject)projectile, gunSpawn.position, gunSpawn.rotation)).GetComponent<BaseProjectile>();
        inv.DecrementActiveAmmo();

        if (!facingRight)
            clone.transform.eulerAngles = new Vector3(0, 0, Mathf.Repeat(-clone.transform.eulerAngles.z + 180, 360));

        clone.GetRB().AddRelativeForce(new Vector2(projectileVelocity, 0));

        clone.wasFiredByPlayer = true;
        Destroy(clone.gameObject, 5.0f);

    }
    
    

}
