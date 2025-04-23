using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Line : MonoBehaviour
{
    [SerializeField] private float _resolution = 0.1f;
    
    private LineRenderer _renderer;
    private EdgeCollider2D _collider;

    private List<Vector2> _pointsCoors = new List<Vector2>();

    void Awake()
    {
        _renderer = GetComponent<LineRenderer>();
        _renderer.positionCount = 0;
        _collider = GetComponent<EdgeCollider2D>();
        _collider.points = _pointsCoors.ToArray();
    }

    public void AddPoint(Vector2 pos)
    {
        if (!CanAddPoint(pos)) return;
        _pointsCoors.Add(pos);
        
        _collider.points = _pointsCoors.ToArray();
        if (_pointsCoors.Count <= 1) _collider.isTrigger = true;
        else _collider.isTrigger = false;

        _renderer.positionCount++;
        _renderer.SetPosition(_renderer.positionCount-1, pos);
    }

    private bool CanAddPoint(Vector2 pos)
    {
        if (_pointsCoors.Count == 0) return true;

        return Vector2.Distance(_pointsCoors[_pointsCoors.Count - 1], pos) >= _resolution;
    }
}