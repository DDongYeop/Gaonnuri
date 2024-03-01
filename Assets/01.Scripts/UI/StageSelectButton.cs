using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] private int _currentIndex;

    private void Start()
    {
        if (_currentIndex > PlayerPrefs.GetInt("MaxStage") || GameManager.Instance.MaxStage < _currentIndex)
            GetComponent<Button>().interactable = false;

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _currentIndex.ToString();
    }

    public void StageSelectButtonDown()
    {
        if (GameManager.Instance.CurrentGameState == GameState.CHANGE)
            return;

        PlayerPrefs.SetInt("CurrentStage", _currentIndex);
        ScreenTransition.Instance.SceneChange(2, GameState.CREATOR);
    }
}
