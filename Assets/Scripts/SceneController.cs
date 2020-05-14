using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {
    private void Awake()
    {
        GameObject pom = Instantiate(Resources.Load<GameObject>("Prefabs/UI/TransparentPanel"), GameObject.Find("Canvas").transform, false);
        pom.GetComponent<SceneTransitionController>().In();
    }
}
