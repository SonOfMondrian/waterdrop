using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject Water;
    public GameObject Water2;
    public GameObject Point;
    public float intervaltime;
    void Start()
    {
        Water = Resources.Load<GameObject>("Prefabs/Water");
        Water2 = Resources.Load<GameObject>("Prefabs/Water2");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        intervaltime += Time.deltaTime;
        if (intervaltime > 0.3f)
        {
            drop();
            intervaltime = 0;
        }
    }
    public void drop()
    {
        Instantiate(Water2, Point.transform);
    }
}
