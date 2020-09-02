using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject Pausepanel;

    void Awake()
    {
        Pausepanel = GameObject.Find("PausePanel");
        Pausepanel.transform.localScale = new Vector3(1, 1, 1);
        Pausepanel.SetActive(false);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click(string ob)
    {
        if (ob == "Pause")
        {
            //패널 활성화
            OpenPausePanel();
            
        }
        else if (ob == "Start")
        {
            
            Resume();
        }
        else if (ob == "Restart")
        {
            Restart();
        }
        else if (ob == "Stage")
        {
            Stage();
        }
    }

    void OpenPausePanel()
    {
        print("OpenPausePanel");
        //현재 유니티 버그. 버튼을 눌러 오브젝트 활성화 할땐 두번 호출해 줘야 정상 작동한다.
        Pausepanel.SetActive(true);
        Pausepanel.SetActive(true);

        //GameMng 에서 일시정지 bool 초기화
        GameMng.instance.SetisPause(true);
        
        //시간정지.
    }

    void Resume()
    {
        Pausepanel.SetActive(false);
        GameMng.instance.SetisPause(false);
    }
    void Restart()
    {
        
        SceneManager.LoadScene("ingame");

    }
    void Stage()
    {
        StageManager.instance.DestroyManager();
        SceneManager.LoadScene("Stage");

    }
}
