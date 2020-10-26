using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public GameObject Stage;
    public bool EnterIngame;
    public int maxWorld;

    [SerializeField]
    private bool ismoving;
    public bool IsMoving
    {
        get { return ismoving; }
        set { ismoving = value; }
    }

    [SerializeField]
    private int currentworld;


    [SerializeField] private string world;
    [SerializeField] private string stage;

    Dictionary<string, object> GimmicksAmount;

    /// <summary>
    /// 현재 선택한 월드
    /// </summary>
    public int CurrentWorld
    {
        get{ return currentworld; }
        set
        {
            currentworld += value;

            if (CurrentWorld >= maxWorld)
                currentworld = maxWorld;


            if (CurrentWorld <= 0)
                currentworld = 1;

            print("현재 월드:" + CurrentWorld);
            IsMoving = true;
            //현재 카메라 위치에서 CurrentWorld로 설정한 월드 위치까지 SmoothStep시키기
            //StartCoroutine(CameraMoving());
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void SelectStage()
    {

    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);
        IsMoving = false;
        print("StageManager Awake");
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        //CurrentWorld = 1;
    }
    void FixedUpdate()
    {
        //print("FixedUpdate");
        //왼쪽 오른쪽 버튼을 눌러 월드가 이동하기 위한 코루틴 함수
        if (IsMoving)
        {
            print("카메라 이동중");

            Camera.main.transform.position = new Vector3(
                    Mathf.SmoothStep(Camera.main.transform.position.x, GameObject.Find("World" + CurrentWorld).transform.position.x, 0.3f),         //x
                    Mathf.SmoothStep(Camera.main.transform.position.y, GameObject.Find("World" + CurrentWorld).transform.position.y, 0.3f), -10f);  //y,z

            if (Vector2.Distance(Camera.main.transform.position, GameObject.Find("World" + CurrentWorld).transform.position) <= 0.1f)
                IsMoving = false;
                
        }
    }

    /// <summary>
    /// 왼쪽 오른쪽 버튼을 눌러 월드가 이동하기 위한 코루틴 함수
    /// </summary>
    /// <returns></returns>
    public IEnumerator CameraMoving()
    {
        IsMoving = true;
        while (IsMoving)
        {
            if (Vector2.Distance(Camera.main.transform.position, GameObject.Find("World" + currentworld).transform.position) <= 0.1f)
            {
                //스탑 코루틴해서 코루틴 죽이면 정확히 안죽음... 중첩되면서 점점 카메라 이동 속도가 빨라지는 이슈 있음 break문을 사용하여 빠져나와야 할듯.
                //StopCoroutine(CameraMoving());
                IsMoving = false;
               break;
            }
            else
                Camera.main.transform.position = new Vector3(
                    Mathf.SmoothStep(Camera.main.transform.position.x, GameObject.Find("World" + currentworld).transform.position.x, 0.3f),
                    Mathf.SmoothStep(Camera.main.transform.position.y, GameObject.Find("World" + currentworld).transform.position.y, 0.3f), -10f);
            yield return new WaitForFixedUpdate();
        }
        IsMoving = false;
        //StopCoroutine(CameraMoving());
    }
    /// <summary>
    /// 스테이지 프리펩을 가져온다
    /// </summary>
    public void GetStage(string s)
    {
        print("스테이지명: " + s);

        string[] stagePath = s.Split('-');
        world = stagePath[0];
        stage = stagePath[1];
        string path = "Prefabs/Stages/World" + world + "/" + stage;
        Stage = Resources.Load<GameObject>(path);
        if(Stage==null)
        {
            Debug.LogError("선택한 스테이지가 존재하지 않습니다.");
        }
    }

    public GameObject ClickStage(string s)
    {
        //클릭한 버튼의 스테이지 프리펩을 가져온다
        GetStage(s);
        //카메라 무빙 상태 비활성화
        IsMoving = false;

        GimmicksAmount = Stageload.instance.SearchStageObjectsAmount(world, stage);


        return Stage;
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
            //스테이지 프리팹 생성
            GameObject newstage = Instantiate(Stage);
            //기믹 오브젝트 갯수 설정
            //SetupGimmicks 스크립트 Start 함수에서 갯수 설정됨
            //GameObject.Find("ShowCase").GetComponent<ShowcaseGenerator>().SetupGimmicks(GimmicksAmount);
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

    public void ClickArrow(int v)
    {
        //print(v);
        CurrentWorld = v;
    }

    /// <summary>
    /// 게임이 클리어되고 다음 스테이지 버튼을 클릭시 호출
    /// </summary>
    public GameObject GetNextStage()
    {
        //현재 스테이지의 변수를 1++해서 다음 스테이지 가져옴
        int tmpnextstage = int.Parse(stage);
        tmpnextstage++;
        string NextStage = world + "-" + tmpnextstage;
        print("다음 스테이지" + NextStage);
        GetStage(NextStage);
        if (Stage == null)
            return null;
        else
            return Stage;
    }
    
}