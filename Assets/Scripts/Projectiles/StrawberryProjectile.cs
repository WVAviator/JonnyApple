using UnityEngine;
using System.Collections;

public class StrawberryProjectile : BaseProjectile {

	public Rigidbody2D projectileExtra;
	public float spreadTiming = 0.02f;
	Vector2 spawnPos;
	Quaternion spawnRot;
	Vector2 spawnVel = Vector2.zero;

	void Start() 
	{
		spawnPos = transform.position;
		spawnRot = transform.rotation;

        StartCoroutine(SpawnExtras());       
    }

    void LateUpdate()
    {
        if(spawnVel == Vector2.zero) spawnVel = GetComponent<Rigidbody2D>().velocity;
    }

    IEnumerator SpawnExtras()
    {
        yield return new WaitForSeconds(spreadTiming);
        SpawnExtra();
        yield return new WaitForSeconds(spreadTiming);
        SpawnExtra();
    }

	void SpawnExtra() 
	{
		Rigidbody2D extra = (Rigidbody2D)Instantiate ((Rigidbody2D)projectileExtra, spawnPos, spawnRot);
		extra.velocity = spawnVel;
		Destroy(extra, 5);
	}

}
