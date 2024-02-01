using System;
using UnityEngine;

public class AreaCreator : MonoBehaviour
{
    private Vector2 _currentPosition;

    private void Update()
    {
        _currentPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            -Camera.main.transform.position.z));

        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }
}
