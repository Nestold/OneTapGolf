using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EndPanelSerializeField))]
public class EndPanel : MonoBehaviour
{
    private EndPanelSerializeField serializeField = null;

    private void Awake()
    {
        serializeField = GetComponent<EndPanelSerializeField>();
        serializeField.RestartButton.onClick.AddListener(OnRestart);
        Hide();
    }

    public void Show()
    {
        var gMan = GameManager.Instance;
        serializeField.ScoreText.text = gMan.Score.ToString();
        serializeField.BestScoreText.text = gMan.BestScore.ToString();
        gameObject.gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.gameObject.SetActive(false);
    }

    private void OnRestart()
    {
        Hide();
        GameManager.Instance.NewGame();
    }
}
