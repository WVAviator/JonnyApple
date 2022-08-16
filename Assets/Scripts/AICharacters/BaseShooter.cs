using UnityEngine;
using System.Collections;

public class BaseShooter : BaseAI {

    public Transform arm;
    public float armAngleOffset;

    public Rigidbody2D projectile;
    public float projectileVelocity = 20;
    public Transform projectileSpawn;
    public float projectileLifetime = 8;

    void LateUpdate()
    {
        if (isTrackingPlayer) Aim();
    }

    protected override void Attack()
    {
        anim.SetTrigger("shootTrigger");
        StartCoroutine("ShootDelay");
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        Rigidbody2D clone = (Rigidbody2D)Instantiate((Rigidbody2D)projectile.GetComponent<Rigidbody2D>(), projectileSpawn.position, projectileSpawn.rotation);

        if (!facingRight)
            clone.transform.eulerAngles = new Vector3(0, 0, Mathf.Repeat(-clone.transform.eulerAngles.z + 180, 360));

        clone.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(projectileVelocity, 0));

        Destroy(clone.gameObject, projectileLifetime);
    }

    void Aim()
    {
        Vector2 aimPos = player.position;
        Vector2 aimDir = new Vector2(aimPos.x - arm.position.x, aimPos.y - arm.position.y);
        float aimAngle = (Mathf.Atan2(aimDir.x, aimDir.y) * Mathf.Rad2Deg);

		arm.eulerAngles = facingRight ? (new Vector3(0, 0, -(aimAngle - 90) + (armAngleOffset*2)))       
			: (new Vector3(0, 0, (aimAngle + 90) + (armAngleOffset*2)));
        
    }
}
