using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    [HideInInspector] public int JumpCnt;
    [HideInInspector] public float MoveDistance;
    [HideInInspector] public float MoveSpeed;
    [HideInInspector] public float JumpPower;

    [Header("Jump")] 
    public LayerMask WhatIsArea;
    
    [Header("Early")] 
    private Vector3 _earlyPos;
    private Quaternion _earlyRot;
    
    private Vector3 _startPos;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = new PlayerMovement(this);
    }

    private void Start()
    {
        _earlyPos = transform.position;
        _earlyRot = transform.rotation;
        
        ChangeEarly();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.PLAY)
            _playerMovement.Movement();
        
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        #endif
    }

    public void ChangeEarly()
    {
        JumpCnt = StageManager.Instance.PlayerSOs[PlayerPrefs.GetInt("CurrentStage")].JumpCnt;
        MoveDistance = StageManager.Instance.PlayerSOs[PlayerPrefs.GetInt("CurrentStage")].MoveDistance;
        MoveSpeed = StageManager.Instance.PlayerSOs[PlayerPrefs.GetInt("CurrentStage")].MoveSpeed;
        JumpPower = StageManager.Instance.PlayerSOs[PlayerPrefs.GetInt("CurrentStage")].JumpPower;
        
        transform.position = _earlyPos;
        transform.rotation = _earlyRot;
    }

    public void Jump()
    {
        if (JumpCnt <= 0)
            return;
        _playerMovement.Jump();
    }

    #if UNITY_EDITOR
    
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(0f, -0.1f, 0), new Vector3(.75f, .25f, 0.75f));
    }
    
    #endif
}
