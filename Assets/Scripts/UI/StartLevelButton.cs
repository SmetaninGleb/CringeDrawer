using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartLevelButton : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    private Button _button;

    public Action OnClickedEvent;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => OnClickedEvent?.Invoke());
        _button.onClick.AddListener(() => 
        {
            _button.interactable = false;
            _iconImage.color = _button.colors.disabledColor;
        });
    }
}