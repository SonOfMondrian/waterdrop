using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMng : MonoBehaviour
{
    public static GameMng instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        //DontDestroyOnLoad(this);
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
}
