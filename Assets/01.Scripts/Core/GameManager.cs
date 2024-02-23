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
}
