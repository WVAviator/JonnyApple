using UnityEngine;
using System.Collections;

public class BaseProjectile : MonoBehaviour {

    Animator anim;
    public AudioSource shootSound;
    public AudioSource impactSound;

    public float damage;
    bool impact;

    public bool wasFiredByPlayer;
    public int damageScore = 14;
    public int killScore = 875;

    Rigidbody2D rb;

    Score scoreObject;

    void Awake()
    {
        anim = GetComponent<Animator>();
        
        scoreObject = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        SoundUtility.PlaySound(shootSound, 0.1f, !wasFiredByPlayer);
    }

    public Rigidbody2D GetRB() { return rb; }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (impact) return;
        impact = true;

		if (anim != null) anim.SetTrigger("impactTrigger");
        SoundUtility.PlaySound(impactSound, 0.1f, true);

        OnImpact();

        DamageHealth(c.gameObject);
		Destroy(gameObject, 3);

    }

    void DamageHealth(GameObject go)
    {
        if (damage == 0) return;
        Health goHealth = go.GetComponent<Health>();
        if (goHealth == null) return;

        goHealth.addDamage(damage);

        if (wasFiredByPlayer) AddScore(goHealth.health);
    }

    void AddScore(float health)
    {
        int score = 0;
        if (health <= 0) score = killScore;
        if (health > 0) score = damageScore;

        scoreObject.AddScore(score);
    }

    protected virtual void OnImpact()
    {

    }

}
