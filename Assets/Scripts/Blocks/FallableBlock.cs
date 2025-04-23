using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallableBlock : MonoBehaviour
{
    private StartLevelButton _startLevelButton;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _startLevelButton = FindObjectOfType<StartLevelButton>();
        _startLevelButton.OnClickedEvent += OnLevelStart;
    }

    private void OnDestroy()
    {
        _startLevelButton.OnClickedEvent -= OnLevelStart;
    }

    private void OnLevelStart()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}