using UnityEngine;
using System.Collections;

public class CornProjectile : MonoBehaviour {

    public GameObject popcorn;
    public float popForce = 100;
    public float popDamage = 13;
    bool hasPopped = false;
    public AudioSource sizzleSound;
    bool attached;
    Vector2 attachedPosition;

    void Update()
    {
        GetComponent<Collider2D>().enabled = !attached;
        if (attached) transform.localPosition = attachedPosition;
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (hasPopped) return;
        hasPopped = true;

        transform.SetParent(c.gameObject.transform);
        attachedPosition = transform.localPosition;
        float random = Random.Range(1f, 4f);

        attached = true;
        
        sizzleSound.loop = true;
        SoundUtility.PlaySound(sizzleSound, 0, true);

        Invoke("Pop", random);

    }

    void Pop()
    {

        sizzleSound.Stop();
        gameObject.SetActive(false);

        GameObject popcornClone = (GameObject)Instantiate((GameObject)popcorn, transform.position, transform.rotation);
        if (transform.parent.GetComponent<Rigidbody2D>() != null)
            transform.parent.GetComponent<Rigidbody2D>().velocity += (Vector2)((transform.parent.position - transform.position).normalized * popForce);
        if (transform.parent.GetComponent<Health>() != null)
            transform.parent.GetComponent<Health>().addDamage(popDamage);

        Destroy(popcornClone, 3);
        Destroy(gameObject, 4);

    }
}
