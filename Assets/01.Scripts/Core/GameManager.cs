using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private GameState _currentGameState = GameState.WAIT;

    public GameState CurrentGameState => _currentGameState;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _currentGameState = GameState.WAIT;
    }

    private void OnEnable()
    {
        CameraSetting();
    }

    public void StateChange(GameState state, float duration)
    {
        StartCoroutine(StateChangeCo(state, duration));
    }

    private IEnumerator StateChangeCo(GameState state, float duration)
    {
        _currentGameState = GameState.CHANGE;
        yield return new WaitForSecondsRealtime(duration);
        _currentGameState = state;
    }

    #region Clear&Fail

    public void OnGameClear()
    {
        PlayerPrefs.SetInt("MaxStage", PlayerPrefs.GetInt("CurrentStage") + 1);
    }

    public void OnGameFail()
    {
        UIManager.Instance.RePlay();
    }

    #endregion

    #region Setting

    private void CameraSetting()
    {
        Camera[] cameras = FindObjectsOfType<Camera>();
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)2 / 1); // (가로 / 세로)
        float scalewidth = 1f / scaleheight;
        
        foreach (var camera in cameras)
        {
            Rect rect = camera.rect;
            if (scaleheight < 1)
            {
                rect.height = scaleheight;
                rect.y = (1f - scaleheight) / 2f;
            }
            else
            {
                rect.width = scalewidth;
                rect.x = (1f - scalewidth) / 2f;
            }
            camera.rect = rect;
        }
    }
    
    #endregion
}
