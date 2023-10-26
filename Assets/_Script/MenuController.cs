using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuInGame;

    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Slider SFXSlider = null;
    [SerializeField] private TextMeshProUGUI textMusicUI;
    [SerializeField] private TextMeshProUGUI textSFXUI;

    public GameObject mainMenu;
    protected float SettingsMusicBef;
    protected float SettingsSFXBef;

    private void Start()
    {
        LoadMusicValue();
    }

    public void OpenMenuInGame()
    {
        menuInGame.SetActive(true);
        SettingsMusicBef = PlayerPrefs.GetFloat("MusicValue");
        SettingsSFXBef = PlayerPrefs.GetFloat("SFXValue");
        Time.timeScale = 0f;
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
        menuInGame.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMusicValue()
    {
        float musicValue = PlayerPrefs.GetFloat("MusicValue");
        float sFXValue = PlayerPrefs.GetFloat("SFXValue");
        musicSlider.value = musicValue;
        SFXSlider.value = sFXValue;
        AudioListener.volume = musicValue;
    }

    public void BackToGame()
    {
        musicSlider.value = SettingsMusicBef;
        SFXSlider.value = SettingsSFXBef;
        menuInGame.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SettingBef()
    {
        SettingsMusicBef = PlayerPrefs.GetFloat("MusicValue");
        SettingsSFXBef = PlayerPrefs.GetFloat("SFXValue");
    }

    public void Back2Menu()
    {
        float musicValue = musicSlider.value;
        float sFXValue = SFXSlider.value;
        PlayerPrefs.SetFloat("MusicValue", musicValue);
        PlayerPrefs.SetFloat("SFXValue", sFXValue);
        LoadMusicValue();
        AudioListener.volume = musicValue;
        SceneManager.LoadScene(0);
    }

    public void SaveGameToQuit()
    {
        float musicValue = musicSlider.value;
        float sFXValue = SFXSlider.value;
        PlayerPrefs.SetFloat("MusicValue", musicValue);
        PlayerPrefs.SetFloat("SFXValue", sFXValue);
        LoadMusicValue();
        Application.Quit();
    }
}
