using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지 별로 어떤 기믹을 사용하게 할지 정하는 스크립트
/// </summary>
public class SetupGimmicks : MonoBehaviour
{
    [SerializeField]
    private int Tree;
    [SerializeField]
    private int Metal;
    [SerializeField]
    private int Amp;
    [SerializeField]
    private int Gun;


    void Awake()
    {

        transform.name = "Environment";
    }

    void Start()
    {

    }

}
