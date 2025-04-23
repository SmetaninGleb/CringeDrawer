using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CollectableFactory : MonoBehaviour
{
    [SerializeField] private float _spawnRate = 0.5f;
    
    private List<Collectable> _collectables = new List<Collectable>();
    private List<CollectableSpawnPoint> _spawnPoints = new List<CollectableSpawnPoint>();
    private StartLevelButton _startLevelButton;

    public List<Collectable> Collectables => _collectables;
    public List<CollectableSpawnPoint> SpawnPoints => _spawnPoints;

    private void Start()
    {
        _spawnPoints = FindObjectsOfType<CollectableSpawnPoint>().ToList();
        _startLevelButton = FindObjectOfType<StartLevelButton>();
        _startLevelButton.OnClickedEvent += OnLevelStarted;
    }

    private void OnDestroy()
    {
        _startLevelButton.OnClickedEvent -= OnLevelStarted;
    }

    private void OnLevelStarted()
    {
        SpawnAll();
    }

    public void SpawnAll()
    {
        StartCoroutine(SpawnWithTime());
    }

    private IEnumerator SpawnWithTime()
    {
        bool shouldSpawn = true;
        while (shouldSpawn)
        {
            shouldSpawn = false;
            foreach (CollectableSpawnPoint point in _spawnPoints)
            {
                if (!point.CanSpawn()) continue;

                shouldSpawn = true;

                Collectable spawnedCollectable = point.Spawn();

                spawnedCollectable.SetKinematic(false);
                _collectables.Add(spawnedCollectable);
            }
            yield return new WaitForSeconds(_spawnRate);
        }
    }
}