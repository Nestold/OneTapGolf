using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathManager : MonoBehaviour
{
    [HideInInspector]
    public bool IsDrawing = false;

    public List<Transform> Path
    {
        get
        {
            if(path == null)
            {
                path = new List<Transform>();
                foreach(Transform t in transform)
                {
                    path.Add(t);
                }
            }
            return path;
        }
    }

    private List<Transform> path;

    private bool pathIsVisible => Path[0].gameObject.activeSelf;

    private Vector3 segVelocity;

    private void Update()
    {
        if (IsDrawing)
        {
            if (!pathIsVisible)
            {
                ShowPath(true);
            }
            Path[0].position = GameManager.Instance.Ball.transform.position;
            segVelocity = new Vector3(GameManager.Instance.Ball.FirePower, GameManager.Instance.Ball.FirePower, 0);
            for (int i = 1; i < Path.Count; i++)
            {
                float segTime = (segVelocity.sqrMagnitude != 0) ? .5f / segVelocity.magnitude : 0;
                segVelocity = segVelocity + Physics.gravity * segTime;
                Path[i].position = Path[i - 1].position + segVelocity * segTime;
            }
        }
        else if (!IsDrawing && pathIsVisible)
        {
            ShowPath(false);
        }
    }

    private void ShowPath(bool show)
    {
        foreach (var p in Path)
        {
            p.gameObject.SetActive(show);
        }
    }
}
