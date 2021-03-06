﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMng : MonoBehaviour
{
    public static GameMng instance;

    public GameObject ClearPanel;
    public Time timescale;
    IEnumerator corutine;
    [SerializeField]
    private bool b_ispause;

    public bool b_IsPause
    {
        get
        {
            return b_ispause;
        }
        set
        {
            if (b_IsPause != value)
            {
                if(corutine!=null)
                    StopCoroutine(corutine);
                corutine = PauseCorutine(value);
                
                StartCoroutine(corutine);
            }
            b_ispause = value;
        }
    }


    /// <summary>
    /// 일시정지 코루틴 false면 재개, true면 일시정지
    /// </summary>
    /// <param name="ispause"></param>
    /// <returns></returns>
    public IEnumerator PauseCorutine(bool ispause)
    {
        while (true)
        {
            if (ispause)
            {
                print("Timescale--");
                float value = Mathf.Lerp(Time.timeScale, 0, 0.05f);
                Time.timeScale = value;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                if (Time.timeScale <= 0.05f)
                {
                    Time.timeScale = 0;
                    StopCoroutine(corutine);
                    print("Stop PauseCorutine");
                }
            }
            else
            {
                print("Timescale++");
                float value = Mathf.Lerp(Time.timeScale, 1, 0.05f);
                Time.timeScale = value;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                if (Time.timeScale >= 0.99f)
                {
                    Time.timeScale = 1;
                    StopCoroutine(corutine);
                    print("Stop PauseCorutine");
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        ClearPanel = GameObject.Find("ClearPanel");
        
    }
    void Start()
    {
        //다른 맵에서 클리했을시 일시정지 됐기 때문에 다음맵으로 넘어가는 버튼을 클릭하면 멈춰있기 때문에 여기서 1로 다시 초기화
        Time.timeScale = 1.0f;
        //DontDestroyOnLoad(this);
        SetisPause(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 클리어
    /// </summary>
    public void Clear()
    {
        if (!b_ispause)
        {
            print("Clear");
            b_IsPause = true;
            
            ClearPanel.GetComponent<ClearPanel>().Popup();
            //타임 스케일 점점 줄이기(슬로우 모션)
            
        }
    }
    public void Restart()
    {

        SceneManager.LoadScene("ingame");

    }
    public void Stage()
    {
        SceneManager.LoadScene("Stage");
    }

    public void SetisPause(bool b)
    {
        b_IsPause = b;

        //if (b)
        //    Time.timeScale = 0;
        //else
        //    Time.timeScale = 1;

    }
    public bool GetisPause()
    {
        return b_IsPause;
    }
}
