using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    [HideInInspector] public int JumpCnt;
    [HideInInspector] public float MoveDistance;
    [HideInInspector] public float MoveSpeed;
    [HideInInspector] public float JumpPower;

    private Vector3 _startPos;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = new PlayerMovement(this);
    }

    private void Start()
    {
        JumpCnt = StageManager.Instance.PlayerSOs[PlayerPrefs.GetInt("CurrentStage")].JumpCnt;
        MoveDistance = StageManager.Instance.PlayerSOs[PlayerPrefs.GetInt("CurrentStage")].MoveDistance;
        MoveSpeed = StageManager.Instance.PlayerSOs[PlayerPrefs.GetInt("CurrentStage")].MoveSpeed;
        JumpPower = StageManager.Instance.PlayerSOs[PlayerPrefs.GetInt("CurrentStage")].JumpPower;

        _startPos = transform.position;
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

    public void Jump()
    {
        if (JumpCnt <= 0)
            return;
        JumpCnt -= 1;
        _playerMovement.Jump();
    }
}
