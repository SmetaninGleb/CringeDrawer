using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{
    [SerializeField] private MMFeedbacks _collectFeedback;
    
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public void Collect()
    {
        _collectFeedback.PlayFeedbacks();
        StartCoroutine(CheckForDeactivate());
    }

    private IEnumerator CheckForDeactivate()
    {
        while (_collectFeedback.IsPlaying)
        {
            yield return null;
        }
        _collider.isTrigger = true;
        gameObject.SetActive(false);
    }

    public void SetKinematic(bool isKinematic)
    {
        if (isKinematic)
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        else
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    public Vector2 GetVelocity()
    {
        return _rigidbody.velocity;
    }
}