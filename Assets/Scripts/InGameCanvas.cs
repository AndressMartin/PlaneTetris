using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameCanvas : MonoBehaviour
{
    public GameObject InGameHud;
    public GameObject LostHud;
    public TextMeshProUGUI NextBlockText;
    private void Start()
    {
        if (!GameManager.UIBlockEnabled) NextBlockText.enabled = false;
    }
    private void Update()
    {
        if (GameManager.Lost == true)
        {
            ShowLostHUD();
            HideInGameHUD();
        }
    }


    // Start is called before the first frame update
    public void CallMenuScene()
    {
        GameManager.Lost = false;
        SceneManager.LoadScene("MainMenu");
    }
    private void ShowLostHUD()
    {
        LostHud.SetActive(true);
    }
    private void HideInGameHUD()
    {
        InGameHud.SetActive(false);
    }
}
