using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;


    [Header("Game Settings")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private float defaultSen = 4.0f;
    public float mainControllerSensitivity = 4.0f;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null;



    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;


    [Header("levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;

    [SerializeField]
    private GameObject noSaveGameDialog = null;

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    // to load game
    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");

           // PlayerPrefs.SetString("SaveLevel",yourLevelList); //to save

            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSaveGameDialog.SetActive(true);
        }
    }

    //Exit game
    public void ExitButton()
    {
        Application.Quit();
    }


    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");  
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume",AudioListener.volume);
        //Show Prompt 
        StartCoroutine(ConfirmationBox());

    }

    public void SetControllerSen(float sensitivity)
    {
        mainControllerSensitivity = sensitivity;
        controllerSenTextValue.text = sensitivity.ToString("0.0");
      //  controllerSenSlider.value = mainControllerSensitivity;
    }

    public void GameplayApply()
    {
        if(invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
            //invert Y 
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
            // not invert
        }

        PlayerPrefs.SetFloat("mastersen", mainControllerSensitivity);
        StartCoroutine (ConfirmationBox());
    }

    public void ResetButton(string MenuType)
    {
        if(MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply(); //to save it
        }
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }
}
