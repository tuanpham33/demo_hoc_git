using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using System.Text.RegularExpressions;

public class MenuManager : MonoBehaviour
{
    protected int Moneyyy;
    public GameObject mainMenu;
    public GameObject menuNewGame;
    public GameObject menuLoadGame;
    public GameObject menuSettings;
    public TMP_InputField inputField;
    protected string textTotalMoneyyy;

    public GameObject cho1000;
    public GameObject nhapNhieuThe;


    private void Awake()
    {
        Moneyyy = PlayerPrefs.GetInt("TotalMoneyHaved");

        inputField.onEndEdit.AddListener(OnEndEdit);
    }

    public void LoadNewGame()
    {
        PlayerPrefs.SetInt("TotalMoneyHaved", Moneyyy);
        SceneManager.LoadScene(2);
    }

    public void LoadMenuNewGame()
    {
        Debug.Log("NewGame");
        mainMenu.SetActive(false);
        menuNewGame.SetActive(true);
    }

    public void Back2MMFromLNG()
    {
        menuNewGame.SetActive(false);
        mainMenu.SetActive(true);

    }

    public void GoToGame()
    {

        if (Regex.IsMatch(textTotalMoneyyy, @"^\d+$"))
        {
            Moneyyy = int.Parse(textTotalMoneyyy);
            if (Moneyyy < 0)
            {
                cho1000.SetActive(true);
                Moneyyy = 1000;
                Invoke("SetFalsecho1000", 3f);
                Invoke("LoadNewGame", 3f);
            }
            else
            {
                if (Moneyyy > 1000)
                {
                    Moneyyy = 1000;
                    nhapNhieuThe.SetActive(true);
                    Invoke("SetFalsenhapNhieuThe", 3f);
                    Invoke("LoadNewGame", 3f);
                }
                else
                {
                    LoadNewGame();
                }
            }
        }
        else
        {
            Debug.Log("Player Đã nhập chữ");
            cho1000.SetActive(true);
            Moneyyy = 1000;
            Invoke("SetFalsecho1000", 3f);
            Invoke("LoadNewGame", 3f);
        }
    }

    private void OnEndEdit(string text)
    {
        // Lưu dữ liệu khi người dùng nhập xong
        textTotalMoneyyy = text;
        Debug.Log("onEndEdit");
    }

    private void SetFalsecho1000()
    {
        cho1000.SetActive(false);
    }

    private void SetFalsenhapNhieuThe()
    {
        nhapNhieuThe.SetActive(false);
    }

    // MenuLoadGame
    public void LoadMenuLoadGame()
    {
        mainMenu.SetActive(false);
        menuLoadGame.SetActive(true);

    }

    public void Back2MMFromMLG()
    {
        menuLoadGame.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void LoadHome()
    {
        PlayerPrefs.SetInt("TotalMoneyHaved", Moneyyy);
        SceneManager.LoadScene(1);
    }

    // Menu Settings

    public void LoadMenuSettings()
    {
        mainMenu.SetActive(false);
        menuSettings.SetActive(true);

    }
    public void Back2MMFromMS()
    {
        menuSettings.SetActive(false);
        mainMenu.SetActive(true);
    }

    // Quit App
    public void QuitGame()
    {
        Application.Quit();
    }
}
