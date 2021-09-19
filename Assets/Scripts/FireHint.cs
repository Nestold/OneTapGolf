using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHint : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
        }
    }
}
