using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LoadPrefs : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MenuController menuController;

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Brightness Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;


    [Header("Quality Level Settings")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("FullScreen Settings")]
    [SerializeField] private Toggle fullScreenToggle;

    [Header("Sensitivity Settings")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;

    [Header("Invert Y Settings")]
    [SerializeField] private Toggle invertYToggle = null;

    private void Awake()
    {
        if (canUse)
        {
            if (PlayerPrefs.HasKey("masterVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("masterVolume");

                volumeTextValue.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else
            {
                menuController.ResetButton("Audio");
            }

            if (PlayerPrefs.HasKey("masterQuality"))
            {
                int localQuality = PlayerPrefs.GetInt("masterQuality");
                qualityDropdown.value = localQuality;
                QualitySettings.SetQualityLevel(localQuality);
            }

            if (PlayerPrefs.HasKey("masterFullScreen"))
            {
                int localFullScreen = PlayerPrefs.GetInt("masterFullScreen");

                if (localFullScreen == 1)
                {
                    Screen.fullScreen = true;
                    fullScreenToggle.isOn = true;
                }
                else
                {
                    Screen.fullScreen = false;
                    fullScreenToggle.isOn = false;
                }
            }

            if (PlayerPrefs.HasKey("masterBrightness"))
            {
                float lovalBrightness = PlayerPrefs.GetFloat("masterBrightness");
                brightnessTextValue.text = lovalBrightness.ToString("0.0");
                brightnessSlider.value = lovalBrightness;
                //Chnge the brightness
            }

            if (PlayerPrefs.HasKey("masterSen"))
            {
                float localSensitivity = PlayerPrefs.GetFloat("masterSen");

                controllerSenTextValue.text = localSensitivity.ToString("0.0");
                controllerSenSlider.value = localSensitivity;
                menuController.mainControllerSensitivity = localSensitivity;
            }

            if (PlayerPrefs.HasKey("masterInvertY"))
            {
                if (PlayerPrefs.GetInt("masterInvertY") == 1)
                {
                    invertYToggle.isOn = true;
                }
                else
                {
                    invertYToggle.isOn = false;
                }
            }
        }
    }
}

// Interface for all setting strategies
//using TMPro;
//using UnityEngine;

//public interface ISettingStrategy
//{
//    void LoadSetting();
//}

//// Volume Setting Strategy
//public class VolumeSettingStrategy : ISettingStrategy
//{
//    private TMP_Text volumeTextValue;
//    private Slider volumeSlider;

//    public VolumeSettingStrategy(TMP_Text textValue, Slider slider)
//    {
//        volumeTextValue = textValue;
//        volumeSlider = slider;
//    }

//    public void LoadSetting()
//    {
//        if (PlayerPrefs.HasKey("masterVolume"))
//        {
//            float localVolume = PlayerPrefs.GetFloat("masterVolume");
//            volumeTextValue.text = localVolume.ToString("0.0");
//            volumeSlider.value = localVolume;
//            AudioListener.volume = localVolume;
//        }
//        else
//        {
//            // Handle default behavior or notify
//        }
//    }
//}

//// Quality Setting Strategy
//public class QualitySettingStrategy : ISettingStrategy
//{
//    private TMP_Dropdown qualityDropdown;

//    public QualitySettingStrategy(TMP_Dropdown dropdown)
//    {
//        qualityDropdown = dropdown;
//    }

//    public void LoadSetting()
//    {
//        if (PlayerPrefs.HasKey("masterQuality"))
//        {
//            int localQuality = PlayerPrefs.GetInt("masterQuality");
//            qualityDropdown.value = localQuality;
//            QualitySettings.SetQualityLevel(localQuality);
//        }
//        // Handle default behavior or notify
//    }
//}

//// FullScreen Setting Strategy
//public class FullScreenSettingStrategy : ISettingStrategy
//{
//    private Toggle fullScreenToggle;

//    public FullScreenSettingStrategy(Toggle toggle)
//    {
//        fullScreenToggle = toggle;
//    }

//    public void LoadSetting()
//    {
//        if (PlayerPrefs.HasKey("masterFullScreen"))
//        {
//            int localFullScreen = PlayerPrefs.GetInt("masterFullScreen");
//            Screen.fullScreen = (localFullScreen == 1);
//            fullScreenToggle.isOn = (localFullScreen == 1);
//        }
//        // Handle default behavior or notify
//    }
//}

//// Brightness Setting Strategy
//public class BrightnessSettingStrategy : ISettingStrategy
//{
//    private TMP_Text brightnessTextValue;
//    private Slider brightnessSlider;

//    public BrightnessSettingStrategy(TMP_Text textValue, Slider slider)
//    {
//        brightnessTextValue = textValue;
//        brightnessSlider = slider;
//    }

//    public void LoadSetting()
//    {
//        if (PlayerPrefs.HasKey("masterBrightness"))
//        {
//            float localBrightness = PlayerPrefs.GetFloat("masterBrightness");
//            brightnessTextValue.text = localBrightness.ToString("0.0");
//            brightnessSlider.value = localBrightness;
//            // Change brightness logic
//        }
//        // Handle default behavior or notify
//    }
//}

//// Sensitivity Setting Strategy
//public class SensitivitySettingStrategy : ISettingStrategy
//{
//    private TMP_Text controllerSenTextValue;
//    private Slider controllerSenSlider;
//    private MenuController menuController;

//    public SensitivitySettingStrategy(TMP_Text textValue, Slider slider, MenuController controller)
//    {
//        controllerSenTextValue = textValue;
//        controllerSenSlider = slider;
//        menuController = controller;
//    }

//    public void LoadSetting()
//    {
//        if (PlayerPrefs.HasKey("masterSen"))
//        {
//            float localSensitivity = PlayerPrefs.GetFloat("masterSen");
//            controllerSenTextValue.text = localSensitivity.ToString("0.0");
//            controllerSenSlider.value = localSensitivity;
//            menuController.mainControllerSensitivity = localSensitivity;
//        }
//        // Handle default behavior or notify
//    }
//}

//// Invert Y Setting Strategy
//public class InvertYSettingStrategy : ISettingStrategy
//{
//    private Toggle invertYToggle;

//    public InvertYSettingStrategy(Toggle toggle)
//    {
//        invertYToggle = toggle;
//    }

//    public void LoadSetting()
//    {
//        if (PlayerPrefs.HasKey("masterInvertY"))
//        {
//            invertYToggle.isOn = (PlayerPrefs.GetInt("masterInvertY") == 1);
//        }
//        // Handle default behavior or notify
//    }
//}

//// Context class that will use a specific strategy
//public class SettingContext
//{
//    private ISettingStrategy settingStrategy;

//    public SettingContext(ISettingStrategy strategy)
//    {
//        settingStrategy = strategy;
//    }

//    public void LoadSetting()
//    {
//        settingStrategy.LoadSetting();
//    }
//}

//public class LoadPrefs : MonoBehaviour
//{
//    [Header("General Settings")]
//    [SerializeField] private bool canUse = false;
//    [SerializeField] private MenuController menuController;

//    [Header("Volume Settings")]
//    [SerializeField] private TMP_Text volumeTextValue = null;
//    [SerializeField] private Slider volumeSlider = null;

//    [Header("Brightness Settings")]
//    [SerializeField] private Slider brightnessSlider = null;
//    [SerializeField] private TMP_Text brightnessTextValue = null;

//    [Header("Quality Level Settings")]
//    [SerializeField] private TMP_Dropdown qualityDropdown;

//    [Header("FullScreen Settings")]
//    [SerializeField] private Toggle fullScreenToggle;

//    [Header("Sensitivity Settings")]
//    [SerializeField] private TMP_Text controllerSenTextValue = null;
//    [SerializeField] private Slider controllerSenSlider = null;

//    [Header("Invert Y Settings")]
//    [SerializeField] private Toggle invertYToggle = null;

//    private void Awake()
//    {
//        if (canUse)
//        {
//            // Create instances of the strategies
//            var volumeStrategy = new VolumeSettingStrategy(volumeTextValue, volumeSlider);
//            var qualityStrategy = new QualitySettingStrategy(qualityDropdown);
//            var fullScreenStrategy = new FullScreenSettingStrategy(fullScreenToggle);
//            var brightnessStrategy = new BrightnessSettingStrategy(brightnessTextValue, brightnessSlider);
//            var sensitivityStrategy = new SensitivitySettingStrategy(controllerSenTextValue, controllerSenSlider, menuController);
//            var invertYStrategy = new InvertYSettingStrategy(invertYToggle);

//            // Use the context to load each setting
//            var volumeContext = new SettingContext(volumeStrategy);
//            volumeContext.LoadSetting();

//            var qualityContext = new SettingContext(qualityStrategy);
//            qualityContext.LoadSetting();

//            var fullScreenContext = new SettingContext(fullScreenStrategy);
//            fullScreenContext.LoadSetting();

//            var brightnessContext = new SettingContext(brightnessStrategy);
//            brightnessContext.LoadSetting();

//            var sensitivityContext = new SettingContext(sensitivityStrategy);
//            sensitivityContext.LoadSetting();

//            var invertYContext = new SettingContext(invertYStrategy);
//            invertYContext.LoadSetting();
//        }
//    }
//}
