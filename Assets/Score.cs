using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private static TextMeshProUGUI scoreTxt;
    public static int maxScore;

    private void Start()
    {
        scoreTxt = GetComponent<TextMeshProUGUI>();
    }
    public static void Increase()
    {
        maxScore += 100;
        scoreTxt.text = maxScore.ToString();
    }
}
