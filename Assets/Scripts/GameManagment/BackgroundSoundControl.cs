using Agava.WebUtility;
using System.Collections;
using UnityEngine;
using YG;

public class BackgroundSoundControl : MonoBehaviour
{
    
    private void Awake()
    {
        WebApplication.InBackgroundChangeEvent += OnBackgroundChanged;
    }

    private void OnDestroy()
    {
        WebApplication.InBackgroundChangeEvent -= OnBackgroundChanged;
    }

    private void OnBackgroundChanged(bool isBackground)
    {
        if (isBackground)
        {
            Debug.Log("Background Turn Off Sound");
            TurnOffSound();
        }
        else
        {
            Debug.Log("Background Turn On Sound");
            TurnOnSound();
        }
    }

    private void TurnOnSound()
    {
        AudioListener.pause = false;
        AudioListener.volume = 1f;
    }

    private void TurnOffSound()
    {
        AudioListener.pause = true;
        AudioListener.volume = 0f;
    }
}