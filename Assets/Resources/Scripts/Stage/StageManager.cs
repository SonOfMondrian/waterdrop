using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public GameObject Stage;
    public bool EnterIngame;

    void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }
    void Start()
    {
        
    }
    /// <summary>
    /// 스테이지 프리펩을 가져온다
    /// </summary>
    public void GetStage(string s)
    {
        print("스테이지명: " + s);

        string[] stagePath = s.Split('-');
        string path = "Prefabs/Stages/World" + stagePath[0] + "/" + stagePath[1];
        Stage = Resources.Load<GameObject>(path);
    }
    /// <summary>
    /// 씬 로드 이벤트
    /// </summary>
    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //인게임 씬에 진입하면 
        if (arg0.name == "Ingame")
        {
            print("SceneManager_sceneLoaded: Ingame");
            //스테이지 프리팹 생성후
            GameObject newstage = Instantiate(Stage);
        }
        else if(arg0.name == "Stage")
        {
            //
           // Destroy(this.gameObject);
        }
    }

    public void DestroyManager()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        Destroy(this.gameObject);
    }
}
