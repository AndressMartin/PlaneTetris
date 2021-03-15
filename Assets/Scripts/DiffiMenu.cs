using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiffiMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Explanation;

    public void CallEasyScene()
    {
        SceneManager.LoadScene("PlaneTetris");
        GameManager.UIBlockEnabled = true;
        GameManager.ShadowBlockEnabled = true;
        GameManager.dropTime = 0.75f;
        GameManager.quickDropTime = 0.04f;
    }
    public void CallMediumScene()
    {
        SceneManager.LoadScene("PlaneTetris");
        GameManager.UIBlockEnabled = false;
        GameManager.ShadowBlockEnabled = true;
        GameManager.dropTime = 0.6f;
        GameManager.quickDropTime = 0.03f;
    }
    public void CallHardScene()
    {
        SceneManager.LoadScene("PlaneTetris");
        GameManager.UIBlockEnabled = false;
        GameManager.ShadowBlockEnabled = false;
        GameManager.dropTime = 0.4f;
        GameManager.quickDropTime = 0.02f;
    }
    public void EasyExplanation()
    {
        Explanation.text = "Hints on next piece.\nShows where pieces land.\nSlow speed.";
    }
    public void MediumExplanation()
    {
        Explanation.text = "Shows where pieces land.\nMedium speed.\nDefault Experience.";
    }
    public void HardExplanation()
    {
        Explanation.text = "No hints.\nFast speed.\nFor experienced players.";
    }
}
