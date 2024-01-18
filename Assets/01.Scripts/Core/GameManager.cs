public class GameManager : MonoSingleton<GameManager>
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
