using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField]
    private float minPosition = 0f;

    [SerializeField]
    private float maxPosition = 0f;

    public void SetRandomPosisiton()
    {
        var position = Random.Range(minPosition, maxPosition);
        transform.position = new Vector3(position, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.NextLevel();
    }
}
