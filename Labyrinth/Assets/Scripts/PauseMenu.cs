using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    [SerializeField] private bool isPaused;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused =!isPaused;
        }

        if(isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactiveMenu();
        }
    }


    public void ActivateMenu()
    {
        //Pause game
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
    }

    public void DeactiveMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }
}
