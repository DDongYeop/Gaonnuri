using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ArrivalPoint"))
            GameManager.Instance.OnGameClear();
        if (other.CompareTag("Gameover"))
            GameManager.Instance.OnGameFail();
    }
}
