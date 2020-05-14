using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManu_Background : MonoBehaviour {
    [SerializeField]
    private float scrollSpeed = -2f;
    [SerializeField]
    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        transform.position = transform.position + Vector3.left * Time.deltaTime * scrollSpeed;
        if (transform.position.x >= 0.001f)
            transform.position = startPos;
    }
}
