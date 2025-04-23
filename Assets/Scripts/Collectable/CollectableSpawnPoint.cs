using MoreMountains.Feedbacks;
using System;
using TMPro;
using UnityEngine;

public class CollectableSpawnPoint : MonoBehaviour
{
    [SerializeField] private int _collectableNumToSpawn = 5;
    [SerializeField] private TMP_Text _collectableText;
    [SerializeField] private Collectable _collectablePrefab;
    [SerializeField] private MMFeedbacks _spawnFeedback;

    private int _spawnedCollectable = 0;

    public Collectable CollectablePrefab => _collectablePrefab;
    public bool AllSpawned => _spawnedCollectable == _collectableNumToSpawn;

    private void Awake()
    {
        _collectableText.text = _collectableNumToSpawn.ToString();
    }

    public Collectable Spawn()
    {
        if (!CanSpawn())
        {
            throw new Exception("Cannot spawn collectable!");
        }

        _spawnFeedback.PlayFeedbacks();

        Collectable spawnedCollectable = Instantiate(_collectablePrefab,
            transform.position,
            transform.rotation,
            transform);

        _spawnedCollectable++;

        _collectableText.text = (_collectableNumToSpawn - _spawnedCollectable).ToString();

        return spawnedCollectable;
    }

    public bool CanSpawn()
    {
        return _spawnedCollectable < _collectableNumToSpawn;
    }
}