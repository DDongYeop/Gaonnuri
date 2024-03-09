using System;
using UnityEngine;

public class AreaCreator : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsArea;
    [SerializeField] private GameObject _temporaryObj;
    private Vector3Int _currentPosition;
    
    [Header("Audio Clip")]
    [SerializeField] private AudioClip _buttonDownClip;
    
    private void Update()
    {
        PositionCheck();
        //TemporaryArea();
        AreaCreate();
    }

    private void PositionCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, _whatIsArea))
        {
            Vector3 hitPos = hit.transform.position;
            _currentPosition = new Vector3Int((int)hitPos.x, (int)hitPos.y, (int)hitPos.z);
        }
        else
            _currentPosition = new Vector3Int(0, 1000000, 0);
    }

    private void TemporaryArea()
    {
        if (_currentPosition == new Vector3Int(0, 1000000, 0))
            _temporaryObj.transform.position = new Vector3(0, 1000000, 0);
        else
            _temporaryObj.transform.position = _currentPosition;
    }

    private void AreaCreate()
    {
        if (_currentPosition == new Vector3Int(0, 1000000, 0) || !Input.GetMouseButtonUp(0))
            return;
        if (UIManager.Instance.AreaDicCnt[AreaManager.Instance.CurrentAreaData] - 1 < 0 || GameManager.Instance.CurrentGameState != GameState.CREATOR)
            return;
        
        AudioPlayer audioPlayer = PoolManager.Instance.Pop("AudioPlayer") as AudioPlayer;
        audioPlayer.Setup(_buttonDownClip);
        
        UIManager.Instance.AreaDicCnt[AreaManager.Instance.CurrentAreaData] -= 1;
        UIManager.Instance.AreaDic[AreaManager.Instance.CurrentAreaData].UseArea(UIManager.Instance.AreaDicCnt[AreaManager.Instance.CurrentAreaData]);
        AreaManager.Instance.CreateArea(_currentPosition);
    }
}
