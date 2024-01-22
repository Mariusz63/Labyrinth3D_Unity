using Assets.Scripts.MenuScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;
    [SerializeField] private FirstPersonController firstPersonController;

    private static PauseMenu instance;

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
            ICommand pauseCommand = new PauseCommand(this);
            pauseCommand.Execute();
        }

    }

    public void OnClickResume()
    {
        TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
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
        Time.timeScale = 0;
        firstPersonController.ToggleCameraLook(false);
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
    }

    public void DeactiveMenu()
    {
        Time.timeScale = 1;
        firstPersonController.ToggleCameraLook(true);
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }
}
