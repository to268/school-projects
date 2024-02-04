using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadHomeScene()
    {
        SceneManager.LoadScene("Home");
    }
    
    public static void LoadTitleScreenScene()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public static void LoadDeathScene()
    {
        SceneManager.LoadScene("Death");
    }
    
    public static void LoadwinScene()
    {
        SceneManager.LoadScene("Win");
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
