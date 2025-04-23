using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private Line _linePrefab;

    private Camera _camera;
    private Line _currentLine;
    private bool _isLevelStarted = false;

    private void Start()
    {
        _camera = Camera.main;
        FindObjectOfType<StartLevelButton>().OnClickedEvent += () => _isLevelStarted = true;
    }

    private void Update()
    {
        if (_isLevelStarted) return;

        if (Input.GetMouseButtonDown(0))
        {
            _currentLine = Instantiate(_linePrefab, Vector2.zero, Quaternion.identity);
        }

        if (Input.GetMouseButton(0))
        {
            _currentLine.AddPoint(GetMouseWorldPos());
        }
    }

    private Vector2 GetMouseWorldPos()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        return mousePos;
    }
}
