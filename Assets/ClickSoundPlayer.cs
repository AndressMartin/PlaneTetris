using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSoundPlayer : MonoBehaviour
{
    private static AudioSource myAudio;
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public static void PlayCLickSound()
    {
        myAudio.Play();
    }
}
