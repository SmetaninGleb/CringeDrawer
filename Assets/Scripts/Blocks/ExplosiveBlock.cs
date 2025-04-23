using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ExplosiveBlock : MonoBehaviour
{
    [SerializeField] private MMFeedbacks _explosionFeedback;
    [SerializeField] private SpriteRenderer _explosionFlickerSprite;
    [SerializeField] private AudioSource _fuseAudioSource;
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private float _timeToExplode = 2f;
    [SerializeField] private float _flickeringTime = 0.5f;
    
    private StartLevelButton _startLevelButton;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private CollectableFactory _collectableFactory;
    private List<ExplosionAffected> _explosionAffectedList;
    private List<ExplosionDestructive> _explosionDestructiveList;
    private bool _isExploded = false;

    public bool IsExploded => _isExploded;

    private void Awake()
    {
        _startLevelButton = FindObjectOfType<StartLevelButton>();
        _startLevelButton.OnClickedEvent += OnLevelStarted;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _collider = GetComponent<Collider2D>();
        _collectableFactory = FindObjectOfType<CollectableFactory>();
        _explosionAffectedList = FindObjectsOfType<ExplosionAffected>().ToList();
        _explosionDestructiveList = FindObjectsOfType<ExplosionDestructive>().ToList();
        _explosionFlickerSprite.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _startLevelButton.OnClickedEvent -= OnLevelStarted;
    }

    private void OnLevelStarted()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _fuseAudioSource.Play();
        StartCoroutine(WaitUntilExplosion());
    }

    private IEnumerator WaitUntilExplosion()
    {
        float _startTime = Time.timeSinceLevelLoad;
        while (_startTime + _timeToExplode > Time.timeSinceLevelLoad)
        {
            _explosionFlickerSprite.gameObject.SetActive(!_explosionFlickerSprite.gameObject.activeSelf);
            yield return new WaitForSeconds(_flickeringTime);
        }
        Explode();
    }

    private void Explode()
    {
        _explosionFlickerSprite.gameObject.SetActive(false);
        _collider.isTrigger = true;
        _isExploded = true;
        _explosionFeedback.PlayFeedbacks();
        ProcessExplosionAffected();
        ProcessExplosionDestructive();
        StartCoroutine(WaitUntilExplosionStop());
    }

    private void ProcessExplosionAffected()
    {
        foreach (ExplosionAffected affected in _explosionAffectedList)
        {
            if (Vector3.Distance(affected.transform.position, transform.position) > _explosionRadius) continue;
            affected.AffectExplosion(transform.position, _explosionForce);
        }
        foreach (Collectable collectable in _collectableFactory.Collectables)
        {
            if (Vector3.Distance(collectable.transform.position, transform.position) > _explosionRadius) continue;
            if (collectable.TryGetComponent(out ExplosionAffected affected))
            {
                affected.AffectExplosion(transform.position, _explosionForce);
            }
        }
    }

    private void ProcessExplosionDestructive()
    {
        List<ExplosionDestructive> _destructiveToRemove = new List<ExplosionDestructive>();
        foreach (ExplosionDestructive destructive in _explosionDestructiveList)
        {
            if (Vector3.Distance(destructive.transform.position, transform.position) > _explosionRadius) continue;
            destructive.Destruct();
            _destructiveToRemove.Add(destructive);
        }
        foreach (ExplosionDestructive destructive in _destructiveToRemove)
        {
            _explosionDestructiveList.Remove(destructive);
        }
    }

    private IEnumerator WaitUntilExplosionStop()
    {
        while (_explosionFeedback.IsPlaying)
        {
            yield return null;
        }
        gameObject.SetActive(false);
    }
}