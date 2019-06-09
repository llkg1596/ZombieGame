using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTime : MonoBehaviour
{
    public static float deltaTime = 0f;
    public static float timeScale = 1f;

    private void Update()
    {
        deltaTime = Time.deltaTime * timeScale;
    }
}
