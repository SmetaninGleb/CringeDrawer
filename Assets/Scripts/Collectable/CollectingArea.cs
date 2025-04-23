using MoreMountains.Feedbacks;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollectingArea : MonoBehaviour
{
    [SerializeField] private int _numOfCollected = 5;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private MMFeedbacks _collectedFeedback;

    private int _collected = 0;

    public int Collected => _collected;
    public Action OnCollectedEvent;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
        if (_text) _text.text = _collected.ToString() + "/" + _numOfCollected.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collected < _numOfCollected
            && collision.TryGetComponent(out Collectable collectable))
        {
            collectable.Collect();
            _collected++;
            OnCollectedEvent?.Invoke();
            if (_text) _text.text = _collected.ToString() + "/" + _numOfCollected.ToString();
            _collectedFeedback.PlayFeedbacks();
        }
    }
}