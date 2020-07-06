using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <=-10)
            Destroy(this.gameObject);
    }
}
