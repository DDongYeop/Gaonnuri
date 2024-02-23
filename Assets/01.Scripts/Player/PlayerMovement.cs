using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    public void Movement()
    {
        Vector3 originPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 dir = new Vector3(transform.forward.x, 0, transform.forward.z);
        transform.position += dir.normalized * (Time.deltaTime * _playerController.MoveSpeed);

        Vector3 endPos = new Vector3(transform.position.x, 0, transform.position.z);
        _playerController.MoveDistance -= Vector3.Distance(originPos, endPos);
    }
}
