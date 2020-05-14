using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{
    string sceneName = "";
    public void In()
    {
        GetComponent<Animator>().SetBool("IsIn", true);
    }
    public void Out(string sceneName)
    {
        GetComponent<Animator>().SetBool("IsOut", true);
        this.sceneName = sceneName;
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    public void GoToScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}