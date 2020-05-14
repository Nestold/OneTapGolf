using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelController : MonoBehaviour {
    [SerializeField]
    private Text score, bestScore;
    [SerializeField]
    private Button exitBtn, restartBtn;

    public void Init(uint score, UnityEngine.Events.UnityAction restartEvent)
    {
        this.score = transform.GetChild(0).Find("ScoreValue").GetComponent<Text>();
        bestScore = transform.GetChild(0).Find("BestScoreValue").GetComponent<Text>();
        exitBtn = transform.GetChild(0).Find("ExitBtn").GetComponent<Button>();
        restartBtn = transform.GetChild(0).Find("RestartBtn").GetComponent<Button>();
        exitBtn.onClick.AddListener(Exit_OnClick);

        this.score.text = "SCORE: " + score;
        this.bestScore.text = "BEST: " + Globals.game.score;

        Debug.Log(score + " : " + Globals.game.score);
        if (score > Globals.game.score)
            Globals.game.score = score;

        restartBtn.onClick.AddListener(restartEvent);
    }
    private void Exit_OnClick()
    {
        GameObject.Find("MusicController(Clone)").transform.Find("Fx_Btn").GetComponent<AudioSource>().Play();
        GameObject pom = Instantiate(Resources.Load<GameObject>("Prefabs/UI/TransparentPanel"), GameObject.Find("Canvas").transform, false);
        pom.GetComponent<SceneTransitionController>().Out("MainMenu");
    }
    private void OnDestroy()
    {
        Globals.game.Save();
    }
}
