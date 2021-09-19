using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPanelSerializeField : MonoBehaviour
{
    public Text ScoreText => scoreText;

    public Text BestScoreText => bestScoreText;

    public Button RestartButton => restartButton;

    [SerializeField]
    private Text scoreText = null;

    [SerializeField]
    private Text bestScoreText = null;

    [SerializeField]
    private Button restartButton = null;
}
