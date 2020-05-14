using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : SceneController {
    
    public Button startBtn, exitBtn;
    public Text bestScore;

    private void Start()
    {
        Globals.LoadData();
        startBtn.onClick.AddListener(Start_OnClick);
        exitBtn.onClick.AddListener(Exit_OnClick);
        bestScore.text = "Best Score: " + Globals.game.score;

        if (GameObject.Find("MusicController(Clone)") == null)
        {
            var pom = Instantiate(Resources.Load<GameObject>("Prefabs/UI/MusicController"));
            DontDestroyOnLoad(pom);
        }
    }

    private void Exit_OnClick()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (GameObject.Find("MusicController(Clone)").transform.Find("Music").GetComponent<AudioSource>().volume < 0.15f)
            GameObject.Find("MusicController(Clone)").transform.Find("Music").GetComponent<AudioSource>().volume += 0.01f;
    }

    private void Start_OnClick()
    {
        GameObject.Find("MusicController(Clone)").transform.Find("Fx_Btn").GetComponent<AudioSource>().Play();
        GameObject pom = Instantiate(Resources.Load<GameObject>("Prefabs/UI/TransparentPanel"), GameObject.Find("Canvas").transform, false);
        pom.GetComponent<SceneTransitionController>().Out("Gameplay");
    }
}
