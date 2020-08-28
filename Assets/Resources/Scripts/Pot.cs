using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pot : MonoBehaviour
{
    TextMesh text;
    /// <summary>
    /// 남은 물 갯수 (목표 물 갯수)
    /// </summary>
    [SerializeField]
    private int GoalWater;
    
    /// <summary>
    /// 현재 남은 물 갯수
    /// </summary>
    [SerializeField]
    private int curwater;

    public int CurWater
    {
        get { return curwater; }
        set
        {
            curwater += value;

            
            
            text.text = CurWater.ToString();

            if (CurWater == 0)
                GameMng.instance.Clear();
        }
    }


    /// <summary>
    /// 남은 물 갯수가 다시 올라가기 시작할때까지 시간
    /// </summary>
    [SerializeField]
    private float StartIncreaseTime;

    /// <summary>
    /// 남은 물 갯수가 올라가는 속도(간격 또는 시간)
    /// </summary>
    [SerializeField]
    [Tooltip("남은 물 갯수가 올라가는 속도(간격 또는 시간)")]
    private float intervaltime;

    public float curtime;
    [Tooltip("차오르는 속도")]
    public float intervalCurtime;
    void Start()
    {
        text = transform.Find("watertext").GetComponent<TextMesh>();
        text.text = GoalWater.ToString();

        CurWater = GoalWater;
    }

    // Update is called once per frame
    void Update()
    {
        curtime += Time.deltaTime;
        //IncreaseWaterTime초 동안 안들어 오면 
        if (curtime > StartIncreaseTime && GoalWater != CurWater)
        {
            //print("물이 다시 차오르기 시작");

            intervalCurtime += Time.deltaTime;      //intervalCurtime초가 늘어남

            //intervaltime초 간격으로 남은 물 갯수가 다시 올라감
            if (intervalCurtime > intervaltime)
            {
                CurWater = 1;
                intervalCurtime = 0;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water")
        {
            print("Pot에 들어감");
            CurWater = -1;
            curtime = 0;
            Destroy(other.gameObject);
        }
    }
}
