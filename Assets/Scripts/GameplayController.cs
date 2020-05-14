using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : SceneController
{
    [SerializeField]
    private float power;
    [SerializeField]
    private int score = -1;
    [HideInInspector]
    public float powerUpValue;

    public Text scoreLabel;
    [SerializeField]
    private GameObject ball;
    public GameObject hint;
    [SerializeField]
    private bool inAir = false;
    private void Start()
    {
        power = 0.01f;
        powerUpValue = 0.05f;
        CreateFlag();
    }
    private void Update()
    {
        if (GameObject.Find("MusicController(Clone)").transform.Find("Music").GetComponent<AudioSource>().volume > 0.05f)
            GameObject.Find("MusicController(Clone)").transform.Find("Music").GetComponent<AudioSource>().volume -= 0.01f;

        scoreLabel.text = score.ToString();
        if (Input.GetKey(KeyCode.Space) && !inAir)
        {
            if (hint.activeSelf)
                hint.SetActive(false);
            power += powerUpValue;
            ball.GetComponent<BallaController>().SetFireStrength(power);
            if (power >= 8f)
                Fire();
        }

        if (Input.GetKeyUp(KeyCode.Space) && !inAir)
        {
            Fire();
        }
    }

    public void CreateFlag()
    {
        inAir = false;
        ball = Instantiate(Resources.Load<GameObject>("Prefabs/Ball"));

        var flagX = Globals.random.Next(-3, 8);

        if (GameObject.FindGameObjectsWithTag("Flag").Length != 0)
            Destroy(GameObject.FindGameObjectWithTag("Flag").gameObject);

        Instantiate(Resources.Load<GameObject>("Prefabs/Flag"), new Vector3(flagX, -0.88f), new Quaternion());

        score++;
        powerUpValue += 0.005f;
    }
    public void Fire()
    {
        GameObject.Find("MusicController(Clone)").transform.Find("Fx_Ball").GetComponent<AudioSource>().Play();
        inAir = true;
        ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(power, power), ForceMode2D.Impulse);
        ball.GetComponent<BallaController>().StartCheckEnd();
        power = 0.01f;
    }

    internal void End()
    {
        var pom = Instantiate(Resources.Load<GameObject>("Prefabs/UI/GameOverPanel"), GameObject.Find("Canvas").transform, false);
        pom.GetComponent<GameOverPanelController>().Init((uint)score, new UnityEngine.Events.UnityAction(Restart));
    }

    private void Restart()
    {
        GameObject.Find("MusicController(Clone)").transform.Find("Fx_Btn").GetComponent<AudioSource>().Play();
        Destroy(GameObject.Find("Canvas").transform.Find("GameOverPanel(Clone)").gameObject);
        powerUpValue = 0.05f;
        score = -1;
        CreateFlag();
    }
}
