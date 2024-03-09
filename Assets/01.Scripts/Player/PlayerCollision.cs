using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Audio Clip")]
    [SerializeField] private AudioClip _gameClearClip;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ArrivalPoint"))
        {
            GameManager.Instance.OnGameClear();
            
            AudioPlayer audioPlayer = PoolManager.Instance.Pop("AudioPlayer") as AudioPlayer;
            audioPlayer.Setup(_gameClearClip);
        }
        if (other.CompareTag("Gameover"))
            GameManager.Instance.OnGameFail();
    }
}
