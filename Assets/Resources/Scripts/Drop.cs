using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject Water;
    public GameObject Water2;
    public GameObject Point;
    public float intervaltime;
    /// <summary>
    /// 몇초마다 생성되는가
    /// </summary>
    [SerializeField]
    private float GenTime;

    /// <summary>
    /// +- 생성되는 범위 값
    /// </summary>
    [SerializeField]
    [Range(0,1)]
    private float GenRange;

    void Start()
    {
        Water = Resources.Load<GameObject>("Prefabs/etcs/Water");
        Water2 = Resources.Load<GameObject>("Prefabs/etcs/Water2");
        Point = transform.Find("Point").gameObject;
    }

    void FixedUpdate()
    {
        intervaltime += Time.deltaTime;
        if (intervaltime > GenTime)
        {
            drop();
            intervaltime = 0;
        }
    }

    public void drop()
    {
        float x = Random.Range(-GenRange, GenRange);
        //Vector3 genpos = new Vector3(Point.transform.position.x + x, Point.transform.position.y, Point.transform.position.z);
        GameObject newWater= Instantiate(Water2,Point.transform);
        newWater.transform.position = new Vector2(newWater.transform.position.x + x, newWater.transform.position.y);
        //newWater.GetComponent<Rigidbody2D>().AddForce(Vector2.right * x);
    }
}
