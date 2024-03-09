using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private AreaUI _areaUIObj;
    private Transform _contents;

    public Dictionary<AreaData, AreaUI> AreaDic = new Dictionary<AreaData, AreaUI>(); 
    public Dictionary<AreaData, int> AreaDicCnt = new Dictionary<AreaData, int>();

    private PlayerController _player;

    [Header("Canvas")] 
    private UICanvasState _currentUICanvasState = UICanvasState.INGAME;
    [SerializeField] private List<GameObject> _uiCanvas;

    [Header("Text")] 
    private TextMeshProUGUI _distanceTxt;
    private TextMeshProUGUI _jumpTxt;

    private void OnEnable()
    {
        _contents = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform;
    }

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _distanceTxt = transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        _jumpTxt = transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        
        foreach (AreaCounterDatas areaCounterData in StageManager.Instance.Stages[PlayerPrefs.GetInt("CurrentStage")].AreaCounterDatas)
        {
            AreaUI areaUI = Instantiate(_areaUIObj, _contents.transform);
            areaUI.SetUp(areaCounterData.Area, StageManager.Instance.AreaUiImage[(int)areaCounterData.Area], areaCounterData.Cnt);
            AreaDic[areaCounterData.Area] = areaUI;
            AreaDicCnt[areaCounterData.Area] = areaCounterData.Cnt;
        }
        
        SetUICanvas(UICanvasState.INGAME);
    }

    private void Update()
    {
        UpdateText();
    }

    #region Button

    public void StartButton()
    {
        CameraManager.Instance.PlayCameraPriority(5);
        GameManager.Instance.StateChange(GameState.PLAY, 1);
    }
    
    public void ReStartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RePlay()
    {
        _player.ChangeEarly();
        CameraManager.Instance.PlayCameraPriority(-5);
        GameManager.Instance.StateChange(GameState.CREATOR, 1);
        SetUICanvas(UICanvasState.INGAME);
    }

    public void HomeButton()
    {
        Admob.Instance.ShowAd();
        ScreenTransition.Instance.SceneChange(1, GameState.WAIT);
    }

    #endregion

    #region UICanvas

    public void SetUICanvas(UICanvasState state)
    {
        foreach (var canvas in _uiCanvas)
            canvas.SetActive(false);
        _currentUICanvasState = state;
        _uiCanvas[(int)_currentUICanvasState].SetActive(true);
    }

    #endregion

    private void UpdateText()
    {
        _distanceTxt.text = $"남은거리: {(Mathf.Floor(_player.MoveDistance * 100f) / 100f).ToString()}";
        _jumpTxt.text = $"뛰어오르기: {_player.JumpCnt.ToString()}";
    }
}
