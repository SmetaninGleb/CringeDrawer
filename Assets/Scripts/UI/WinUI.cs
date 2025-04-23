using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    [SerializeField] private MMFeedbacks _openFeedback;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        _openFeedback.PlayFeedbacks();
    }
}