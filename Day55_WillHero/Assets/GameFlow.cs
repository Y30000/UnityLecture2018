using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : Singleton<GameFlow> {

    public RectTransform gameSplash;
    public RectTransform eventUI;
    public RectTransform titleSence;
    public RectTransform playScene;
    public RectTransform restartgameUI;



    [HideInInspector]
    public Animator fsm;

    [HideInInspector]
    public PlayerController player;

    protected GameFlow() { }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        fsm = GetComponent<Animator>();
        Physics.gravity *= 4f;

        StartCoroutine(RestartGame());
	}

    public IEnumerator RestartGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Play");
        while (!asyncLoad.isDone)
        {
            print(asyncLoad.progress);
            yield return null;
        }
        //Scene 로딩 끝
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
}
