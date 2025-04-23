using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class RestartGameButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            YandexGame.savesData.currentLevelSceneIndex = 1;
            YandexGame.SaveProgress();
            SceneManager.LoadScene(0);
        });
    }
}