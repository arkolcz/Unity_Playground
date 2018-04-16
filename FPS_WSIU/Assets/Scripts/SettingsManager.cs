/*
Made by RoXKhaar
*/

using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown antialiasingDropdown;
    public Dropdown vSyncDropdown;
    public Slider musicVolumeSlider;
    public Button saveButton;

    public AudioSource musicSource;
    public Resolution[] resolutions;
    public GameSettings gameSettings;

    void OnEnable()
    {
        string settingsFilePath = (Application.persistentDataPath + "/Settings.cfg");
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        saveButton.onClick.AddListener(delegate { OnSaveButtonClick(); });

        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        if (!File.Exists(settingsFilePath))
        {
            SetDefaultValues();            
        }
        else
        {
            try
            {
                LoadSettingsFromFile(settingsFilePath);
            }                
            catch
            {
                // If reading from file fail set default values instead.
                Debug.Log(settingsFilePath + "file data corrupted or unacessible");
                SetDefaultValues();
            }
        }
    }

    public void OnFullscreenToggle()
    {

        gameSettings.fullscreen = fullscreenToggle.isOn;
        Screen.fullScreen = gameSettings.fullscreen;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width,
                             resolutions[resolutionDropdown.value].height,
                             Screen.fullScreen);
    }

    public void OnTextureQualityChange()
    {
        gameSettings.textureQuality = textureQualityDropdown.value;
        QualitySettings.masterTextureLimit = gameSettings.textureQuality;
    }
    public void OnAntialiasingChange()
    {
        gameSettings.antialiasing = (int)Mathf.Pow(antialiasingDropdown.value, 2f);
        QualitySettings.antiAliasing = gameSettings.antialiasing;
    }
    
    public void OnVSyncChange()
    {
        gameSettings.vSync = vSyncDropdown.value;
        QualitySettings.vSyncCount = gameSettings.vSync;
    }

    public void OnMusicVolumeChange()
    {
        gameSettings.musicVolume = musicVolumeSlider.value;
        musicSource.volume = gameSettings.musicVolume;
    }

    public void OnSaveButtonClick()
    {
        SaveSettingsToFile();
    }

    public void SaveSettingsToFile()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/Settings.cfg", jsonData);  // Store File in AppData folder
    }

    public void LoadSettingsFromFile(string pathToFile)
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(pathToFile));
        musicVolumeSlider.value = gameSettings.musicVolume;
        antialiasingDropdown.value = gameSettings.antialiasing;
        vSyncDropdown.value = gameSettings.vSync;
        textureQualityDropdown.value = gameSettings.textureQuality;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        fullscreenToggle.isOn = gameSettings.fullscreen;

        resolutionDropdown.RefreshShownValue();
    }

    public void SetDefaultValues()
    {
        musicVolumeSlider.value = 0.3f;
        antialiasingDropdown.value = 0;
        vSyncDropdown.value = 0;
        textureQualityDropdown.value = 0;
        resolutionDropdown.value = 0;
        fullscreenToggle.isOn = true;
    }
}
