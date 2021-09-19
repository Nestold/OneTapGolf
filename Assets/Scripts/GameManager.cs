using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<int> OnScoreChange = delegate { };

    public static GameManager Instance;

    public int Score { get; private set; }

    public int BestScore { get; private set; } = 0;

    public Ball Ball => ball;

    public PathManager PathManager => pathManager;

    [SerializeField]
    private Ball ball = null;

    [SerializeField]
    private Hole hole = null;

    [SerializeField]
    private UIManager uiManager = null;

    [SerializeField]
    private PathManager pathManager = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        NewGame();
        OnScoreChange(Score);
    }

    public void NewGame()
    {
        Score = 0;
        ball.SetStartPosition();
        ball.SetDefaultFirePower();
        hole.SetRandomPosisiton();
        OnScoreChange(Score);
    }

    public void NextLevel()
    {
        Score++;
        ball.SetStartPosition();
        ball.IncreasePowerMultiplier();
        hole.SetRandomPosisiton();
        OnScoreChange(Score);
    }

    public void GameOver()
    {
        if(BestScore < Score)
        {
            BestScore = Score;
        }
        uiManager.ShowEndPanel(true);
    }
}
