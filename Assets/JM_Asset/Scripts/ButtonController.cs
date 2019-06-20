using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    //public UnityEngine.UI.Image fade;
    //float fades = 1.0f;
    //float time = 0;

    public void OnClickExit()
    {

        Application.Quit();
        Debug.Log("End the Game");

    }

    public void ChangeFirstScene()
    {
        SceneManager.LoadScene("Intro_Main_JM");
    }

    public void ChangeSecondScene()
    {
        SceneManager.LoadScene("Intro_1_JM");
    }
}
