using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : Singleton<GameFlow> {

    public RectTransform gameSplash;
    public RectTransform eventUI;

    protected GameFlow() { }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
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
    }
}
