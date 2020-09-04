using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField]
    private float power;

    public GameObject Point;

    void Awake()
    {
        Point = transform.parent.Find("Point").gameObject;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        print("물방울 발사!");
        if (other.collider.tag == "Water")
        {
            other.transform.position = Point.transform.position;
            other.collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.collider.GetComponent<Rigidbody2D>().AddForce(Point.transform.right * -1 * power);
        }
    }
}
