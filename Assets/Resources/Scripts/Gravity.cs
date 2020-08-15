using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravitypower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().gravityScale = 0;
        float distance = Vector3.Distance(transform.position, other.transform.position);
        float power = 1/distance * gravitypower;
        other.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.position.x - other.transform.position.x,
                                                                transform.position.y - other.transform.position.y).normalized * power);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
