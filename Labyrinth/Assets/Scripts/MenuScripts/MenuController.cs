using Assets.Scripts.CharacterScripts;
using Assets.Scripts.MenuScripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour, IPlayerSensitivity
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

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defailtBrightnessValue = 1.0f;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSaveGameDialog = null;

    [Header("Resolution Settings")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {

        // Register this class as an observer
      //  FirstPersonController.Instance.RegisterSensObserver(this);

        resolutions = Screen.resolutions; 
        resolutionDropdown.ClearOptions();

        //List of resolutions options
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }



    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution( resolution.width, resolution.height, Screen.fullScreen);
    }

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


    //Set volume 
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }


    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
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
        if (invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
            //invert Y 
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
            // not invert
        }

        PlayerPrefs.SetFloat("masterSen", mainControllerSensitivity);
        StartCoroutine(ConfirmationBox());
    }

    //Set up brightness
    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");

    }

    //set up Fullscreen
    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    //Set Quality
    public void SetQuality (int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    //Graphisc apply
    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness",_brightnessLevel);
        //Change your brightness with your post processing or whatever it is 

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullScreen",(_isFullScreen?1:0));
        Screen.fullScreen = _isFullScreen;
        
        StartCoroutine(ConfirmationBox());
    }


    public void ResetButton(string MenuType)
    {
        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply(); //to save it
        }

        if (MenuType == "Gameplay")
        {
            controllerSenTextValue.text = defaultSen.ToString("0.0");
            controllerSenSlider.value = defaultSen;
            mainControllerSensitivity = defaultSen;
            invertYToggle.isOn = false;
            GameplayApply();
        }

        if(MenuType == "Graphics")
        {
            //Reset brightness value
            brightnessSlider.value = defailtBrightnessValue;
            brightnessTextValue.text = defailtBrightnessValue.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }

    public void UpdateMouseSensitivity(float sensitivity)
    {
        FirstPersonController.Instance.UpdateMouseSensitivity(sensitivity);
        Debug.Log($"Mouse sensitivity updated from MenuController: {sensitivity}");
    }

}
