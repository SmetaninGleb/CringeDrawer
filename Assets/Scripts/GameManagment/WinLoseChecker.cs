using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class WinLoseChecker : MonoBehaviour
{
    [SerializeField] private int _numToWin;
    [SerializeField] private GameStopChecker _gameStopChecker;
    [SerializeField] private WinUI _winUI;
    [SerializeField] private LoseUI _loseUI;

    private List<CollectingArea> _collectingAreaList;
    private int _collectedNum = 0;

    private void Start()
    {
        _collectingAreaList = FindObjectsOfType<CollectingArea>().ToList();
        foreach (CollectingArea area in _collectingAreaList)
        {
            area.OnCollectedEvent += () => _collectedNum++;
        }
        _gameStopChecker.OnGameStoppedEvent += OnGameStopped;
    }

    private void OnGameStopped()
    {
        if (_collectedNum < _numToWin)
        {
            Lose();
        } else
        {
            Win();
        }
    }

    private void Lose()
    {
        _loseUI.Open();
    }

    private void Win()
    {
        _winUI.Open();
        if (SceneManager.GetActiveScene().buildIndex == YandexGame.savesData.currentLevelSceneIndex)
        {
            YandexGame.savesData.currentLevelSceneIndex++;
            YandexGame.SaveProgress();
        }
    }

    private void OnDestroy()
    {
        _gameStopChecker.OnGameStoppedEvent -= OnGameStopped;
    }
}