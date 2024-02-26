using UnityEngine;

[CreateAssetMenu (menuName = "SO/Player/PlayerData", fileName = "PlayerData")]
public class PlayerSO : ScriptableObject
{
    public int JumpCnt;
    public float MoveDistance;
    public float MoveSpeed;
    public float JumpPower;
}
