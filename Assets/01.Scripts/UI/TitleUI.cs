using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class TitleUI : MonoBehaviour
{
    private float _currentTime = 0;
    private Transform _mainCamera;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip _buttonDownClip;
    
    [Header("UI")] 
    private UIDocument _uiDocument;
    private VisualElement _gameLabel;
    private VisualElement _touchToPlayLabel;

    private void Awake()
    {
        _mainCamera = Camera.main.transform;
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        VisualElement rootElement = _uiDocument.rootVisualElement;
        _gameLabel = rootElement.Q<Label>("game-label");
        _touchToPlayLabel = rootElement.Q<Label>("touchtoplay-label");
    }

    private IEnumerator Start()
    {
        GameManager.Instance.StateChange(GameState.WAIT, 3);
        StartCoroutine(PositionTransitionCo(_mainCamera, new Vector3(10, -5, -10), new Vector3(10, 32.5f, -10), 2.5f));
        yield return new WaitForSeconds(2);
        _gameLabel.RemoveFromClassList("unshow");
        yield return new WaitForSeconds(1f);
        StartCoroutine(TouchToPlayCo(true));
    }

    private void Update()
    {
        #if UNITY_EDITOR
        if (GameManager.Instance.CurrentGameState == GameState.WAIT && Input.GetMouseButtonDown(0))
        {
            ScreenTransition.Instance.SceneChange(1, GameState.WAIT);
            AudioPlayer audioPlayer = PoolManager.Instance.Pop("AudioPlayer") as AudioPlayer;
            audioPlayer.Setup(_buttonDownClip);
        }
        #else
        if (GameManager.Instance.CurrentGameState == GameState.WAIT && Input.touchCount >= 1)
        {
            ScreenTransition.Instance.SceneChange(1, GameState.WAIT);
            AudioPlayer audioPlayer = PoolManager.Instance.Pop("AudioPlayer") as AudioPlayer;
            audioPlayer.Setup(_buttonDownClip);
        }
        #endif
    }

    private IEnumerator PositionTransitionCo(Transform trm, Vector3 start, Vector3 end, float time)
    {
        _currentTime = 0;
        while (_currentTime <= time)
        {
            yield return null;
            _currentTime += Time.deltaTime;
            float t = 1 - Mathf.Pow(1 - (_currentTime / time), 5);
            trm.position = Vector3.Lerp(start, end, t);
        }

        trm.position = end;
    }

    private IEnumerator TouchToPlayCo(bool value)
    {
        while (true)
        {
            if (value)
                _touchToPlayLabel.RemoveFromClassList("unshow");
            else
                _touchToPlayLabel.AddToClassList("unshow");
            value = !value;
            yield return new WaitForSeconds(1f);
        }
    }
}
