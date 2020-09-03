using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public GameObject Stage;
    public bool EnterIngame;

    public bool ismoving;

    //public bool IsMoving
    //{
    //    get { return ismoving; }
    //    set { ismoving = value; }
    //}


    /// <summary>
    /// 사용X
    /// </summary>
    [SerializeField]
    private int currentworld;

    ///// <summary>
    ///// 현재 선택한 월드
    ///// </summary>
    //public int CurrentWorld
    //{
    //    get
    //    {
    //        return currentworld;
    //    }
    //    set
    //    {
    //        currentworld += value;
            
    //        print("현재 월드:" + CurrentWorld);
            
    //        //현재 카메라 위치에서 CurrentWorld로 설정한 월드 위치까지 SmoothStep시키기

    //    }
    //}


    void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);
        ismoving = false;
        print("StageManager Awake");
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        //CurrentWorld = 1;
    }
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (ismoving)
        {
            print("카메라 이동중");
            Camera.main.transform.position = new Vector2(Mathf.SmoothStep(Camera.main.transform.position.x, GameObject.Find("World"+ currentworld).transform.position.x, 0.5f),
                                                        Mathf.SmoothStep(Camera.main.transform.position.y, GameObject.Find("World" + currentworld).transform.position.y, 0.5f));
            //if (Vector2.Distance(Camera.main.transform.position, GameObject.Find("World" + CurrentWorld).transform.position) <= 0.1f)
               // ismoving = false;
        }
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

    public void ClickArrow(string s)
    {
        print(s);
        if(s=="l")
        {
            print("왼쪽 버튼");
            if (currentworld <= 0)
                currentworld = 1;
            else
                currentworld--;

            ismoving = true;
        }
        else if(s=="r")
        {
            print("오른쪽 버튼");
            currentworld++;

            ismoving = true;
        }
        
    }
}
