using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStopChecker : MonoBehaviour
{
    [SerializeField] private float _timeBetweenWinChecks = 3f;
    [SerializeField] private CollectableFactory _collectableFactory;
    [SerializeField] private StartLevelButton _startLevelButton;

    private bool _isLevelStarted = false;
    private List<ExplosiveBlock> _explosiveList;

    public Action OnGameStoppedEvent;

    public void Start()
    {
        _startLevelButton.OnClickedEvent += () => _isLevelStarted = true;
        _explosiveList = FindObjectsOfType<ExplosiveBlock>().ToList();
        StartCoroutine(StopChecker());
    }

    private IEnumerator StopChecker()
    {
        while (!CheckForGameProcessStoped())
        {
            yield return new WaitForSeconds(_timeBetweenWinChecks);
        }
        ProcessGameStopping();
    }

    private bool CheckForGameProcessStoped()
    {
        if (!_isLevelStarted) return false;

        foreach (CollectableSpawnPoint spawnPoint in _collectableFactory.SpawnPoints)
        {
            if (!spawnPoint.AllSpawned) return false;
        }
        foreach (ExplosiveBlock explosive in _explosiveList)
        {
            if (!explosive.IsExploded) return false;
        }
        foreach (Collectable collectable in _collectableFactory.Collectables)
        {
            if (collectable.GetVelocity().magnitude > .1f)
            {
                return false;
            }
        }
        return true;
    }

    private void ProcessGameStopping()
    {
        OnGameStoppedEvent?.Invoke();
    }
}