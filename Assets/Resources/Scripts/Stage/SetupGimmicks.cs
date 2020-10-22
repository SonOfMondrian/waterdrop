using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지 별로 어떤 기믹을 사용하게 할지 정하는 스크립트
/// </summary>
public class SetupGimmicks : MonoBehaviour
{
    GameObject Showcase;
    [SerializeField]
    int[] Gimmicks = new int[5]; //0:Tree 1:Metal 2:Amp 3:Gun 4:Gravity


    void Awake()
    {
        Showcase = GameObject.Find("ShowCase");
        transform.name = "Environment";
    }

    void Start()
    {
        Showcase.GetComponent<ShowcaseGenerator>().SetupGimmicks(Gimmicks);
    }
}
