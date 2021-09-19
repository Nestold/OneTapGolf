using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText = null;

    [SerializeField]
    private EndPanel endPanel = null;

    private void Awake()
    {
        GameManager.Instance.OnScoreChange -= OnScoreChange;
        GameManager.Instance.OnScoreChange += OnScoreChange;
    }

    private void OnScoreChange(int score)
    {
        scoreText.text = score.ToString();
    }

    public void ShowEndPanel(bool show)
    {
        if(show)
        {
            endPanel.Show();
        }
        else
        {
            endPanel.Hide();
        }
    }
}
