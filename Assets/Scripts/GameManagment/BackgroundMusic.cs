using System.Collections;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic _backgroundMusic;

    private void Awake()
    {
        if (_backgroundMusic == null)
        {
            _backgroundMusic = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}