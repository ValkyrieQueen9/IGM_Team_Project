using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menus: MonoBehaviour
{
    public bool IsGamePaused;
    public GameObject[] menuGameObjs;
    string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;

        if(currentScene == "Mansion")
        {
            foreach (GameObject m in menuGameObjs)
            {
                m.SetActive(false);
            }

        }
        IsGamePaused = false;
    }

    void Update()
    {
        if (currentScene == "Mansion")
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
    }

    public void WinGame()
    {
        menuGameObjs[0].SetActive(true);//sets win menu active
    }

    public void pauseGame()
    {
        menuGameObjs[1].SetActive(true); //sets pause menu active
        IsGamePaused = true;
    }
    public void unPauseGame()
    {
        menuGameObjs[1].SetActive(false);
        IsGamePaused = false;
    }

    public void exitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }

    public void restartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void playGame()
    {
        SceneManager.LoadScene("Mansion");
    }

    public void showCredits()
    {
        menuGameObjs[2].SetActive(true);
    }
    public void closeCredits()
    {
        menuGameObjs[2].SetActive(false);
    }

}
