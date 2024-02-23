using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")]
    public int JumpCnt;
    public float MoveDistance;
    public float MoveSpeed;

    private Vector3 _startPos;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        JumpCnt = StageManager.Instance.PlayerSOs[PlayerPrefs.GetInt("CurrentStage")].JumpCnt;
        MoveDistance = StageManager.Instance.PlayerSOs[PlayerPrefs.GetInt("CurrentStage")].MoveDistance;
        MoveSpeed = StageManager.Instance.PlayerSOs[PlayerPrefs.GetInt("CurrentStage")].MoveSpeed;

        _startPos = transform.position;
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.PLAY)
            _playerMovement.Movement();
    }
}
