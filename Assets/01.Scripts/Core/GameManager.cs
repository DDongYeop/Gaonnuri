using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    private GameState _currentGameState = GameState.WAIT;

    public GameState CurrentGameState => _currentGameState;

    [SerializeField] private PoolingListSO _initPoolList;
    public int MaxStage;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _currentGameState = GameState.WAIT;
        
        EarlySetting();
        FrameLimit();
        CreatePool();
        SceneManager.sceneLoaded += LoadedSceneEvent;
    }
    
    private void EarlySetting()
    {
        if (!PlayerPrefs.HasKey("MaxStage"))
            PlayerPrefs.SetInt("MaxStage", 1);
    }

    private void LoadedSceneEvent(Scene scene, LoadSceneMode mode)
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
        StateChange(GameState.CHANGE, 0);
        UIManager.Instance.SetUICanvas(UICanvasState.CLEAR);
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
        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)2 / 1); // (가로 / 세로)
        float scaleWidth = 1f / scaleHeight;
        
        foreach (var camera in cameras)
        {
            Rect rect = camera.rect;
            if (scaleHeight < 1)
            {
                rect.height = scaleHeight;
                rect.y = (1f - scaleHeight) / 2f;
            }
            else
            {
                rect.width = scaleWidth;
                rect.x = (1f - scaleWidth) / 2f;
            }
            camera.rect = rect;
        }
    }
    
    #endregion

    private void FrameLimit()
    {
#if UNITY_ANDROID
        Application.targetFrameRate = 120;
#endif
    }
    
    private void CreatePool()
    {
        PoolManager.Instance = new PoolManager(transform);
        _initPoolList.PoolList.ForEach(p =>
        {
            PoolManager.Instance.CreatePool(p.Prefab, p.Count);
        });
    }
}
