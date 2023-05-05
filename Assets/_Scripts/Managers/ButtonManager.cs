using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
