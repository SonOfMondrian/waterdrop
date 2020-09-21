using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water1 : MonoBehaviour
{
    private float mag;
    private Vector2 velocity;
    private float sleepTimer;
    [SerializeField]
    private float destroytime;
    [SerializeField]
    private float AmpBouncePower;
    [SerializeField]
    private float MetalBouncePower;
    [SerializeField]
    private float TreeBouncePower;
    Rigidbody2D rig;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        if (transform.position.y <= -10)
            Destroy(this.gameObject);

        mag = rig.velocity.magnitude;
        velocity = rig.velocity;

        if (mag <= 0.05f)
        {
            sleepTimer += Time.deltaTime;
            if (sleepTimer >= destroytime)
                Destroy(this.gameObject);
        }
        else
            sleepTimer = 0;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag=="Amp")
        {
            print(velocity.normalized);
            Vector2 reflectVector = Vector2.Reflect(velocity.normalized, coll.contacts[0].normal);
            rig.velocity = reflectVector * AmpBouncePower*mag;
        }
        else if(coll.gameObject.tag == "Metal")
        {
            Vector2 reflectVector = Vector2.Reflect(velocity.normalized, coll.contacts[0].normal);
            rig.velocity = reflectVector * MetalBouncePower * mag;
        }
    }
}
