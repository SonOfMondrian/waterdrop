using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMng : MonoBehaviour
{
    public static GameMng instance;

    bool b_isPause;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        //DontDestroyOnLoad(this);
        SetisPause(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clear()
    {
        SceneManager.LoadScene("Clear");
    }
    public void Restart()
    {
        
        SceneManager.LoadScene("ingame");
        
    }

    public void SetisPause(bool b)
    {
        b_isPause = b;

        if (b)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

    }
    public bool GetisPause()
    {
        return b_isPause;
    }
}
