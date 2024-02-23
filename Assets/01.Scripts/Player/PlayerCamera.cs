using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] public Transform _firstCamTarget = null;

    [SerializeField] private float _spdMouseX = 5f;
    [SerializeField] private float _spdMouseY = 5f;
    [SerializeField] private float _rotationMouseX = .0f;
    [SerializeField] private float _rotationMouseY = .0f;

    private void LateUpdate()
    {
        if (GameManager.Instance.CurrentGameState == GameState.PLAY)
            FirstCam();
    }
    
    private void FirstCam()
    {
        float mouseX = Input.GetAxis("Mouse X");
        _rotationMouseX = transform.localEulerAngles.y + mouseX * _spdMouseX;
        _rotationMouseX = (_rotationMouseX > 180f) ? _rotationMouseX - 360f : _rotationMouseX;
        
        float mouseY = Input.GetAxis("Mouse Y");
        _rotationMouseY = transform.localEulerAngles.x + mouseY * _spdMouseY;
        _rotationMouseY = (_rotationMouseY > 180f) ? _rotationMouseY - 360f : _rotationMouseY;

        transform.localEulerAngles = new Vector3(_rotationMouseY, _rotationMouseX, 0f);
        _firstCamTarget.localRotation = transform.localRotation;
        transform.position = _firstCamTarget.position;
    }
}
