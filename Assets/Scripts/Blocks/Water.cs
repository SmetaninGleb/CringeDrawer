using System.Collections;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private Transform _view;
    [SerializeField] private float _waterSpeed;

    private void Update()
    {
        float currentPosX = _view.position.x;
        float newPosX = (currentPosX + _waterSpeed * Time.deltaTime);
        while (newPosX > 1)
        { 
            newPosX -= 1;
        }
        while (newPosX < -1)
        {
            newPosX += 1;
        }
        _view.position = new Vector3(newPosX, _view.position.y, _view.position.z);
    }
}