using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ExplosionDestructive : MonoBehaviour
{
    [SerializeField] private MMFeedbacks _destructFeedback;
    [SerializeField] private GameObject _view;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void Destruct()
    {
        _collider.isTrigger = true;
        _view.SetActive(false);
        _destructFeedback.PlayFeedbacks();
    }
}