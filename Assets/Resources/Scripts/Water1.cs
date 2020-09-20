using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water1 : MonoBehaviour
{
    public float mag;
    public bool isSleep;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isSleep)
            return;

        if(transform.position.y <=-10)
            Destroy(this.gameObject);

        mag = GetComponent<Rigidbody2D>().velocity.magnitude;
        if(mag <=0.0001f)
        {
            isSleep = true;
            Destroy(this.gameObject,1f);
        }
    }
}
