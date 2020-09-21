using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water1 : MonoBehaviour
{
    public float mag;
    public Vector2 velocity;
    public bool isSleep;
    float sleepTimer;
    public float destroytime;
    public float bouncepower;
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

            //print("충돌한 물체 법선 : "+coll.contacts[0].normal);
            print(velocity.normalized);
            Vector2 reflectVector = Vector2.Reflect(velocity.normalized, coll.contacts[0].normal);
            //print("반사 벡터: " + reflectVector);
            rig.velocity = reflectVector * bouncepower*mag;
            //GetComponent<Rigidbody2D>().AddForce(reflectVector * mag  *bouncepower);
        }
    }
}
