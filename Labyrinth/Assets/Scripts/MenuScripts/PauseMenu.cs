using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    [SerializeField] private bool isPaused;

    // Singleton instance
    private static PauseMenu instance;

    // Property to access the singleton instance
    public static PauseMenu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PauseMenu>();

                if (instance == null)
                {
                    GameObject singleton = new GameObject("PauseMenu");
                    instance = singleton.AddComponent<PauseMenu>();
                }
            }

            return instance;
        }
    }

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
