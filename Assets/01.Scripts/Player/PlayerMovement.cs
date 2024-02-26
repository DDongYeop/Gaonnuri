using UnityEngine;

public class PlayerMovement 
{
    private Transform _playerTrm;
    private PlayerController _playerController;
    private Rigidbody _rigidbody;

    public PlayerMovement(PlayerController playerControl)
    {
        _playerTrm = playerControl.transform;
        _playerController = playerControl;
        _rigidbody = playerControl.GetComponent<Rigidbody>();
    }

    public void Movement()
    {
        Vector3 originPos = new Vector3(_playerTrm.position.x, 0, _playerTrm.position.z);
        Vector3 dir = new Vector3(_playerTrm.forward.x, 0, _playerTrm.forward.z);
        _playerTrm.position += dir.normalized * (Time.deltaTime * _playerController.MoveSpeed);

        Vector3 endPos = new Vector3(_playerTrm.position.x, 0, _playerTrm.position.z);
        _playerController.MoveDistance -= Vector3.Distance(originPos, endPos);
    }

    public void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _playerController.JumpPower);
    }
}
