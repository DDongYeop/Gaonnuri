using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] public Transform _firstCamTarget = null;

    [SerializeField] private Vector3 _addPos;
    [SerializeField] private float _spdMouseX = 5f;
    [SerializeField] private float _spdMouseY = 5f;
    [SerializeField] private float _rotationMouseX = .0f;
    [SerializeField] private float _rotationMouseY = .0f;

    private void Start()
    {
        transform.position = _firstCamTarget.position + _addPos;
    }

    private void LateUpdate()
    {
        if (GameManager.Instance && (GameManager.Instance.CurrentGameState == GameState.PLAY || GameManager.Instance.CurrentGameState == GameState.CHANGE))
            FirstCam();
    }
    
    private void FirstCam()
    {
        float mouseX = Input.GetAxis("Mouse X");
        _rotationMouseX = transform.localEulerAngles.y + mouseX * _spdMouseX;
        _rotationMouseX = (_rotationMouseX > 180f) ? _rotationMouseX - 360f : _rotationMouseX;
        
        float mouseY = Input.GetAxis("Mouse Y") * -1;
        _rotationMouseY = transform.localEulerAngles.x + mouseY * _spdMouseY;
        _rotationMouseY = (_rotationMouseY > 180f) ? _rotationMouseY - 360f : _rotationMouseY;

        transform.localEulerAngles = new Vector3(_rotationMouseY, _rotationMouseX, 0f);
        transform.position = _firstCamTarget.position + _addPos;

        Quaternion rot = new Quaternion(0, transform.localRotation.y, 0, transform.localRotation.w);
        _firstCamTarget.localRotation = rot;
    }
}
