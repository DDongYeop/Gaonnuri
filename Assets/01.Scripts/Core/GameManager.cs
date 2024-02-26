using System.Collections;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameState CurrentGameState = GameState.CREATOR;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        CurrentGameState = GameState.CREATOR;
    }

    public void StateChange(GameState state, float duration)
    {
        StartCoroutine(StateChangeCo(state, duration));
    }

    private IEnumerator StateChangeCo(GameState state, float duration)
    {
        CurrentGameState = GameState.CHANGE;
        yield return new WaitForSecondsRealtime(duration);
        CurrentGameState = state;
    }
}
