using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Slider SFXSlider = null;
    [SerializeField] private TextMeshProUGUI textMusicUI;
    [SerializeField] private TextMeshProUGUI textSFXUI;

    public GameObject menuSettings;
    public GameObject mainMenu;
    protected float SettingsMusicBef;
    protected float SettingsSFXBef;

    private void Start()
    {
        LoadMusicValue();
    }
    public void SetMusicSlider(float MusicValue)
    {
        textMusicUI.text = MusicValue.ToString("N0");
    }

    public void SetSFXSlider(float SFXValue)
    {
        textSFXUI.text = SFXValue.ToString("N0");
    }

    public void SaveSettingsMusic()
    {
        float musicValue = musicSlider.value;
        float sFXValue = SFXSlider.value;
        PlayerPrefs.SetFloat("MusicValue", musicValue);
        PlayerPrefs.SetFloat("SFXValue", sFXValue);
        LoadMusicValue();
        menuSettings.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void LoadMusicValue()
    {
        float musicValue = PlayerPrefs.GetFloat("MusicValue");
        float sFXValue = PlayerPrefs.GetFloat("SFXValue");
        musicSlider.value = musicValue;
        SFXSlider.value = sFXValue;
        AudioListener.volume = musicValue;
    }

    public void BackToMainMenu()
    {
        musicSlider.value = SettingsMusicBef;
        SFXSlider.value = SettingsSFXBef;
        menuSettings.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SettingBef()
    {
        SettingsMusicBef = PlayerPrefs.GetFloat("MusicValue");
        SettingsSFXBef = PlayerPrefs.GetFloat("SFXValue");
    }
}
