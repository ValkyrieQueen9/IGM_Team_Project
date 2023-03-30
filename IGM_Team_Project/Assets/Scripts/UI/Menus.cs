using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{

    public GameObject pauseMenuCanvas;
   // public GameObject winMenuCanvas;
    public static bool IsGamePaused;

    void Start()
    {
        pauseMenuCanvas.SetActive(false);
      //  winMenuCanvas.SetActive(false);
        IsGamePaused = false;
    }

    void Update()
    {
        if (IsGamePaused == false && Input.GetButtonDown("Cancel"))
        {
            pauseGame();
        }

        else if (IsGamePaused && Input.GetButtonDown("Cancel"))
        {
            unPauseGame();
        }
    }

    public void WinGame()
    {
       // winMenuCanvas.SetActive(true);
    }

    public void pauseGame()
    {
        pauseMenuCanvas.SetActive(true);
        IsGamePaused = true;
        Debug.Log("Game is paused");
        //Stop playermovement
        //Stop enemy movements
        //Stop timers if any
    }
    public void unPauseGame()
    {
        pauseMenuCanvas.SetActive(false);
        IsGamePaused = false;
        Debug.Log("Game is not paused");
        //start playermovement again
        //start enemy movements
        //start timers
    }

    public void exitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }

    public void restartRoom()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }


}
