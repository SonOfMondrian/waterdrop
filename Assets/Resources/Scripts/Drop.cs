using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject Water;
    public GameObject Point;
    public float intervaltime;
    void Start()
    {
        Water = Resources.Load<GameObject>("Prefabs/Water");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        intervaltime += Time.deltaTime;
        if (intervaltime > 0.3f)
        {
            Instantiate(Water, Point.transform);
            intervaltime = 0;
        }
    }
    public void drop()
    {
        Instantiate(Water, Point.transform);
    }
}
