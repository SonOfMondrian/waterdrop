using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Remove : MonoBehaviour,IPointerUpHandler
{

    public void OnPointerUp(PointerEventData eventData)
    {
        print("Remove Btn Click");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
