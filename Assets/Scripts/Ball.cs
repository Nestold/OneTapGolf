using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallSerializeField))]
public class Ball : MonoBehaviour
{
    public float FirePower => firePower;

    [SerializeField]
    private Vector3 startedPosition = Vector3.zero;

    [SerializeField]
    private float basePowerMultiplier = 3f;

    [SerializeField]
    private float powerStep = 0.5f;

    private BallSerializeField serializeField;

    private float firePower = 0f;

    private float currentPowerMultiplier = 3f;

    private bool inAir = false;

    private void Awake()
    {
        serializeField = GetComponent<BallSerializeField>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag.Equals("Ground") && inAir)
        {
            serializeField.Rb.velocity = Vector2.zero;
            GameManager.Instance.GameOver();
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space) && !inAir)
        {
            firePower += Time.deltaTime * currentPowerMultiplier;
            GameManager.Instance.PathManager.IsDrawing = true;
        }

        if(Input.GetKeyUp(KeyCode.Space) && !inAir)
        {
            Fire();
        }

        if(firePower >= 8f)
        {
            Fire();
        }
    }

    private void Fire()
    {
        serializeField.Rb.AddForce(new Vector2(firePower, firePower), ForceMode2D.Impulse);
        firePower = 0f;
        inAir = true;
        GameManager.Instance.PathManager.IsDrawing = false;
    }

    public void IncreasePowerMultiplier()
    {
        currentPowerMultiplier += powerStep;
        Debug.Log($"currentPowerMultiplier: {currentPowerMultiplier}");
    }

    public void SetStartPosition()
    {
        inAir = false;
        transform.position = startedPosition;
        serializeField.Rb.velocity = Vector2.zero;
    }

    public void SetDefaultFirePower()
    {
        currentPowerMultiplier = basePowerMultiplier;
    }
}
