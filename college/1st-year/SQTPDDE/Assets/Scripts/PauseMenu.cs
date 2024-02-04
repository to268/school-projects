using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool gameIsPaused;
    public GameObject pauseMenuUI;
    public TextMeshProUGUI tPoint;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadTitle()
    {
        Debug.Log("Loading menu");
    }
    
    public void Quit()
    {
        Debug.Log("Quit");
    }

    public void UpdateBoneCount(int count)
    {
        tPoint.text = "x" + count;
    }
}
