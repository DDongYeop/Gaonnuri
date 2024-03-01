using System;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    [SerializeField] private int _currentIndex;

    private void Start()
    {
        if (_currentIndex > PlayerPrefs.GetInt("MaxStage"))
            GetComponent<Button>().interactable = false;
    }

    public void StageSelectButtonDown()
    {
        if (GameManager.Instance.CurrentGameState == GameState.CHANGE)
            return;

        PlayerPrefs.SetInt("CurrentStage", _currentIndex);
        ScreenTransition.Instance.SceneChange(2, GameState.CREATOR);
    }
}
